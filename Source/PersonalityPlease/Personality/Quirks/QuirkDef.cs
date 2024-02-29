using System.Collections.Generic;
using Verse;

namespace Personality;

public class QuirkDef : Def
{
    public List<QuirkCategoryDef> categories;
    public bool binary = false;
}