using Verse;

namespace Personality;

public class Quirk : IExposable
{
    public QuirkDef Def;
    public float value;

    public float Value => value;

    public Quirk()
    {
    }

    public Quirk(QuirkDef def, MindComp mind)
    {
        Def = def;
        MakeValue(mind);
    }

    private void MakeValue(MindComp _)
    {
        if (Def.binary)
        {
            value = 1f;
        }
        else
        {
            // eventually, this will probably allow for clamping the rolled range in some way based
            // on... parameters
            int roll = Rand.RangeInclusive(-100, 100);
            value = roll / 100f;
        }
    }

    public void ExposeData()
    {
        Scribe_Defs.Look(ref Def, "def");
        Scribe_Values.Look(ref value, "value");
    }
}