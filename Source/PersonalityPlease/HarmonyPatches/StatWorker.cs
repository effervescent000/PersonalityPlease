using HarmonyLib;
using RimWorld;
using Verse;

namespace Personality.HarmonyPatches;

[HarmonyPatch(typeof(StatWorker), nameof(StatWorker.GetValueUnfinalized))]
public class StatWorkerPatch
{
    [HarmonyPostfix]
    public static void ApplyStatModifiers(ref float __result, StatRequest req, StatDef ___stat)
    {
        if (req.HasThing && req.Thing.def.defName == "Human" && req.Thing.Spawned)
        {
            MindComp comp = (req.Thing as Pawn).GetComp<MindComp>();
            ModifierValues modValues = comp?.Modifiers.GetValue(___stat.defName);
            __result += modValues?.Offset ?? 0f;
            foreach (var quirk in comp.Quirks)
            {
                __result += quirk.OffsetOfStat(___stat);
            }

            __result *= modValues?.Factor ?? 1f;
            foreach (var quirk in comp.Quirks)
            {
                __result *= quirk.MultiplierOfStat(___stat);
            }
        }
    }
}