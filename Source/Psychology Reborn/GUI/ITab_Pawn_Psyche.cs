using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Personality
{
    public class ITab_Pawn_Psyche : ITab
    {

        private static readonly Listing_Standard listingStandard = new Listing_Standard();

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
                    float textHeight = Text.CalcHeight(label, 200f);
                    Rect innerRect = new Rect(0f, (rectFromStandard.y + textHeight) * i, 200f, textHeight);
                    //GUI.BeginGroup(innerRect);

                    //listingStandard.Label(label);
                    Widgets.Label(innerRect, label);

                    //GUI.EndGroup();

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
                    PsychologyComp psyche = pawn.GetComp<PsychologyComp>();
                    if (psyche == null)
                    {
                        PsycheTracker tracker = new PsycheTracker(pawn);
                        tracker.Initialize();
                    }
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
