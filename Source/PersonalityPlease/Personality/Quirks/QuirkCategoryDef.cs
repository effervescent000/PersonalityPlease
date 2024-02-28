using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality;

public class QuirkCategoryDef : Def
{
    // does every pawn have to have at least one quirk in this category?
    public bool required;

    // how many quirks in this category are pawns allowed to have?
    public int limit = 1;

    public int minAge = 3;
}