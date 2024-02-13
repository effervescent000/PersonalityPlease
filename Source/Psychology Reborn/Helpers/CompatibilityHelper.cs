﻿using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Personality
{
    public static class CompatibilityHelper
    {

        public static float EvaluateCompatibility(Pawn initiator, Pawn recipient)
        {
            PsychologyComp initComp = PersonalityHelper.Comp(initiator);
            PsychologyComp reciComp = PersonalityHelper.Comp(recipient);

            List<PersonalityComparison> comparisons = new();
            List<PersonalityNodeDef> defs = PersonalityHelper.GetAll;

            foreach (PersonalityNodeDef def in defs)
            {
                PersonalityComparison comparison = new(initValue: initComp.Psyche.nodes[def.defName].AdjustedRating, reciValue: reciComp.Psyche.nodes[def.defName].AdjustedRating, personalityDefName: def.defName);
                comparisons.Add(comparison);

            }

            float totalCompatibility = 1f;
            foreach (PersonalityComparison comparison in comparisons)
            {
                float diff = comparison.Difference;
                if (diff >= .25)
                {
                    totalCompatibility -= diff * ( 1f / defs.Count);
                }
            }
            return Mathf.Clamp01(totalCompatibility);

        }

    }
}
