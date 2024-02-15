using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Personality
{
    public static class UIComponents
    {

        public static void LineWithIndicator(Rect rect, float value, int height = 3, string text = null)
        {
            //Widgets.BeginGroup(rect);

            text ??= value.ToString();
            Rect line = new(rect.x, rect.y + (rect.yMax - rect.yMin) / 2f, rect.width, height);
            Widgets.DrawBox(line, thickness: height);
            float verticalLineHeight = height * 2.5f;
            Widgets.DrawLineVertical(line.x + (value * line.width), line.y - height, verticalLineHeight);
            Text.Font = GameFont.Tiny;
            Vector2 labelSize = Text.CalcSize(text);
            Rect labelRect = new((line.x + value * line.width) - (labelSize.x / 2), line.y - verticalLineHeight * 2.5f, labelSize.x, labelSize.y);
            Widgets.Label(labelRect, text);

            //Widgets.EndGroup();
        }

    }
}
