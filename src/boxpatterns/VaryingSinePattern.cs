namespace arrow_olympics;

public class VaryingSinePattern : BoxPattern {

    // Amount of time to vary each box, in each direction
    private const float VARYING_AMT = 0.3f;

    // Multiplier to speed up the pattern
    private const float PATTERN_SPEED = 1.4F;

    private const float DURATION = (float)(2 * Math.PI / PATTERN_SPEED);
    public float GetDuration() {
        return DURATION;
    }

    public float GetPositionAtTime(int boxPos, float time) {
        float timeDelta = time + boxPos * VARYING_AMT;
        return (float)((Math.Sin(timeDelta * PATTERN_SPEED) + 1) / 2);
    }
}
