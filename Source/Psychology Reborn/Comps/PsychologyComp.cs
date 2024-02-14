using Verse;

namespace Personality;

public class PsychologyComp : ThingComp
{
    private PsycheTracker psyche;

    public PsycheTracker Psyche
    {
        get
        {
            return psyche;
        }
        set => psyche = value;
    }

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Deep.Look(ref psyche, "psyche", new object[] { parent as Pawn });
    }

    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        if (parent.def.defName == "Human")
        {
            psyche = new(parent as Pawn);
            psyche.Initialize();
            psyche.ApplyAdjustments();
        }
        
    }
}
