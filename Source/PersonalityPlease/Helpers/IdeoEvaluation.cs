using RimWorld;
using System;
using Verse;

namespace Personality;

public class IdeoEvaluation
{
    private readonly MindComp comp;
    private readonly IdeoProfile ideoProfile;

    public IdeoEvaluation(Pawn pawn, Ideo ideo)
    {
        comp = pawn.GetComp<MindComp>();
        ideoProfile = Current.Game.GetComponent<IdeoProfileComp>()?.GetProfileFor(ideo);
    }

    public float FinalScore
    {
        get
        {
            float total = 1f;
            foreach (PersonalityNode node in comp.Mind.nodes.Values)
            {
                float ideoValue = ideoProfile.Values[node.def.defName];
                if (ideoValue == 0f) { continue; }
                float diff = Math.Abs(node.AdjustedRating - ideoValue);
                if (diff < 0.5f) { continue; }
                Log.Message($"found ideo diff of {diff}: pawn's value {node.AdjustedRating}, ideoValue: {ideoValue}");
                total -= diff * .5f;
            }
            return total;
        }
    }
}