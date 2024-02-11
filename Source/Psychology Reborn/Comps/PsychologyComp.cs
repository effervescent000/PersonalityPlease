using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Psychology_Reborn
{
    public class PsychologyComp : ThingComp
    {
        private PsycheTracker psyche;


        public PsycheTracker Psyche
        {
            get
            {
                if (this.psyche == null)
                {
                    Pawn pawn = this.parent as Pawn;
                    if (pawn != null)
                    {
                        this.psyche = new PsycheTracker(pawn);
                        this.psyche.Initialize();
                    }
                    else
                    {
                        Log.Message("help i dont understand");
                    }
                }
                return this.psyche;
            }
            set => this.psyche = value;
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Deep.Look(ref this.psyche, "psyche", new object[] { this.parent as Pawn });
        }
    }
}
