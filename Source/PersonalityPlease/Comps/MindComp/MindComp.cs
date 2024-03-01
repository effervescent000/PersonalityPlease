﻿using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Personality;

public class MindComp : ThingComp
{
    private ModifierTracker modifierTracker;
    private IdeoFeelingsTracker ideoFeelings;

    public Dictionary<string, PersonalityNode> nodes;
    private HashSet<Quirk> quirks;

    private Pawn Pawn => (Pawn)parent;

    public ModifierTracker Modifiers => modifierTracker;
    public IdeoFeelingsTracker IdeoFeelings => ideoFeelings;

    public MindComp()
    {
    }

    public override void Initialize(CompProperties props)
    {
        base.Initialize(props);
    }

    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        InitializePersonalityNodes();
        ApplyAdjustments();

        InitializeQuirks(respawningAfterLoad);

        modifierTracker = new ModifierTracker();

        foreach (PersonalityNode node in nodes.Values)
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

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Collections.Look(ref nodes, "personality", LookMode.Value, LookMode.Deep);
        Scribe_Collections.Look(ref quirks, "quirks", LookMode.Deep);

        quirks ??= new();
    }

    public void InitializePersonalityNodes()
    {
        nodes = new Dictionary<string, PersonalityNode>();
        foreach (PersonalityNodeDef def in DefDatabase<PersonalityNodeDef>.AllDefsListForReading)
        {
            nodes.Add(def.defName, new PersonalityNode(def, Pawn));
        }

        int seed = Pawn.GetSeed();
        Random random = new(seed);

        foreach (PersonalityNode node in nodes.Values)
        {
            if (node.BaseRating.NakedValue < -1)
            {
                float r = random.Next(-100, 100);
                node.BaseRating.SetValue(r / 100f);
            }
        }
    }

    public void ApplyAdjustments()
    {
        ApplyAdjustments(Pawn);
    }

    public void ApplyAdjustments(Pawn pawn)
    {
        foreach (PersonalityNode node in nodes.Values)
        {
            node.ModifyRating(pawn);
        }
    }

    public PersonalityNode GetNode(string key)
    {
        if (nodes.TryGetValue(key, out PersonalityNode node))
        {
            return node;
        }
        return null;
    }

    public void InitializeQuirks(bool respawningAfterLoad)
    {
        quirks ??= new();
        if (!respawningAfterLoad)
        {
            MakeQuirks();
        }
    }

    public HashSet<Quirk> Quirks => quirks;

    public bool GetQuirkByDef(QuirkDef def, out Quirk quirk)
    {
        quirk = quirks.FirstOrFallback(x => x.Def == def);
        return quirk != null;
    }

    public List<Quirk> GetQuirksByCategory(QuirkCategoryDef category)
    {
        return (from quirk in quirks
                where quirk.Def.categories.Contains(category)
                select quirk).ToList();
    }

    public Quirk GetFirstQuirkInCategory(QuirkCategoryDef category)
    {
        return (from quirk in quirks
                where quirk.Def.categories.Contains(category)
                select quirk).First();
    }

    public Quirk TryGenerateQuirkForCategory(QuirkCategoryDef category)
    {
        if (QuirkHelper.QuirkDefsByCategory.TryGetValue(category.defName, out List<QuirkDef> value))
        {
            if (value.Count > 0)
            {
                QuirkDef selection = value.RandomElement();
                Quirk quirk = new(selection, this);
                return quirk;
            }
        }
        return null;
    }

    public void MakeQuirks()
    {
        //always generate required categories, then generate 2-5? additional quirks
        foreach (QuirkCategoryDef x in QuirkHelper.RequiredCategories)
        {
            Quirk quirk = TryGenerateQuirkForCategory(x);
            if (quirk != null)
            {
                TryAddQuirk(quirk);
            }
        }
    }

    /// <summary>
    /// attempt to get a quirk by its def, or if none is found, assign one. should only be used for
    /// uncategorized quirkdefs.
    /// </summary>
    /// <param name="def"></param>
    /// <returns></returns>
    public Quirk GetOrGainQuirkSingular(QuirkDef def)
    {
        if (GetQuirkByDef(def, out Quirk quirk))
        {
            return quirk;
        }
        quirk = new(def, this);
        TryAddQuirk(quirk);
        return quirk;
    }

    public void TryAddQuirk(Quirk quirk)
    {
        List<Quirk> query = (from x in quirks
                             where x.Def == quirk.Def
                             select x).ToList();
        if (query.Count == 0)
        {
            quirks.Add(quirk);
            Log.Message($"Added quirk ${quirk.Def.defName}");
        }
    }
}