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

    private static readonly SimpleCurve certaintyGainByDistanceFromGoodwill = new()
    {
        new CurvePoint(-.6f, .03f), new CurvePoint(.6f, -.03f)
    };

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

    public float CertaintyChangePerDay
    {
        get
        {
            return certaintyGainByDistanceFromGoodwill.Evaluate(pawn.ideo.Certainty) + ConversionTuning.CertaintyPerDayByMoodCurve.Evaluate(pawn.needs.mood.CurInstantLevel) * 0.75f;
        }
    }

    public void Tick()
    {
        if (!pawn.Destroyed && !pawn.InMentalState && pawn.ideo != null && !Find.IdeoManager.classicMode && !pawn.Deathresting)
        {
            // TODO figure out what to do with certainty change multiplier? `CertaintyChangeFactor`
            float curCertainty = pawn.ideo.Certainty;
            float newCertainty = (curCertainty + CertaintyChangePerDay) / 60_000f;
            pawn.ideo.OffsetCertainty(newCertainty - curCertainty);
        }
    }
}