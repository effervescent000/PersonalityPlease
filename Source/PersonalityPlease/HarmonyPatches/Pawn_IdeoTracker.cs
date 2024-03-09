using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality.HarmonyPatches;

[HarmonyPatch(typeof(Pawn_IdeoTracker), nameof(Pawn_IdeoTracker.IdeoTrackerTick))]
public class Pawn_IdeoTracker_CertaintyPerDayPatch
{
    [HarmonyPrefix]
    public static bool PatchIdeoTrackerTick(Pawn ___pawn, ref float ___certaintyInt)
    {
        MindComp comp = ___pawn.GetComp<MindComp>();
        float? newCertainty = comp?.IdeoFeelings?.Tick();
        if (newCertainty != null)
        {
            ___certaintyInt = (float)newCertainty;
            return false;
        }
        return true;
    }
}