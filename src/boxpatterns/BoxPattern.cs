namespace arrow_olympics;

public interface BoxPattern {

    /// <summary>
    /// Adds a game time delta to the pattern and returns its vertical percent.
    /// </summary>
    /// <param name="timeDelta"></param>
    /// <returns>The vertical percent of the box</returns>
    public float AddTimeAndGetVerticalPercent(float timeDelta);

    public float PeekPercent(float timeDelta);
}
