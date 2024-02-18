using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality;

public class IdeoFeelingsTracker
{
    private readonly Pawn pawn;
    private float naturalCertainty;

    public IdeoFeelingsTracker(Pawn pawn)
    {
        this.pawn = pawn;
    }

    public float NaturalCertainty => naturalCertainty;

    public float CurrentIdeoHarmony => IdeoHelper.GetCompatibility(pawn, pawn.Ideo);

    public void FindNaturalCertainty()
    {
        // for now just use ideo harmony but would like to have traits and personality modify this eventually
        naturalCertainty = CurrentIdeoHarmony;
    }

    public void Initialize()
    {
        FindNaturalCertainty();
    }

    public void Notify_IdeoChanged()
    {
        FindNaturalCertainty();
    }
}