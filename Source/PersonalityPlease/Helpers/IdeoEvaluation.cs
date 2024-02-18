using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            foreach (var node in mind.Mind.nodes.Values)
            {
                float ideoValue = ideoProfile.Values[node.def.defName];
                if (ideoValue == 0f) { continue; }
                total -= Math.Abs(node.AdjustedRating - ideoValue);
            }
            return total;
        }
    }
}