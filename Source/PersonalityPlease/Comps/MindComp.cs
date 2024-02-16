
using System.Collections.Generic;
using Verse;

namespace Personality;

public class MindComp : ThingComp
{
    private Mind mind;
    private ModifierTracker modifierTracker;

    public Mind Mind => mind;

    public ModifierTracker Modifiers => modifierTracker;

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Deep.Look(ref mind, "psyche", new object[] { parent as Pawn });
    }

    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        if (parent.def.defName == "Human")
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
                            Offset = modifier.isFactor ? 0f : modifier.GetValueAt(node.AdjustedRating),
                            Factor = modifier.isFactor ? modifier.GetValueAt(node.AdjustedRating) : 1f
                        };
                        modifierTracker.AppendValue(modifier.StatDef.defName, modValues);
                        //Log.Message($"Added stat value for {modifier.StatDef.defName}: {modValues.Offset} || {modValues.Factor}");
                    }
                }
            }
        }

    }
}
