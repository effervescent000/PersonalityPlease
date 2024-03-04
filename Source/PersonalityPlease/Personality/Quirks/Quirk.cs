using Verse;

namespace Personality;

public class Quirk : IExposable
{
    public QuirkDef Def;

    public Quirk()
    {
    }

    public Quirk(QuirkDef def)
    {
        Def = def;
    }

    public string GetLabel => Def.label;

    public void ExposeData()
    {
        Scribe_Defs.Look(ref Def, "def");
    }
}