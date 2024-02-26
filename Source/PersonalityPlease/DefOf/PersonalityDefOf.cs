using RimWorld;

namespace Personality;

[DefOf]
public static class PersonalityDefOf
{
    public static PersonalityNodeDef PP_Purity;

    static PersonalityDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(PersonalityDefOf));
    }
}