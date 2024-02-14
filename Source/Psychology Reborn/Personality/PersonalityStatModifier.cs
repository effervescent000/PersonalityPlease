
using RimWorld;
using System;

namespace Personality;

public class PersonalityStatModifier
{

    private readonly float value;
    public bool isFactor = false;
    private readonly StatDef statDef;
    private readonly float beginsAt = 0.25f;
    private readonly float? maxValueAt;

    //public PersonalityStatModifier(float offset, float factor, stat)
    //{
        
    //}

    public float Value => value;
    public StatDef StatDef => statDef;
    public float BeginsAt => beginsAt;
    public float MaxValueAt
    {
        get
        {
            if (maxValueAt != null)
            {
                return (float)maxValueAt ;
            }
            if (beginsAt > 0.5f) { return 1f; }
            if (beginsAt < 0.5f) { return 0f; }
            throw new Exception($"Malformed PersonalityStatModifier: stat {statDef.defName}");
        }
    }
}
