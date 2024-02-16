﻿using System;
using System.Collections.Generic;
using Verse;

namespace Personality;

public class Mind : IExposable
{
    private readonly Pawn pawn;
    public Dictionary<string, PersonalityNode> nodes;

    public Mind(Pawn pawn)
    {
        this.pawn = pawn;
    }

    public void Initialize()
    {
        Initialize(pawn);
    }

    public void Initialize(Pawn pawn)
    {
        nodes = new Dictionary<string, PersonalityNode>();
        foreach (PersonalityNodeDef def in DefDatabase<PersonalityNodeDef>.AllDefsListForReading)
        {
            nodes.Add(def.defName, new PersonalityNode(def, pawn));
        }

        int seed = PersonalityHelper.PawnSeed(pawn);
        Random random = new(seed);
        
        
        foreach (PersonalityNode node in nodes.Values)
        {
            if (node.BaseRating < -1)
            {
                float r = random.Next(-100, 100);
                node.BaseRating = r / 100f;
            }
        }
    }

    public void ApplyAdjustments()
    {
        ApplyAdjustments(pawn);
    }

    public void ApplyAdjustments(Pawn pawn) {
        foreach (PersonalityNode node in nodes.Values)
        {
            node.ModifyRating(pawn);
        }
    }

    public void ExposeData()
    {
        Scribe_Collections.Look(ref nodes, "personality", LookMode.Value, LookMode.Deep);
    }

}