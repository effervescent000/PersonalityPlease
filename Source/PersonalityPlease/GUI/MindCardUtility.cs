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

        float leftWidth = 250f;

        Widgets.BeginGroup(mainRect);

        Rect ideoRect = new(mainRect.x, mainRect.y, leftWidth, 30f);
        DrawCertaintyBar(pawn, mind, ideoRect);

        float personalityHeight = CalculatePersonalityHeight();
        Rect personalitySectionRect = DrawSection(mainRect.x, mainRect.y + ideoRect.height + 10f, leftWidth, personalityHeight);

        DrawPersonality(mind, personalitySectionRect);

        Widgets.EndGroup();
    }

    public static void DrawCertaintyBar(Pawn pawn, MindComp comp, Rect rect)
    {
        Widgets.DrawBoxSolid(rect, Color.grey);
        Rect barRect = rect.ContractedBy(3f);
        Widgets.FillableBar(barRect, pawn.ideo.Certainty);
        DrawNaturalCertainty(comp, barRect);
    }

    public static void DrawNaturalCertainty(MindComp comp, Rect certaintyRect)
    {
        float lineHeight = certaintyRect.height * 1.5f;
        Widgets.DrawLineVertical(certaintyRect.x + comp.IdeoFeelings.NaturalCertainty * 100, certaintyRect.y - lineHeight * .15f, lineHeight);
    }

    public static void DrawPersonality(MindComp mind, Rect rect)
    {
        var nodes = mind.Mind.nodes.Values.ToList();
        for (int i = 0; i < nodes.Count; i++)
        {
            Text.Font = GameFont.Small;
            Log.Message($"nodes: {nodes.Count}");
            float nodeHeight = rect.height * (float)(1f / nodes.Count);
            Rect nodeRect = new(rect.x, rect.y + (nodeHeight * i) + 5f, rect.width, nodeHeight);
            Rect labelRect = new(nodeRect.x, nodeRect.y, nodeRect.width * .55f, nodeRect.height);
            Widgets.Label(labelRect, nodes[i].def.label.Translate());

            Text.Font = GameFont.Tiny;
            Rect lineRect = new(nodeRect.width * .6f, nodeRect.y - nodeHeight * .2f, nodeRect.width * .45f, nodeRect.height);
            UIComponents.LineWithIndicator(lineRect, (nodes[i].AdjustedRating + 1) / 2f, text: nodes[i].AdjustedRating.ToString());
        }
    }

    public static Rect DrawSection(float x, float y, float width, float height)
    {
        Rect sectionRect = new(x, y, width, height);
        Widgets.DrawBoxSolidWithOutline(sectionRect, new Color(.2f, .2f, .2f), Color.grey);
        return new Rect(sectionRect).ContractedBy(10f);
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