using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality;

public class Settings : ModSettings
{
    // set internally based on active mods

    public static bool RomanceModuleActive = false;
    public static bool LovinModuleActive = false;

    public override void ExposeData()
    {
        base.ExposeData();
    }
}