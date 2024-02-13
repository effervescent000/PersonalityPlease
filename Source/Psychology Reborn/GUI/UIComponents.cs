﻿using System;
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


        public static void LineWithIndicator(Rect rect, float value, int height = 3)
        {
            //Widgets.BeginGroup(rect);


            Rect line = new(rect.x, rect.y + (rect.yMax - rect.yMin) / 2f, rect.width, height);
            Widgets.DrawBox(line, thickness: height);
            Widgets.DrawLineVertical(line.x + (value * line.width), line.y - height, height * 2.5f);

            //Widgets.EndGroup();
        }

    }
}
