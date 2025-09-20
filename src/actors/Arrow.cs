

using Foster.Framework;

namespace arrow_olympics;

public class Arrow : Actor {

    public const int WIDTH = 8;
    public const int HEIGHT = 1;
    public static readonly Color ARROW_COLOR = Color.Gray;

    private const int SPEED = 230;

    private bool facingRight = true;


    public Arrow() {
        this.Velocity.X = SPEED;
    }

    public void Init(bool facing) {
        this.facingRight = true;
    }

    public override void Update() {
        base.Update();

        if (Position.X > ArrowGame.WIDTH) {
            Game.Destroy(this);
        }
    }

    public override void Render(Batcher batcher) {
        base.Render(batcher);

        if (facingRight) {
            batcher.Rect(-WIDTH, 0, WIDTH, HEIGHT, ARROW_COLOR);
        } else {
            batcher.Rect(0, 0, WIDTH, HEIGHT, ARROW_COLOR);
        }
    }

}
