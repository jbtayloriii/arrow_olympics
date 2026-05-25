namespace arrow_olympics;

public class WaitingPattern {

    private readonly BoxPattern delegatePattern;

    public WaitingPattern(BoxPattern del) {
        this.delegatePattern = del;
    }

    /// <summary>
    /// Returns the vertical position of a given box at time t.
    /// </summary>
    /// <param name="boxPos">The index of the box, starting at 0</param>
    /// <param name="time">The current time.</param>
    /// <returns>A value from 0 to 1 indicating what percent of the maximum height the box is at.</returns>
    public float GetPositionAtTime(int boxPos, float t) => delegatePattern.GetVerticalPosPercent(boxPos, t);
}
