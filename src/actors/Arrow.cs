

using Foster.Framework;

namespace arrow_olympics;

public class Arrow : Actor {

    public const int WIDTH = 8;
    public const int HEIGHT = 1;
    public static readonly Color ARROW_COLOR = Color.Gray;
    public static readonly Color ARROW_TIP_COLOR = Color.LightGray;

    private const int SPEED = 400;

    private bool facingRight = true;


    public Arrow() {
        Hitbox = new(new RectInt(0, 0, 1, 1));
    }

    public void Init(bool facing) {
        this.facingRight = facing;
        this.Velocity.X = facingRight ? SPEED : -SPEED;
    }

    public override void Update() {
        base.Update();

        if (Position.X > ArrowGame.WIDTH) {
            Game.Destroy(this);
        }

        if (OverlapsFirst(Masks.Box) is Actor hit)
            hit.Hit(this);
    }

    public override void OnWasHit(Actor by) {
        Game.Destroy(this);
    }

    public override void Render(Batcher batcher) {
        base.Render(batcher);

        if (facingRight) {
            batcher.Rect(-WIDTH, 0, WIDTH, HEIGHT, ARROW_COLOR);
        } else {
            batcher.Rect(0, 0, WIDTH, HEIGHT, ARROW_COLOR);
        }
        batcher.Rect(0, 0, 1, 1, ARROW_TIP_COLOR);
    }

}
