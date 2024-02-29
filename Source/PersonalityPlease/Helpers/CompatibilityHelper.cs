using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Personality;

public static class CompatibilityHelper
{
    public static float EvaluateCompatibility(Pawn initiator, Pawn recipient)
    {
        MindComp initComp = PersonalityHelper.Comp(initiator);
        MindComp reciComp = PersonalityHelper.Comp(recipient);

        List<PersonalityComparison> comparisons = new();
        List<PersonalityNodeDef> defs = PersonalityHelper.GetAll;

        foreach (PersonalityNodeDef def in defs)
        {
            PersonalityComparison comparison = new(initValue: initComp.nodes[def.defName].FinalRating.Value, reciValue: reciComp.nodes[def.defName].FinalRating.Value, personalityDefName: def.defName);
            comparisons.Add(comparison);
        }

        float totalCompatibility = 1f;
        foreach (PersonalityComparison comparison in comparisons)
        {
            float diff = comparison.Difference;
            if (diff >= .25)
            {
                totalCompatibility -= diff * (1f / defs.Count);
            }
        }
        return Mathf.Clamp01(totalCompatibility);
    }
}