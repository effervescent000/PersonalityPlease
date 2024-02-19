using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Personality;

public static class UIComponents
{
    public static void LineWithIndicator(Rect rect, float value, int height = 3, string text = null, string tooltip = null)
    {
        text ??= value.ToString();
        Rect lineRect = new(rect.x, rect.y + (rect.yMax - rect.yMin) / 2f, rect.width, height);
        Widgets.DrawBox(lineRect, thickness: height);
        float verticalLineHeight = height * 2.5f;
        Widgets.DrawLineVertical(lineRect.x + (value * lineRect.width), lineRect.y - height, verticalLineHeight);
        Text.Font = GameFont.Tiny;
        Vector2 labelSize = Text.CalcSize(text);

        Rect labelRect = new((lineRect.x + value * lineRect.width) - (labelSize.x / 2), lineRect.y - verticalLineHeight * 2.5f, labelSize.x, labelSize.y);
        Widgets.Label(labelRect, text);
        if (tooltip != null)
        {
            Rect tipRegionRect = new(rect.x, rect.yMin - labelSize.y, lineRect.width, labelSize.y + rect.height);
            TooltipHandler.TipRegion(tipRegionRect, tooltip);
        }
    }

    public static void DrawNaturalCertainty(Pawn pawn, Rect certaintyRect)
    {
        MindComp comp = pawn.GetComp<MindComp>();
        Widgets.DrawLineVertical(certaintyRect.x + comp.IdeoFeelings.NaturalCertainty * 100, certaintyRect.y, 30f);
    }

    public static string GetNewCertaintyTooltipText(Pawn pawn)
    {
        Log.Message("im in the new method");
        return "test test";
    }
}