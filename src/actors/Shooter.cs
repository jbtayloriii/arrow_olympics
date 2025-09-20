

using Foster.Framework;

namespace arrow_olympics;

public class Shooter : Actor {

    public const int HEIGHT = 7;
    public const int WIDTH = 24;

    private const int ACCEL = 350;
    private const int MAX_SPEED = 88;
    private const int FRICTION = 75;

    private const int ARROW_COOLDOWN = 1;

    private readonly bool facingRight;

    private Point2 ArrowOffset => new(facingRight ? WIDTH : -8, HEIGHT / 2);

    // Add arrow width if facing right; hitbox is on right side
    private Point2 ArrowPosition => Position + ArrowOffset + (facingRight ? new(8, 0) : Point2.Zero);

    // mutable state
    private int moveVal = 0;

    private float arrowTimer = 0;

    public Shooter(Point2 startingPoint, bool facingRight = true) {
        this.Position = startingPoint;
        this.facingRight = facingRight;
    }

    public override void Render(Batcher batcher) {
        // TODO: Draw sprite instead of rect
        Rect currentRect = new(0, 0, WIDTH, HEIGHT);
        batcher.Rect(currentRect, Color.Green);

        if (arrowTimer <= 0) {
            batcher.Rect(new(ArrowOffset, 8, 1), Color.LightGray);
        }
    }

    public void TryShoot() {
        if (arrowTimer > 0) {
            return;
        }

        arrowTimer = ARROW_COOLDOWN;
        Game.Create<Arrow>(ArrowPosition);
    }

    public void SetMove(int moveVal) {
        this.moveVal = moveVal;
    }

    public override void Update() {
        base.Update();

        if (arrowTimer > 0) {
            arrowTimer -= Time.Delta;
        }

        // speed
        Velocity.Y += this.moveVal * Time.Delta * ACCEL;

        // max speed
        if (MathF.Abs(Velocity.Y) > MAX_SPEED) {
            Velocity.Y = Calc.Approach(Velocity.Y, MathF.Sign(Velocity.Y) * MAX_SPEED, 2000 * Time.Delta);
        }

        // friction with no input
        if (moveVal == 0) {
            Velocity.Y = Calc.Approach(Velocity.Y, 0, FRICTION * Time.Delta);
        }

        // Clamp to top or bottom of the arena
        if (Position.Y < ArrowGame.ARENA_TOP) {
            Position.Y = ArrowGame.ARENA_TOP;

            // Bounce
            if (Velocity.Y < 0) {
                Velocity.Y = -Velocity.Y;
            }
        } else if (Position.Y > ArrowGame.ARENA_BOTTOM - HEIGHT) {
            Position.Y = ArrowGame.ARENA_BOTTOM - HEIGHT;

            // Bounce
            if (Velocity.Y > 0) {
                Velocity.Y = -Velocity.Y;
            }
        }
    }
}
