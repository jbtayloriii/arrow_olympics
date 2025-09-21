namespace arrow_olympics;

public interface BoxPattern {
    public float GetDuration();

    /// <summary>
    /// Returns the vertical position of a given box at time t.
    /// </summary>
    /// <param name="boxPos">The index of the box, starting at 0</param>
    /// <param name="time">The current time.</param>
    /// <returns>A value from 0 to 1 indicating what percent of the maximum height the box is at.</returns>
    public float GetPositionAtTime(int boxPos, float t);
}
