

using Foster.Framework;

namespace arrow_olympics;

public class Box : Actor {

    public const int HEIGHT = 14;
    public const int WIDTH = HEIGHT;

    private static readonly Color HIT_COLOR = Color.CornflowerBlue;
    private static readonly Color NOT_HIT_COLOR = Color.Blue;

    private bool leftHit = false;
    private bool rightHit = false;

    private Player claimedByPlayer = Player.NoPlayer;
    public Player ClaimedByPlayer => claimedByPlayer;

    public Box() {
        Hitbox = new(new RectInt(0, 0, WIDTH, HEIGHT));
        Mask = Masks.Box;
    }

    public override void OnWasHit(Actor by) {
        if (by is not Arrow arrow) {
            return;
        }

        switch (arrow.player) {
            case Player.LeftPlayer:
                if (leftHit) {
                    claimedByPlayer = Player.LeftPlayer;
                } else {
                    leftHit = true;
                }
                break;
            case Player.RightPlayer:
                if (rightHit) {
                    claimedByPlayer = Player.RightPlayer;
                } else {
                    rightHit = true;
                }
                break;
        }

        base.OnWasHit(by);
    }

    public override void Render(Batcher batcher) {
        base.Render(batcher);

        var leftRect = new RectInt(0, 0, WIDTH / 2, HEIGHT);
        var rightRect = new RectInt(WIDTH / 2, 0, WIDTH / 2, HEIGHT);

        batcher.Rect(leftRect, leftHit ? HIT_COLOR : NOT_HIT_COLOR);
        batcher.Rect(rightRect, rightHit ? HIT_COLOR : NOT_HIT_COLOR);
    }

}
