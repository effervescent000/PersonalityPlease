﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality;

public class Settings : ModSettings
{
    public SettingValues<int> MaxInteractionDistance = new(50, "PP.MaxInteractDistance.Label", "PP.MaxInteractDistance.Desc", 25, 250);

    // set internally based on active mods
    public static bool LovinModuleActive = false;

    public override void ExposeData()
    {
        base.ExposeData();
    }
}