using RimWorld;
using System.Collections.Generic;
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

    public float OffsetOfStat(StatDef stat)
    {
        List<StatModifier> offsets = Def.statOffsets;
        if (offsets != null)
        {
            foreach (StatModifier item in offsets)
            {
                if (item.stat == stat) return item.value;
            }
        }

        return 0f;
    }

    public float MultiplierOfStat(StatDef stat)
    {
        List<StatModifier> factors = Def.statFactors;
        if (factors != null)
        {
            foreach (StatModifier item in factors)
            {
                if (item.stat == stat) return item.value;
            }
        }

        return 1f;
    }

    public void ExposeData()
    {
        Scribe_Defs.Look(ref Def, "def");
    }
}