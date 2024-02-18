using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality;

public static class IdeoHelper
{
    public static float GetCompatibility(Pawn pawn, Ideo ideo)
    {
        IdeoEvaluation eval = new(pawn, ideo);
        return eval.FinalScore;
    }

    //public static float GetCompatibilityWithCurrentIdeo(this Pawn pawn)
    //{
    //    return GetCompatibility(pawn, pawn.Ideo);
    //}
}