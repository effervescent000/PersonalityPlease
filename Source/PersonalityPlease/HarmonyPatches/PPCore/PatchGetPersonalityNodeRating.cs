using HarmonyLib;
using Personality.Core;
using Verse;

namespace Personality.HarmonyPatches;

[HarmonyPatch(typeof(CorePersonalityHelper), nameof(CorePersonalityHelper.GetPersonalityNodeRating))]
public static class PatchGetPersonalityNodeRating
{
    public static float Postfix(float _, string defName, Pawn pawn)
    {
        MindComp comp = pawn.GetComp<MindComp>();
        if (comp != null && comp.Mind.nodes.TryGetValue(defName, out PersonalityNode node))
        {
            return node.FinalRating.Value;
        }
        return 0f;
    }
}