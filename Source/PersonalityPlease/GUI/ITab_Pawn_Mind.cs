using RimWorld;
using Verse;

namespace Personality;

public class ITab_Pawn_Mind : ITab
{
    public ITab_Pawn_Mind()
    {
        size = MindCardUtility.GetMindCardSize();
        labelKey = "TabMind";
        tutorTag = "Mind";
    }

    protected override void FillTab()
    {
        Pawn pawn = PawnToDisplay;
        if (pawn != null)
        {
            MindCardUtility.DrawMindCard(pawn, size);
        }
    }

    public override bool IsVisible
    {
        get
        {
            Pawn pawn = PawnToDisplay;
            if (pawn != null && pawn.def.defName == "Human")
            {
                return true;
            }
            return false;
        }
    }

    private Pawn PawnToDisplay
    {
        get
        {
            if (SelPawn != null)
            {
                return SelPawn;
            }
            if (SelThing is Corpse corpse)
            {
                return corpse.InnerPawn;
            }
            return null;
        }
    }
}