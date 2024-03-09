using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Personality;

public class IdeoProfileComp : GameComponent
{
    private readonly HashSet<IdeoProfile> profiles = new();

    public IdeoProfileComp(Game _)
    {
    }

    public override void FinalizeInit()
    {
        List<PersonalityNodeDef> personalityNodeDefs = PersonalityHelper.GetAll;

        List<Ideo> ideos = Find.IdeoManager.IdeosListForReading;
        foreach (Ideo ideo in ideos)
        {
            IdeoProfile profile = new(ideo);
            profile.MakeValues();
            profiles.Add(profile);
        }
        List<Pawn> pawnsToNotify = (from p in Find.AnyPlayerHomeMap.mapPawns.AllPawnsSpawned
                                    where p.def.defName == "Human" && p.ageTracker.AgeBiologicalYears >= 3
                                    select p).ToList();

        foreach (var p in pawnsToNotify)
        {
            MindComp mind = p.GetComp<MindComp>();
            mind.Notify_IdeoProfileSetupDone();
        }
    }

    public IdeoProfile GetProfileFor(Ideo ideo)
    {
        return (
            from profile in profiles
            where profile.Ideo.id == ideo.id
            select profile
            ).First();
    }
}