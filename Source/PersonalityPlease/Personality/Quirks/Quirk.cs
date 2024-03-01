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
            value = GeneralHelper.RollValueInRange();
        }
    }

    public string GetLabel
    {
        get
        {
            if (Def.binary) return Def.label.Translate();

            if (value >= 0.25f) return Def.highLabel.Translate();
            if (value <= 0.25f) return Def.lowLabel.Translate();

            return null;
        }
    }

    public void ExposeData()
    {
        Scribe_Defs.Look(ref Def, "def");
        Scribe_Values.Look(ref value, "value");
    }
}