using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace Personality.HarmonyPatches;

[HarmonyDebug]
[HarmonyPatch(typeof(SocialCardUtility), nameof(SocialCardUtility.DrawPawnCertainty))]
public class DrawPawnCertainty
{
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> ModifyCertaintyBar(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        //int countSinceSkip = 1000;
        //int endCount = 0;
        //bool skip = false;

        MethodInfo drawMethod = AccessTools.Method(typeof(UIComponents), "DrawNaturalCertainty");
        //MethodInfo colorizeMethod = AccessTools.Method(typeof(ColoredText), nameof(ColoredText.Colorize), new Type[] { typeof(TaggedString), typeof(Color) });
        //MethodInfo greyProp = AccessTools.PropertyGetter(typeof(Color), "grey");
        //MethodInfo overwriteTooltipMethod = AccessTools.Method(typeof(UIComponents), "GetNewCertaintyTooltipText");
        //FieldInfo tipField = null;

        //var fields = AccessTools.GetDeclaredFields(typeof(string));
        //foreach (var field in fields)
        //{
        //    if (field.FieldType == typeof(string))
        //    {
        //        Log.Message(field.Attributes.ToString());
        //    }
        //}
        //if (tipField == null)
        //{
        //    throw new Exception("tipField still null");
        //}
        var codes = instructions.ToList();
        for (int i = 0; i < codes.Count; i++)
        {
            if (i == codes.Count - 1)
            {
                yield return new CodeInstruction(OpCodes.Ldarg_0);
                yield return new CodeInstruction(OpCodes.Ldloc, 4);
                yield return new CodeInstruction(OpCodes.Call, drawMethod);
                yield return codes[i];
            }
            //else if (countSinceSkip <= 8)
            //{
            //    // do nothing
            //}
            //else if (codes[i].opcode == OpCodes.Endfinally)
            //{
            //    yield return codes[i];
            //    if (endCount == 1)
            //    {
            //        yield return new CodeInstruction(OpCodes.Ldarg_0);
            //        yield return new CodeInstruction(OpCodes.Call, overwriteTooltipMethod);
            //        yield return new CodeInstruction(OpCodes.Stloc_S, 6);

            //        countSinceSkip = 0;
            //        skip = true;
            //    }
            //    else
            //    {
            //        endCount++;
            //    }
            //}
            else
            {
                yield return codes[i];
            }
            //countSinceSkip++;
        }
    }
}