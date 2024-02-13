using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Personality
{
    public class ITab_Pawn_Psyche : ITab
    {

        private static readonly Listing_Standard listingStandard = new();

        public ITab_Pawn_Psyche()
        {
            size = new Vector2(400f, 400f);
            labelKey = "TabPsyche";
            tutorTag = "Psyche";
        }

        protected override void FillTab()
        {

            Pawn pawn = PawnToDisplay;
            PsychologyComp psyche = pawn.GetComp<PsychologyComp>();

            Rect rect = new Rect(0f, 0f, size.x, size.y).ContractedBy(1f);
            Text.Font = GameFont.Small;
            GUI.color = Color.white;
            listingStandard.ColumnWidth = size.x - 20;
            listingStandard.Begin(rect);


            if (psyche != null)
            {
                Rect rectFromStandard = listingStandard.GetRect(20f, listingStandard.ColumnWidth);

                Dictionary<string, PersonalityNode> nodes = psyche.Psyche.nodes;

                int i = 0;
                foreach (PersonalityNode node in nodes.Values)
                {

                    string label = $"{node.def.defName} @ {node.AdjustedRating} (base {node.BaseRating})";
                    float textHeight = Text.CalcHeight(label, 250f);
                    Rect innerRect = new(0f, (rectFromStandard.y + textHeight) * i, 250f, textHeight);
                    Widgets.Label(innerRect, label);
                    Rect lineRect = new(innerRect.xMax, (rectFromStandard.y + textHeight) * i, 100f, textHeight);
                    UIComponents.LineWithIndicator(lineRect, value: node.AdjustedRating);

                    i++;
                }

            }

            listingStandard.End();

        }

        public override bool IsVisible
        {
            get
            {
                Pawn pawn = PawnToDisplay;
                if (pawn != null)
                {
                    if (pawn.def.defName == "Human")
                    {
                        return true;
                    }
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
}
