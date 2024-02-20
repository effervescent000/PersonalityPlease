#nullable enable

using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Personality;

public class PersonalityNode : IExposable
{
    public PersonalityNodeDef def;
    private readonly Pawn pawn;

    public readonly SemiClampedValue BaseRating = new(-2f);
    public readonly SemiClampedValue AdjustedRating = new(0f);

    public PersonalityNode(PersonalityNodeDef def, float baseRating, Pawn pawn)
    {
        this.def = def;
        this.BaseRating.SetValue(baseRating);
        this.pawn = pawn;
    }

    public PersonalityNode(PersonalityNodeDef def, Pawn pawn)
    {
        this.def = def;
        this.pawn = pawn;
    }

    public override string ToString() => $"{def.defName} @ {BaseRating}";

    public void ModifyRating()
    {
        ModifyRating(pawn);
    }

    public void ModifyRating(Pawn pawn)
    {
        AdjustedRating.SetValue(BaseRating.Value);

        foreach (Trait trait in pawn.story.traits.allTraits)
        {
            Pair<string, int> traitPair = new(trait.def.defName, trait.Degree);
            Dictionary<string, float>? result = PersonalityHelper.traitLedStore.GetValue(traitPair);
            if (result is not null)
            {
                if (result.TryGetValue(def.defName, out float value))
                {
                    AdjustedRating.OffsetValue(value);
                }
            }
        }

        Ideo ideo = pawn.Ideo;
        foreach (Precept precept in ideo.PreceptsListForReading)
        {
            Dictionary<string, float> result = PreceptLedStore.GetValue(precept.def.defName);
            if (result is not null)
            {
                if (result.TryGetValue(def.defName, out float value))
                {
                    //Log.Message($"Adjusting personality node {def.defName} based on precept {precept.def.defName}");
                    AdjustedRating.OffsetValue(value);
                }
            }
        }
    }

    public void ExposeData()
    {
        Scribe_Defs.Look(ref def, "def");
        Scribe_Values.Look(ref BaseRating.NakedValue, "baseRating");
    }
}