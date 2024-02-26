#nullable enable

using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Personality;

public class PersonalityNode : IExposable
{
    public PersonalityNodeDef def;
    private readonly Pawn pawn;

    // the base rating is the raw roll
    public readonly SemiClampedValue BaseRating = new(-2f);

    // the personal rating is the rating including traits (and maybe other things eventually?)
    public readonly SemiClampedValue PersonalRating = new(0f);

    // the final rating includes changes from ideo
    public readonly SemiClampedValue FinalRating = new(0f);

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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public PersonalityNode()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    { }

    public void ModifyRating()
    {
        ModifyRating(pawn);
    }

    public void ModifyRating(Pawn pawn)
    {
        PersonalRating.SetValue(BaseRating.Value);

        foreach (Trait trait in pawn.story.traits.allTraits)
        {
            Pair<string, int> traitPair = new(trait.def.defName, trait.Degree);
            Dictionary<string, float>? result = PersonalityHelper.traitLedStore.GetValue(traitPair);
            if (result is not null)
            {
                if (result.TryGetValue(def.defName, out float value))
                {
                    PersonalRating.OffsetValue(value);
                }
            }
        }

        FinalRating.SetValue(PersonalRating.NakedValue);

        Ideo ideo = pawn.Ideo;
        foreach (Precept precept in ideo.PreceptsListForReading)
        {
            Dictionary<string, float> result = PreceptLedStore.GetValue(precept.def.defName);
            if (result is not null)
            {
                if (result.TryGetValue(def.defName, out float value))
                {
                    //Log.Message($"Adjusting personality node {def.defName} based on precept {precept.def.defName}");
                    FinalRating.OffsetValue(value);
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