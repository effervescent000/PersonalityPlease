using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Personality;

public static class MindCardUtility

{
    //[TweakValue("AAAtest", 1f, 2f)]
    //private static float naturalCertaintyLineHeightMulti = 1.5f;

    //[TweakValue("AAAtest", 0f, 3f)]
    //private static float naturalCertLinePlacementAdjusterMulti = .5f;

    public static void DrawMindCard(Pawn pawn, Vector2 size)
    {
        MindComp mind = pawn.GetComp<MindComp>();

        if (mind == null) { return; }

        Rect mainRect = new Rect(0f, 0f, size.x, size.y).ContractedBy(10f);

        float colWidth = 250f;

        Widgets.BeginGroup(mainRect);

        Rect certaintyRect = new(mainRect.x, mainRect.y, colWidth, 30f);
        DrawCertaintyBar(pawn, mind, certaintyRect);

        float personalityHeight = CalculatePersonalityHeight();
        Rect personalitySectionRect = UIComponents.DrawSection(mainRect.x, mainRect.y + certaintyRect.height + 10f, colWidth, personalityHeight);

        DrawPersonality(mind, personalitySectionRect);

        // add checks here for ideo being active and eventually, settings for ideo integration with mod
        IdeoProfileComp ideoProfileComp = Current.Game.GetComponent<IdeoProfileComp>();

        Rect ideoRect = new(personalitySectionRect.xMax + 20f, personalitySectionRect.y, colWidth * .35f, personalityHeight);
        DrawIdeoProfile(ideoProfileComp.GetProfileFor(pawn.Ideo), ideoRect.ContractedBy(10f));

        Rect ideoIconRect = new(ideoRect.center.x - certaintyRect.height * .5f, certaintyRect.y, certaintyRect.height, certaintyRect.height);
        pawn.Ideo.DrawIcon(ideoIconRect);

        Widgets.EndGroup();
    }

    public static void DrawCertaintyBar(Pawn pawn, MindComp comp, Rect rect)
    {
        Widgets.DrawBoxSolid(rect, Color.grey);
        Rect barRect = rect.ContractedBy(3f);
        Widgets.FillableBar(barRect, pawn.ideo.Certainty);
        DrawNaturalCertainty(comp, barRect);
        TooltipHandler.TipRegion(rect, () => GetCertaintyTipText(pawn, comp), 24521558);
    }

    public static string GetCertaintyTipText(Pawn pawn, MindComp mind)
    {
        string tip = "NaturalCertaintyGlobal".Translate() + " " + "NaturalCertaintyOfPawn".Translate(mind.IdeoFeelings.NaturalCertainty.ToStringPercent(), pawn.Named("PAWN"));
        return tip;
    }

    public static void DrawNaturalCertainty(MindComp comp, Rect certaintyRect)
    {
        float lineHeight = certaintyRect.height * 1.5f;
        Widgets.DrawLineVertical(certaintyRect.x + comp.IdeoFeelings.NaturalCertainty * certaintyRect.width, certaintyRect.y - lineHeight * .15f, lineHeight);
    }

    public static void DrawIdeoProfile(IdeoProfile profile, Rect rect)
    {
        int i = 0;
        Text.Font = GameFont.Tiny;
        foreach (var kvp in profile.Values)
        {
            float nodeHeight = rect.height * (float)(1f / profile.Values.Count);
            Rect nodeRect = new(rect.x, rect.y + (nodeHeight * i) - 5f, rect.width, nodeHeight);
            Rect lineRect = new(nodeRect.x, nodeRect.y - nodeHeight * .2f, nodeRect.width, nodeRect.height);
            UIComponents.DrawLineWithIndicator(lineRect, (kvp.Value.Value + 1) / 2f, text: kvp.Value.Value.ToString());

            i++;
        }
    }

    public static void DrawPersonality(MindComp mind, Rect rect)
    {
        var nodes = mind.Mind.nodes.Values.ToList();
        for (int i = 0; i < nodes.Count; i++)
        {
            Text.Font = GameFont.Small;
            float nodeHeight = rect.height * (float)(1f / nodes.Count);
            Rect nodeRect = new(rect.x, rect.y + (nodeHeight * i) + 5f, rect.width, nodeHeight);
            Rect labelRect = new(nodeRect.x, nodeRect.y, nodeRect.width * .55f, nodeRect.height);
            Widgets.Label(labelRect, nodes[i].def.label.Translate());

            Text.Font = GameFont.Tiny;
            Rect lineRect = new(nodeRect.width * .6f, nodeRect.y - nodeHeight * .2f, nodeRect.width * .45f, nodeRect.height);
            UIComponents.DrawLineWithIndicator(lineRect, (nodes[i].AdjustedRating.Value + 1) / 2f, text: nodes[i].AdjustedRating.Value.ToString());
        }
    }

    public static float CalculatePersonalityHeight()
    {
        float height = 0;
        foreach (var node in PersonalityHelper.GetAll)
        {
            height += Text.CalcHeight(node.defName, 100f) * 1.5f;
        }
        return height;
    }
}