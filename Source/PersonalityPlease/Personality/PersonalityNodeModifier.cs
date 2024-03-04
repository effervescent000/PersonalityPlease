using Verse;

namespace Personality;

public class PersonalityNodeModifier<T> where T : Def
{
    public T Def;
    public float modifier;
}