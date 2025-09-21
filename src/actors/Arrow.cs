

using Foster.Framework;

namespace arrow_olympics;

public class Arrow : Actor {

    public const int WIDTH = 8;
    public const int HEIGHT = 1;
    public static readonly Color ARROW_COLOR = Color.Gray;
    public static readonly Color ARROW_TIP_COLOR = Color.LightGray;

    private const int SPEED = 400;

    public Player player = Player.NoPlayer;


    public Arrow() {
        Hitbox = new(new RectInt(0, 0, 1, 1));
    }

    public void Init(Player player) {
        this.player = player;
        this.Velocity.X = player == Player.LeftPlayer ? SPEED : -SPEED;
    }

    public override void Update() {
        base.Update();

        if (Position.X > ArrowGame.WIDTH) {
            Game.Destroy(this);
        }

        if (OverlapsFirst(Masks.Box) is Actor hit)
            this.Hit(hit);
    }

    public override void OnPerformHit(Actor by) {
        Game.Destroy(this);
    }

    public override void Render(Batcher batcher) {
        base.Render(batcher);

        // Arrow extends to negative space for left player
        if (player == Player.LeftPlayer) {
            batcher.Rect(-WIDTH, 0, WIDTH, HEIGHT, ARROW_COLOR);
        } else {
            batcher.Rect(0, 0, WIDTH, HEIGHT, ARROW_COLOR);
        }
        batcher.Rect(0, 0, 1, 1, ARROW_TIP_COLOR);
    }

}
