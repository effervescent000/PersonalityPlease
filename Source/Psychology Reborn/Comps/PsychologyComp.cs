
using System.Collections.Generic;
using Verse;

namespace Personality;

public class PsychologyComp : ThingComp
{
    private PsycheTracker psyche;
    private ModifierTracker modifierTracker;

    public PsycheTracker Psyche => psyche;

    public ModifierTracker Modifiers => modifierTracker;

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Deep.Look(ref psyche, "psyche", new object[] { parent as Pawn });
    }

    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        if (parent.def.defName == "Human")
        {
            psyche = new(parent as Pawn);
            psyche.Initialize();
            psyche.ApplyAdjustments();

            modifierTracker = new ModifierTracker();

            foreach (PersonalityNode node in psyche.nodes.Values)
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
                        Log.Message($"Added stat value for {modifier.StatDef.defName}: {modValues.Offset} || {modValues.Factor}");
                    }
                }
            }
        }

    }
}
