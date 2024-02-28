using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Personality;

public static class ColorHelper
{
    public static Color LightGray => Color.gray;
    public static Color MediumGray => new(.38f, .42f, .48f);
    public static Color DarkGray => new(.20f, .23f, .26f);
    public static Color OffBlack => new(.08f, .1f, .11f);

    public static float DistanceBetweenColors(Color a, Color b)
    {
        return Math.Abs(a.r - b.r) + Math.Abs(a.g - b.g) + Math.Abs(a.b - b.b);
    }
}