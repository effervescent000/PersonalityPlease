using HarmonyLib;
using Verse;

namespace Personality.HarmonyPatches;

[StaticConstructorOnStartup]
public static class HarmonyStartup
{
    static HarmonyStartup()
    {
        //Harmony.DEBUG = true;
        Harmony harmonyInstance = new("effervescent.personalityplease");
        harmonyInstance.PatchAll();
    }
}