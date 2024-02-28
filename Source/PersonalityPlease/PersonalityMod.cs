using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HugsLib;
using Verse;

namespace Personality;

internal class PersonalityMod : Mod
{
    public static Settings Settings;

    public PersonalityMod(ModContentPack content) : base(content)
    {
        Settings = GetSettings<Settings>();
    }

    public override string SettingsCategory()
    {
        return "PersonalityPlease".Translate();
    }
}