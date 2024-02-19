using RimWorld;
using System;
using Verse;

namespace Personality;

public class IdeoEvaluation
{
    private readonly MindComp mind;
    private readonly IdeoProfile ideoProfile;

    public IdeoEvaluation(Pawn pawn, Ideo ideo)
    {
        mind = pawn.GetComp<MindComp>();
        ideoProfile = Current.Game.GetComponent<IdeoProfileComp>()?.GetProfileFor(ideo);
    }

    public float FinalScore
    {
        get
        {
            float total = 1f;
            foreach (PersonalityNode node in mind.Mind.nodes.Values)
            {
                float ideoValue = ideoProfile.Values[node.def.defName];
                if (ideoValue == 0f) { continue; }
                float diff = Math.Abs(node.AdjustedRating - ideoValue);
                if (diff < 0.25) { continue; }
                total -= diff;
            }
            return total;
        }
    }
}