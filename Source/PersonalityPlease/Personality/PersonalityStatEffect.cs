using RimWorld;
using System;
using Verse;

namespace Personality;

public class PersonalityStatEffect
{
    private readonly float value;
    public bool isFactor = false;
    private readonly StatDef statDef;
    private readonly float beginsAt = 0.25f;
    private readonly float? maxValueAt;

    public float Value => value;
    public StatDef StatDef => statDef;
    public float BeginsAt => beginsAt;

    public float MaxValueAt
    {
        get
        {
            if (maxValueAt != null)
            {
                return (float)maxValueAt;
            }
            if (beginsAt > 0f) { return 1f; }
            if (beginsAt < 0f) { return -1f; }
            throw new Exception($"Malformed PersonalityStatModifier: stat {statDef.defName}");
        }
    }

    public float GetValueAt(float targetValue)
    {
        SimpleCurve curve = new()
        {
            new CurvePoint(MaxValueAt, value),
            new CurvePoint(BeginsAt, isFactor ? 1f : 0f),
            new CurvePoint(-MaxValueAt, isFactor ? 1f : 0f)
        };
        return curve.Evaluate(targetValue);
    }
}