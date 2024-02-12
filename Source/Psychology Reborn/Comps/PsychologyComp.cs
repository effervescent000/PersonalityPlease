using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality
{
    public class PsychologyComp : ThingComp
    {
        private PsycheTracker psyche;


        public PsycheTracker Psyche
        {
            get
            {
                if (psyche == null)
                {
                    Pawn pawn = parent as Pawn;
                    if (pawn != null)
                    {
                        psyche = new PsycheTracker(pawn);
                        psyche.Initialize();
                    }
                    else
                    {
                        Log.Message("help i dont understand");
                    }
                }
                return psyche;
            }
            set => psyche = value;
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Deep.Look(ref psyche, "psyche", new object[] { parent as Pawn });
        }
    }
}
