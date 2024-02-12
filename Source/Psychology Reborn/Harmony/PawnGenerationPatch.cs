using HarmonyLib;
using System;
using Verse;

namespace Personality.Harmony
{
    [HarmonyPatch(typeof(PawnGenerator), nameof(PawnGenerator.GeneratePawn), new Type[] { typeof(PawnGenerationRequest) })]
    public class PawnGenerationPatch
    {

        [HarmonyPostfix]
        public static Pawn AddPersonality(Pawn pawn)
        {
            if (pawn.def.defName == "Human")
            {
                PsychologyComp psyche = pawn.GetComp<PsychologyComp>();
                PsycheTracker tracker = new(pawn);
                tracker.Initialize();
                psyche.Psyche = tracker;
            }

            return pawn;
        }
    }
}
