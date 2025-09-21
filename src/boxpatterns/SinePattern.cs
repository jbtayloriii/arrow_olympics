namespace arrow_olympics;

public class SinePattern : BoxPattern {

    private const float DURATION = (float)(2 * Math.PI);
    public float GetDuration() {
        return DURATION;
    }

    public float GetPositionAtTime(int boxPos, float time) {
        return (float)((Math.Sin(time) + 1) / 2);
    }
}
