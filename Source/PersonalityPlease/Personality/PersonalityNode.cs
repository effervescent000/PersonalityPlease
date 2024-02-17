#nullable enable

using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Personality;

public class PersonalityNode : IExposable
{
    public PersonalityNodeDef def;
    private readonly Pawn pawn;
    private float baseRating = -2f;
    private float adjustedRating = 0f;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public PersonalityNode()

    {
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public PersonalityNode(PersonalityNodeDef def, float baseRating, Pawn pawn)
    {
        this.def = def;
        this.baseRating = baseRating;
        this.pawn = pawn;
    }

    public PersonalityNode(PersonalityNodeDef def, Pawn pawn)
    {
        this.def = def;
        this.pawn = pawn;
    }

    public float AdjustedRating
    {
        get => adjustedRating; set => adjustedRating = value;
    }

    public float BaseRating { get => baseRating; set => baseRating = value; }

    public override string ToString() => $"{def.defName} @ {baseRating}";

    public void ModifyRating()
    {
        ModifyRating(pawn);
    }

    public void ModifyRating(Pawn pawn)
    {
        adjustedRating = baseRating;

        foreach (Trait trait in pawn.story.traits.allTraits)
        {
            Pair<string, int> traitPair = new(trait.def.defName, trait.Degree);
            Dictionary<string, float>? result = PersonalityHelper.traitLedStore.GetValue(traitPair);
            if (result is not null)
            {
                if (result.TryGetValue(def.defName, out float value))
                {
                    adjustedRating += value;
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
                    //Log.Message($"Adjusting personality node {def.defName} based on precept {precept.def.defName}");
                    adjustedRating += value;
                }
            }
        }

        adjustedRating = Mathf.Clamp(adjustedRating, -1, 1);
    }

    public void ExposeData()
    {
        Scribe_Defs.Look(ref def, "def");
        Scribe_Values.Look(ref baseRating, "baseRating");
    }
}