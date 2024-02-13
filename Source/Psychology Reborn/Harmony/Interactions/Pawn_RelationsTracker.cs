using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality.Harmony;

[HarmonyPatch(typeof(Pawn_RelationsTracker), nameof(Pawn_RelationsTracker.CompatibilityWith))]
public class Pawn_RelationsTracker_Patch
{

    private static AccessTools.FieldRef<object, Pawn> firstPawnRef = AccessTools.FieldRefAccess<Pawn>(typeof(Pawn_RelationsTracker), "pawn");

    [HarmonyPrefix]
    public static bool PatchCompatibility(Pawn otherPawn, ref float __result, Pawn_RelationsTracker __instance) {
        Log.Message("Attempting to patch compatibility...");
        Pawn pawnInst = new();

        ref Pawn firstPawn = ref firstPawnRef.Invoke(__instance);

        Log.Message($"Got {firstPawn} and {otherPawn}");
        float baseCompatibility = CompatibilityHelper.EvaluateCompatibility(firstPawn, otherPawn);
        Log.Message($"Compatibility is {baseCompatibility}");
        __result = baseCompatibility;
        Log.Message("Done!");
        return true;
    }

}
