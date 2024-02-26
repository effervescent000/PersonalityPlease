using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Personality;

public class SemiClampedValue
{
    public float Min = -1f;
    public float Max = 1f;

    private float value = 0f;

    public SemiClampedValue(float value)
    {
        this.value = value;
    }

    public float OffsetValue(float offset)
    {
        value += offset;
        return Value;
    }

    public float Value => Mathf.Clamp(value, Min, Max);

    public float SetValue(float newValue)
    {
        value = newValue;
        return Value;
    }

    public ref float NakedValue => ref value;
}