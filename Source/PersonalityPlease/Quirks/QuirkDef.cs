using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality;

public class QuirkDef : Def
{
    public List<QuirkCategoryDef> categories;
    public bool binary = false;
}