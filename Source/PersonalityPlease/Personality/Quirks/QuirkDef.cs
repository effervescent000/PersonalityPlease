using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Personality;

public class QuirkDef : Def
{
    public QuirkCategoryDef category;
    public List<StatModifier> statFactors;
    public List<StatModifier> statOffsets;
    public bool hidden = false;
}