namespace arrow_olympics;

/// <summary>
/// Box pattern that waits a set amount of time before delegating to another pattern.
/// </summary>
public class WaitingPattern : BoxPattern {
    private float waitTime;

    private readonly BoxPattern delegatePattern;

    public WaitingPattern(BoxPattern del, float wait) {
        this.delegatePattern = del;
        this.waitTime = wait;
    }

    public float AddTimeAndGetVerticalPercent(float timeDelta) {
        if (waitTime > 0) {
            waitTime -= timeDelta;
            if (waitTime > 0) {
                return 0;
            } else {
                float excess = -waitTime;
                waitTime = 0;
                return delegatePattern.AddTimeAndGetVerticalPercent(-excess);
            }
        }
        return delegatePattern.AddTimeAndGetVerticalPercent(timeDelta);
    }
}
