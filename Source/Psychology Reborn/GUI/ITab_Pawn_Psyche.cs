using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using UnityEngine;
using Verse;

namespace Psychology_Reborn
{
    public class ITab_Pawn_Psyche : ITab
    {
        public ITab_Pawn_Psyche()
        {
            size = new Vector2(200f, 200f);
            labelKey = "TabPsyche";
            tutorTag = "Psyche";
        }

        protected override void FillTab()
        {
            Rect rect = new Rect(0f, 0f, 1f, 1f);
            GUI.BeginGroup(rect);
            GUI.EndGroup();
            
        }

        public override bool IsVisible
        {
            get {
                //Log.Message("IS VISIBLE");
                return true;
            }
        }

    }
}
