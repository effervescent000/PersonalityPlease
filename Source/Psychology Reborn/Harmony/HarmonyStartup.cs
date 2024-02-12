using HarmonyLib;
using Verse;

namespace Personality.Harmony
{
    [StaticConstructorOnStartup]
    public static class HarmonyStartup
    {
        static HarmonyStartup()
        {
            HarmonyLib.Harmony harmonyInstance = new HarmonyLib.Harmony("effervescent.personalityplease");
            harmonyInstance.PatchAll();
        }
    }
}
