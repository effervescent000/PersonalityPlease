using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality;

public class PreceptComp_SelfTookMemoryThoughtWithSubject : PreceptComp_Thought
{
    public HistoryEventDef eventDef;

    public override void Notify_MemberTookAction(HistoryEvent ev, Precept precept, bool canApplySelfTookThoughts)
    {
        if (ev.def != eventDef || !canApplySelfTookThoughts) return;

        Pawn doer = ev.args.GetArg<Pawn>(HistoryEventArgsNames.Doer);
        Pawn subject = ev.args.GetArg<Pawn>(HistoryEventArgsNames.Subject);

        if (doer.CanHaveThoughts())
        {
            Thought_Memory memory = ThoughtMaker.MakeThought(thought, precept);
            memory.otherPawn = subject;
            doer.TryGiveThought(memory);
        }
    }
}