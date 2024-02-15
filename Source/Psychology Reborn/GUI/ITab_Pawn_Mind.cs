#nullable enable
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Personality
{
    public class ITab_Pawn_Mind : ITab
    {

        private static readonly Listing_Standard listingStandard = new();

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
                listingStandard.ColumnWidth = size.x - 20;
                listingStandard.Begin(rect);


                if (comp != null)
                {
                    
                    Rect rectFromStandard = listingStandard.GetRect(20f, 80);

                    Dictionary<string, PersonalityNode> nodes = comp.Mind.nodes;

                    int i = 0;
                    foreach (PersonalityNode node in nodes.Values)
                    {
                        Text.Font = GameFont.Small;
                        GUI.color = Color.white;
                        string label = node.def.defName;
                        float textHeight = Text.CalcHeight(label, 250f);
                        Rect innerRect = new(0f, (rectFromStandard.y + textHeight) * i, 150f, textHeight);
                        Widgets.Label(innerRect, label);
                        Rect lineRect = new(innerRect.xMax, (rectFromStandard.y + textHeight) * i, 100f, textHeight);
                        UIComponents.LineWithIndicator(lineRect, value: (node.AdjustedRating + 1) / 2, text: node.AdjustedRating.ToString());

                        i++;
                    }

                }

                listingStandard.End();
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
}
