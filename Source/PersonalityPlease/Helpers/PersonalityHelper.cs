using System.Collections.Generic;
using Verse;

namespace Personality;

[StaticConstructorOnStartup]
public static class PersonalityHelper
{
    public static TraitLedStore traitLedStore = new();

    private static List<PersonalityNodeDef> PersonalityNodeDefList => DefDatabase<PersonalityNodeDef>.AllDefsListForReading;

    static PersonalityHelper()
    {
        foreach (PersonalityNodeDef nodeDef in PersonalityNodeDefList)
        {
            if (!nodeDef.traitModifiers.NullOrEmpty())
            {
                foreach (PersonalityNodeTraitModifier traitMod in nodeDef.traitModifiers)
                {
                    traitLedStore.AppendValue(nodeDef.defName, traitMod);
                }
            }
            if (!nodeDef.preceptModifiers.NullOrEmpty())
            {
                foreach (PersonalityNodePreceptModifier preceptMod in nodeDef.preceptModifiers)
                {
                    PreceptLedStore.AppendValue(nodeDef.defName, preceptMod);
                }
            }
        }
    }

    public static int PawnSeed(Pawn pawn)
    {
        int thingID = pawn.thingIDNumber;
        int worldID = Find.World.info.Seed;
        return Gen.HashCombineInt(thingID, worldID);
    }

    public static MindComp Comp(Pawn pawn)
    {
        return pawn.GetComp<MindComp>();
    }

    public static List<PersonalityNodeDef> GetAll
    {
        get => DefDatabase<PersonalityNodeDef>.AllDefsListForReading;
    }

    public static string GetDescription(PersonalityNode node, Pawn pawn)
    {
        if (node.AdjustedRating >= 0.25)
        {
            return node.def.highDescription.Translate(pawn.Named("PAWN"));
        }
        if (node.AdjustedRating <= -0.25)
        {
            return node.def.lowDescription.Translate(pawn.Named("PAWN"));
        }
        return ((string)"PersonalityNodeAverage".Translate(node.def.highLabel.Translate(), node.def.lowLabel.Translate(), pawn.Named("PAWN")));
        //return ((string)"PersonalityNodeAverage".Formatted(pawn.Named("PAWN"))).Translate(node.def.highLabel.Translate(), node.def.lowLabel.Translate());
    }
}