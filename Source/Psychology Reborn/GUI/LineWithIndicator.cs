using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Personality
{
    public static class LineWithIndicator
    {


        public static void DrawLineWithIndicator(Rect rect, float value, int height = 3)
        {
            //Widgets.BeginGroup(rect);


            Rect line = new Rect(rect.x, rect.y, rect.width, height);
            Widgets.DrawBox(line, thickness: height);
            float normalizedValue = Mathf.Clamp01((value + 1) / 2);
            Widgets.DrawLineVertical(line.x + (normalizedValue * line.width), line.y, height * 2.5f);

            //Widgets.EndGroup();
        }

    }
}
