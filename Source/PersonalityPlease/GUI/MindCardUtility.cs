using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Personality;

public static class MindCardUtility

{
    //[TweakValue("AAAtest", 1f, 2f)]
    //private static float naturalCertaintyLineHeightMulti = 1.5f;

    //[TweakValue("AAAtest", 0f, 3f)]
    //private static float naturalCertLinePlacementAdjusterMulti = .5f;

    private const float CERTAINTY_HEIGHT = 30f;
    private const float COL_WIDTH = 250f;

    //[TweakValue("AAAtest", 10f, 30f)]
    private const float NODE_HEIGHT = 20f;

    public static Vector2 GetMindCardSize()
    {
        float width = 250f;
        float height = CalculatePersonalityHeight();

        if (ModsConfig.IdeologyActive)
        {
            height += CERTAINTY_HEIGHT;
            width += COL_WIDTH * .35f;
        }

        // this can't use the Settings setting b/c OnStartup hasn't initialized when this is called
        if (ModsConfig.IsActive("effervescent.personalityplease.lovin"))
        {
            width += 250f;
        }

        // add a little extra for padding
        return new Vector2(width * 1.2f, height * 1.2f);
    }

    public static void DrawMindCard(Pawn pawn, Vector2 size)
    {
        MindComp mind = pawn.GetComp<MindComp>();

        if (mind == null) { return; }

        Rect mainRect = new Rect(0f, 0f, size.x, size.y).ContractedBy(10f);
        float xStart = mainRect.x;

        Widgets.BeginGroup(mainRect);

        float personalityHeight = CalculatePersonalityHeight();
        Rect personalitySectionRect = UIComponents.DrawSection(mainRect.x, ModsConfig.IdeologyActive ? mainRect.y + CERTAINTY_HEIGHT + 10f : mainRect.y, COL_WIDTH, personalityHeight);

        DrawPersonality(mind, personalitySectionRect);

        // update xStart
        xStart += personalitySectionRect.width;

        if (ModsConfig.IdeologyActive)
        {
            Rect certaintyRect = new(mainRect.x, mainRect.y, COL_WIDTH, CERTAINTY_HEIGHT);
            DrawCertaintyBar(pawn, mind, certaintyRect);

            IdeoProfileComp ideoProfileComp = Current.Game.GetComponent<IdeoProfileComp>();

            Rect ideoRect = new(personalitySectionRect.xMax + 20f, personalitySectionRect.y, COL_WIDTH * .35f, personalityHeight);
            DrawIdeoProfile(ideoProfileComp.GetProfileFor(pawn.Ideo), ideoRect.ContractedBy(10f));

            Rect ideoIconRect = new(ideoRect.center.x - certaintyRect.height * .5f, certaintyRect.y, certaintyRect.height, certaintyRect.height);
            pawn.Ideo.DrawIcon(ideoIconRect);
            xStart = ideoRect.xMax + 20f;
        }

        Rect quirksRect = new(xStart, mainRect.y, 100f, mainRect.height * 0.5f);
        DrawQuirks(mind, quirksRect);
        xStart += quirksRect.width;

        if (Settings.LovinModuleActive)
        {
            Rect romanceRect = new(xStart, mainRect.y, COL_WIDTH, 100f);
            DrawRomance(romanceRect, pawn);
        }

        Widgets.EndGroup();
    }

    public static void DrawQuirks(MindComp mind, Rect quirksRect)
    {
        float yStart = quirksRect.y;
        Text.Font = GameFont.Medium;
        string header = "PP.QuirkHeader".Translate();
        float headerHeight = Text.CalcHeight(header, quirksRect.width);
        Rect headerRect = new(quirksRect.x, yStart, quirksRect.width, headerHeight);
        Widgets.Label(headerRect, header);
        yStart += headerRect.height + 5f;
        Text.Font = GameFont.Small;
        foreach (var quirk in mind.Quirks)
        {
            string label = quirk.GetLabel;
            if (label != null)
            {
                var height = Text.CalcHeight(label, quirksRect.width);
                Rect localRect = new(quirksRect.x, yStart, quirksRect.width, height);
                Widgets.Label(localRect, label);
                yStart += height + 3f;
            }
        }
    }

    public static void DrawRomance(Rect rect, Pawn pawn)
    {
        return;
    }

    public static void DrawCertaintyBar(Pawn pawn, MindComp comp, Rect rect)
    {
        Widgets.DrawBoxSolid(rect, Color.grey);
        Rect barRect = rect.ContractedBy(3f);
        Widgets.FillableBar(barRect, pawn.ideo.Certainty);
        DrawNaturalCertainty(comp, barRect);
        TooltipHandler.TipRegion(rect, () => GetCertaintyTipText(pawn, comp), 24521558);
    }

    public static string GetCertaintyTipText(Pawn pawn, MindComp mind)
    {
        string tip = "NaturalCertaintyGlobal".Translate()
            + "\n\n"
            + "NaturalCertaintyOfPawn".Translate(mind.IdeoFeelings.NaturalCertainty.ToStringPercent(), pawn.Named("PAWN"))
            + "\n\n"
            + "CertaintyChange".Translate(pawn.Named("PAWN"));

        return tip;
    }

    public static void DrawNaturalCertainty(MindComp comp, Rect certaintyRect)
    {
        float lineHeight = certaintyRect.height * 1.5f;
        Widgets.DrawLineVertical(certaintyRect.x + comp.IdeoFeelings.NaturalCertainty * certaintyRect.width, certaintyRect.y - lineHeight * .15f, lineHeight);
    }

    public static void DrawIdeoProfile(IdeoProfile profile, Rect rect)
    {
        int i = 0;
        Text.Font = GameFont.Tiny;
        foreach (KeyValuePair<string, SemiClampedValue> kvp in profile.Values)
        {
            float nodeHeight = rect.height * (float)(1f / profile.Values.Count);
            Rect nodeRect = new(rect.x, rect.y + (nodeHeight * i) - 5f, rect.width, nodeHeight);
            Rect lineRect = new(nodeRect.x, nodeRect.y - nodeHeight * .2f, nodeRect.width, nodeRect.height);
            UIComponents.DrawLineWithIndicator(lineRect, (kvp.Value.Value + 1) / 2f, text: kvp.Value.Value.ToString());

            i++;
        }
    }

    public static void DrawPersonality(MindComp mind, Rect rect)
    {
        List<PersonalityNode> nodes = mind.nodes.Values.ToList();
        for (int i = 0; i < nodes.Count; i++)
        {
            Text.Font = GameFont.Small;
            float nodeHeight = rect.height * (float)(1f / nodes.Count);
            Rect nodeRect = new(rect.x, rect.y + (nodeHeight * i) + 5f, rect.width, nodeHeight);
            Rect labelRect = new(nodeRect.x, nodeRect.y, nodeRect.width * .55f, nodeRect.height);
            Widgets.Label(labelRect, nodes[i].def.label.Translate());

            Text.Font = GameFont.Tiny;
            Rect lineRect = new(nodeRect.width * .6f, nodeRect.y - nodeHeight * .2f, nodeRect.width * .45f, nodeRect.height);
            UIComponents.DrawLineWithIndicator(lineRect, (nodes[i].FinalRating.Value + 1) / 2f, text: nodes[i].FinalRating.Value.ToString());
        }
    }

    public static float CalculatePersonalityHeight()
    {
        float height = 0;
        foreach (PersonalityNodeDef node in PersonalityHelper.GetAll)
        {
            height += NODE_HEIGHT * 1.5f;
        }
        return height;
    }
}