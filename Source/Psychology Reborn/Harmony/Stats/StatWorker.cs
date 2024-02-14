using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace Personality.Harmony.Stats;

//[HarmonyPatch(typeof(StatWorker), nameof(StatWorker.GetValueUnfinalized))]
public class StatWorkerPatch
{
    //private static readonly AccessTools.FieldRef<object, StatDef> statRef = AccessTools.FieldRefAccess<StatDef>(typeof(StatWorker), "stat");

    //[HarmonyPostfix]
    //public static float ApplyStatModifiers(float value, StatRequest request)
    //{
    //    float factor = 1f;
    //    float offset = 0f;

    //    PsychologyComp comp = PersonalityHelper.Comp(request.Pawn);
    //    // todo tomorrow: extract the personality stuff from the comp,
    //    // check for values in applicable ranges, and apply their modifiers
    //    // before passing the request through
    //}
}
