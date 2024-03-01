using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Personality;

public static class GeneralHelper
{
    public static int GetSeed(this Pawn pawn)
    {
        int thingID = pawn.thingIDNumber;
        int worldID = Find.World.info.Seed;
        return Gen.HashCombineInt(thingID, worldID);
    }

    public static int GetHourBasedDuration(float hourMulti, float lowerBound = 0.5f, float upperBound = 1.5f)
    {
        return (int)(GenDate.TicksPerHour * hourMulti * Rand.Range(lowerBound, upperBound));
    }

    public static bool IsTargetInRange(Pawn actor, Pawn target)
    {
        return actor.Position.InHorDistOf(target.Position, PersonalityMod.Settings.MaxInteractionDistance.Value);
    }

    public static bool IsOk(this Pawn pawn)
    {
        if (pawn == null || pawn.health.Downed || pawn.health.Dead) return false;

        return true;
    }

    public static bool IsBloodRelatedTo(this Pawn pawn, Pawn target)
    {
        List<Pawn> familyMembers = (from member in pawn.relations.FamilyByBlood
                                    where member.ThingID == target.ThingID
                                    select member).ToList();

        if (familyMembers.Count > 0) { return true; }
        return false;
    }

    public static void ThrowHeart(this Pawn pawn)
    {
        FleckMaker.ThrowMetaIcon(pawn.Position, pawn.Map, FleckDefOf.Heart);
    }

    public static float RollValueInRange(float upperBound = 1f, float lowerBound = -1f, Random rng = null)
    {
        int roll;
        if (rng != null)
        {
            roll = rng.Next((int)(upperBound * 100), (int)(lowerBound * 100));
        }
        else
        {
            roll = Rand.RangeInclusive((int)(upperBound * 100), (int)(lowerBound * 100));
        }
        return roll / 100f;
    }
}