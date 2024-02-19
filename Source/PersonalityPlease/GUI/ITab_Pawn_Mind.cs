#nullable enable

using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Personality;

public class ITab_Pawn_Mind : ITab
{
    public ITab_Pawn_Mind()
    {
        size = new Vector2(400f, 400f);
        labelKey = "TabMind";
        tutorTag = "Mind";
    }

    protected override void FillTab()
    {
        Pawn? pawn = PawnToDisplay;
        if (pawn != null)
        {
            MindComp comp = PersonalityHelper.Comp(pawn);

            Rect rect = new Rect(0f, 0f, size.x, size.y).ContractedBy(10f);

            Widgets.BeginGroup(rect);

            if (comp != null)
            {
                Rect personalityRect = new(rect.x, rect.y, size.x * .8f, size.y * .8f);
                float topPadding = 20f;

                Dictionary<string, PersonalityNode> nodes = comp.Mind.nodes;

                int i = 0;
                foreach (PersonalityNode node in nodes.Values)
                {
                    Text.Font = GameFont.Small;
                    GUI.color = Color.white;
                    string label = node.def.defName;
                    float textHeight = Text.CalcHeight(label, 250f);
                    Rect innerRect = new(0f, (personalityRect.y + textHeight) * i + topPadding, 150f, textHeight);
                    Widgets.Label(innerRect, label);
                    Rect lineRect = new(innerRect.xMax, (personalityRect.y + textHeight) * i + topPadding, 100f, textHeight);
                    UIComponents.LineWithIndicator(lineRect, value: (node.AdjustedRating + 1) / 2, text: node.AdjustedRating.ToString(), tooltip: PersonalityHelper.GetDescription(node, pawn));
                    i++;
                }
            }
            Widgets.EndGroup();
        }
    }

    public override bool IsVisible
    {
        get
        {
            Pawn? pawn = PawnToDisplay;
            if (pawn != null && pawn.def.defName == "Human")
            {
                return true;
            }
            return false;
        }
    }

    private Pawn? PawnToDisplay
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