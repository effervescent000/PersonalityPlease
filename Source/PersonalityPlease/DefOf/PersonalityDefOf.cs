using RimWorld;

namespace Personality;

[DefOf]
public static class PersonalityDefOf
{
    public static PersonalityNodeDef Ambition;
    public static PersonalityNodeDef Assertiveness;
    public static PersonalityNodeDef Compassion;
    public static PersonalityNodeDef Lawfulness;
    public static PersonalityNodeDef Meticulousness;
    public static PersonalityNodeDef PP_Purity;
    public static PersonalityNodeDef Rationality;

    static PersonalityDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(PersonalityDefOf));
    }
}