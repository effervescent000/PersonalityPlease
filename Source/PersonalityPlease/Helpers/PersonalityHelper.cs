using System.Collections.Generic;
using Verse;

namespace Personality;

[StaticConstructorOnStartup]
public static class PersonalityHelper
{
    public static TraitLedStore traitLedStore = new();

    public static Dictionary<string, PersonalityNodeDef> NodeMapping = new()
    {
        { "PP_Purity", PersonalityDefOf.PP_Purity }
    };

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

    public static MindComp Comp(Pawn pawn) => pawn.GetComp<MindComp>();

    public static List<PersonalityNodeDef> GetAll => DefDatabase<PersonalityNodeDef>.AllDefsListForReading;

    public static string GetDescription(PersonalityNode node, Pawn pawn)
    {
        if (node.FinalRating.Value >= 0.25f)
        {
            return node.def.highDescription.Translate(pawn.Named("PAWN"));
        }
        if (node.FinalRating.Value <= -0.25f)
        {
            return node.def.lowDescription.Translate(pawn.Named("PAWN"));
        }
        return ((string)"PersonalityNodeAverage".Translate(node.def.highLabel.Translate(), node.def.lowLabel.Translate(), pawn.Named("PAWN")));
        //return ((string)"PersonalityNodeAverage".Formatted(pawn.Named("PAWN"))).Translate(node.def.highLabel.Translate(), node.def.lowLabel.Translate());
    }
}