#nullable enable
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Personality
{
    public class PersonalityNode : IExposable
    {
        public Pawn pawn;
        public PersonalityNodeDef def;
        private float baseRating;
        private float cachedRating = 0.5f;


        public PersonalityNode(Pawn pawn, PersonalityNodeDef def)
        {
            this.pawn = pawn;
            this.def = def;
        }

        public float AdjustedRating
        {
            get => cachedRating; set => cachedRating = value;
        }

        public float BaseRating { get => baseRating; set => baseRating = value; }

        public override string ToString() => $"{def.defName} @ {baseRating}";

        public void ExposeData()
        {
            Scribe_Defs.Look(ref def, "def");
            Scribe_Values.Look(ref baseRating, "baseRating");
        }

        public void ModifyRating()
        {
            cachedRating = baseRating;

            foreach (Trait trait in pawn.story.traits.allTraits)
            {
                Pair<string, int> traitPair = new(trait.def.defName, trait.Degree);
                Dictionary<string, float>? result = PersonalityHelper.traitLedStore.GetValue(traitPair);
                if (result is not null)
                {

                    if (result.TryGetValue(def.defName, out float value))
                    {
                        cachedRating += value;
                    }
                }

            }

            Ideo ideo = pawn.Ideo;
            foreach (Precept precept in ideo.PreceptsListForReading)
            {
                Dictionary<string, float>? result = PersonalityHelper.preceptLedStore.GetValue(precept.def.defName);
                if (result is not null)
                {
                    if (result.TryGetValue(def.defName, out float value))
                    {
                        Log.Message($"Adjusting personality node {def.defName} based on precept {precept.def.defName}");
                        cachedRating += value;
                    }
                }

            }

            cachedRating = Mathf.Clamp01(cachedRating);

        }
    }

}
