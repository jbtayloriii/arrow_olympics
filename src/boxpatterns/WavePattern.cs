namespace arrow_olympics;

public class WavePattern : BoxPattern {
    private float timeAcc = 0;

    // Multiplier to speed up the pattern
    private const float PATTERN_SPEED = 1.8F;

    private const float PERIOD = (float)(2 * Math.PI / PATTERN_SPEED);


    public float AddTimeAndGetVerticalPercent(float timeDelta) {
        timeAcc += timeDelta;
        if (timeAcc > PERIOD) {
            timeAcc -= PERIOD;
        }
        return (float)((Math.Cos((timeAcc * PATTERN_SPEED) + Math.PI) + 1) / 2);
    }

    public float PeekPercent(float timeDelta) {
        float peekTimeAcc = timeAcc + timeDelta;
        if (peekTimeAcc > PERIOD) {
            peekTimeAcc -= PERIOD;
        }
        return (float)((Math.Cos((peekTimeAcc * PATTERN_SPEED) + Math.PI) + 1) / 2);
    }
}
