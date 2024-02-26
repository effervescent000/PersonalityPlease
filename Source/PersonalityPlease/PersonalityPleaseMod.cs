using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HugsLib;
using Verse;

namespace Personality;

internal class PersonalityPleaseMod : Mod
{
    public static Settings settings;

    public PersonalityPleaseMod(ModContentPack content) : base(content)
    {
        settings = GetSettings<Settings>();
    }

    public override string SettingsCategory()
    {
        return "PersonalityPlease".Translate();
    }
}