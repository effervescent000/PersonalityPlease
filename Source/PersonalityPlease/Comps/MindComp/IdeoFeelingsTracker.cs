using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Personality;

public class IdeoFeelingsTracker
{
    private readonly Pawn pawn;

    public IdeoFeelingsTracker(Pawn pawn)
    {
        this.pawn = pawn;
    }
}