using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Personality;

public class IdeoProfileComp : GameComponent
{
    private readonly HashSet<IdeoProfile> profiles = new();

    public override void LoadedGame()
    {
        base.LoadedGame();

        List<PersonalityNodeDef> personalityNodeDefs = PersonalityHelper.GetAll;

        List<Ideo> ideos = Find.IdeoManager.IdeosListForReading;
        foreach (Ideo ideo in ideos)
        {
            IdeoProfile profile = new(ideo);
            profile.MakeValues(personalityNodeDefs);
            profiles.Add(profile);
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