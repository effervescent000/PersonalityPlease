using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality;

[StaticConstructorOnStartup]
public static class OnStartup
{
    static OnStartup()
    {
        if (ModsConfig.IsActive("effervescent.personalityplease.romance"))
        {
            Settings.RomanceModuleActive = true;
        }
        if (ModsConfig.IsActive("effervescent.personalityplease.lovin"))
        {
            Settings.LovinModuleActive = true;
        }

        Harmony harmony = new("effervescent.personalityplease");
        harmony.PatchAll();
    }
}