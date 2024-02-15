namespace Personality;

public class ModifierValues
{
    private float offset;
    private float factor;

    public ModifierValues(float offset, float factor)
    {
        this.offset = offset;
        this.factor = factor;
    }

    public ModifierValues()
    {
        offset = 0f; factor = 1f;
    }

    public float Offset
    {
        get => offset; set => offset = value;
    }

    public float Factor { get => factor; set => factor = value; }

    public void ResetValues()
    {
        offset = 0f; factor = 1f;
    }

}
