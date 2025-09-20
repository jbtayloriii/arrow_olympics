using Foster.Framework;

namespace arrow_olympics;

/// <summary>
/// Hitbox can test overlaps between Rectangles
/// </summary>
public readonly struct Hitbox {
    // public enum Shapes {
    //     Rect
    // }

    // public readonly Shapes Shape;
    private readonly RectInt rect;
    // private readonly bool[,]? grid;

    public Hitbox() {
        // Shape = Shapes.Rect;
        rect = new RectInt(0, 0, 0, 0);
    }

    public Hitbox(in RectInt value) {
        // Shape = Shapes.Rect;
        rect = value;
    }

    public bool Overlaps(in RectInt rect)
        => Overlaps(Point2.Zero, new Hitbox(rect));

    public bool Overlaps(in Hitbox other)
        => Overlaps(Point2.Zero, other);

    public bool Overlaps(in Point2 offset, in Hitbox other) {
        return RectToRect(rect + offset, other.rect);
    }

    private static bool RectToRect(in RectInt a, in RectInt b) {
        return a.Overlaps(b);
    }

    public void Render(Batcher batcher) {
        // if (Shape == Shapes.Rect) {
        batcher.RectLine(rect, lineWeight: 1, Color.Blue);
        // }
    }

}
