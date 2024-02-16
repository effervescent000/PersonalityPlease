using HarmonyLib;
using RimWorld;
using Verse;

namespace Personality.HarmonyPatches;

[HarmonyPatch(typeof(Pawn_RelationsTracker), nameof(Pawn_RelationsTracker.CompatibilityWith))]
public class Pawn_RelationsTracker_Patch
{

    private static readonly AccessTools.FieldRef<object, Pawn> firstPawnRef = AccessTools.FieldRefAccess<Pawn>(typeof(Pawn_RelationsTracker), "pawn");

    [HarmonyPrefix]
    public static bool PatchCompatibility(Pawn otherPawn, ref float __result, Pawn_RelationsTracker __instance)
    {
        ref Pawn firstPawn = ref firstPawnRef.Invoke(__instance);
        float baseCompatibility = CompatibilityHelper.EvaluateCompatibility(firstPawn, otherPawn);
        //Log.Message($"Compatibility is {baseCompatibility}");
        __result = baseCompatibility;
        return true;
    }

}
