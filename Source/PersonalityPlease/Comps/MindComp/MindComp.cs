﻿using System.Collections.Generic;
using Verse;

namespace Personality;

public class MindComp : ThingComp
{
    private MindTracker mind;
    private ModifierTracker modifierTracker;
    private IdeoFeelingsTracker ideoFeelings;

    public MindTracker Mind => mind;
    public ModifierTracker Modifiers => modifierTracker;
    public IdeoFeelingsTracker IdeoFeelings => ideoFeelings;

    public override void Initialize(CompProperties props)
    {
        base.Initialize(props);
    }

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Deep.Look(ref mind, "mind", new object[] { parent as Pawn });
    }

    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        mind = new(parent as Pawn);
        mind.Initialize();
        mind.ApplyAdjustments();

        modifierTracker = new ModifierTracker();

        foreach (PersonalityNode node in mind.nodes.Values)
        {
            List<PersonalityStatModifier> statMods = node.def.statModifiers;
            if (!statMods.NullOrEmpty())
            {
                foreach (PersonalityStatModifier modifier in statMods)
                {
                    ModifierValues modValues = new()
                    {
                        Offset = modifier.isFactor ? 0f : modifier.GetValueAt(node.FinalRating.Value),
                        Factor = modifier.isFactor ? modifier.GetValueAt(node.FinalRating.Value) : 1f
                    };
                    modifierTracker.AppendValue(modifier.StatDef.defName, modValues);
                }
            }
        }
        try
        {
            ideoFeelings = new(parent as Pawn);
            ideoFeelings.Initialize();
        }
        catch { }
    }

    public void Notify_IdeoProfileSetupDone()
    {
        ideoFeelings = new(parent as Pawn);
        ideoFeelings.Initialize();
    }
}