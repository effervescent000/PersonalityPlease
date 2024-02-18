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
    private static readonly AccessTools.FieldRef<object, Pawn> pawnRef = AccessTools.FieldRefAccess<Pawn>(typeof(Pawn_IdeoTracker), "pawn");

    [HarmonyPrefix]
    public static bool PatchIdeoTrackerTick(Pawn_IdeoTracker __instance)
    {
        ref Pawn pawn = ref pawnRef.Invoke(__instance);
        MindComp comp = pawn.GetComp<MindComp>();
        comp?.IdeoFeelings?.Tick();
        return true;
    }
}