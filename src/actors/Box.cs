

using Foster.Framework;

namespace arrow_olympics;

public class Box : Actor {

    public const int HEIGHT = 14;
    public const int WIDTH = HEIGHT;

    public Box() {
        Hitbox = new(new RectInt(0, 0, WIDTH, HEIGHT));
        Mask = Masks.Box;
    }

    public override void Render(Batcher batcher) {
        base.Render(batcher);

        batcher.Rect(new(0, 0, WIDTH, HEIGHT), Color.CornflowerBlue);
    }

}
