using HarmonyLib;
using RimWorld;
using Verse;

namespace Personality.Harmony.Stats;

[HarmonyPatch(typeof(StatWorker), nameof(StatWorker.GetValueUnfinalized))]
public class StatWorkerPatch
{
    private static readonly AccessTools.FieldRef<object, StatDef> statRef = AccessTools.FieldRefAccess<StatDef>(typeof(StatWorker), "stat");

    [HarmonyPostfix]
    public static void ApplyStatModifiers(ref float __result, StatRequest req, StatWorker __instance)
    {
        if (req.HasThing && req.Thing.def.defName == "Human" && req.Thing.Spawned)
        {
            ref StatDef stat = ref statRef.Invoke(__instance);

            PsychologyComp comp = PersonalityHelper.Comp((Pawn)req.Thing);
            ModifierValues modValues = comp.Modifiers.GetValue(stat.defName);
            if (modValues != null)
            {
                //if (modValues.Offset != 0f || modValues.Factor != 1f)
                //{
                //    Log.Message($"Patching StatWorker for {stat.defName} for {req.Thing.ThingID} with a factor of {modValues.Factor} and an offset of {modValues.Offset}");
                //}
                
                __result += modValues.Offset;
                __result *= modValues.Factor;
            }
            

        }

    }
}
