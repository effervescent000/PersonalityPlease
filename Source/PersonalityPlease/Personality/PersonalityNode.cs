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
        BaseRating.SetValue(baseRating);
        this.pawn = pawn;
    }

    public PersonalityNode(PersonalityNodeDef def, Pawn pawn)
    {
        this.def = def;
        this.pawn = pawn;
    }

    public override string ToString() => $"{def.defName} @ {BaseRating}";

    public PersonalityNode()

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
            Dictionary<string, float> result = PersonalityHelper.traitLedStore.GetValue(traitPair);
            if (result is not null)
            {
                if (result.TryGetValue(def.defName, out float value))
                {
                    PersonalRating.OffsetValue(value);
                }
            }
        }

        if (def.geneModifiers?.Count > 0)
        {
            foreach (PersonalityNodeModifier<GeneDef> geneModifier in def.geneModifiers)
            {
                if (pawn.genes.HasGene(geneModifier.Def))
                {
                    PersonalRating.OffsetValue(geneModifier.modifier);
                }
            }
        }

        FinalRating.SetValue(PersonalRating.NakedValue);

        Ideo ideo = pawn.Ideo;
        if (ideo != null)
        {
            foreach (Precept precept in ideo.PreceptsListForReading)
            {
                Dictionary<string, float> result = PreceptLedStore.GetValue(precept.def.defName);
                if (result is not null)
                {
                    if (result.TryGetValue(def.defName, out float value))
                    {
                        FinalRating.OffsetValue(value);
                    }
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