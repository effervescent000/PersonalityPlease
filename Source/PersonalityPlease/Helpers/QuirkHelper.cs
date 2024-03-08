using System.Collections.Generic;
using Verse;

namespace Personality;

[StaticConstructorOnStartup]
public static class QuirkHelper
{
    public static HashSet<QuirkCategoryDef> RequiredCategories = new();
    public static Dictionary<string, List<QuirkDef>> QuirkDefsByCategory = new();

    static QuirkHelper()
    {
        List<QuirkCategoryDef> categories = DefDatabase<QuirkCategoryDef>.AllDefsListForReading;
        foreach (QuirkCategoryDef category in categories)
        {
            QuirkDefsByCategory.Add(category.defName, new());
            if (category.required)
            {
                RequiredCategories.Add(category);
            }
        }

        List<QuirkDef> quirkDefs = DefDatabase<QuirkDef>.AllDefsListForReading;
        foreach (QuirkDef quirk in quirkDefs)
        {
            if (quirk.category != null)
            {
                QuirkDefsByCategory[quirk.category.defName].Add(quirk);
            }
        }
    }
}