

using Foster.Framework;

namespace arrow_olympics;

public class Shooter : Actor {

    public const int HEIGHT = 6;
    public const int WIDTH = 24;

    private const int ACCEL = 350;
    private const int MAX_SPEED = 88;
    private const int FRICTION = 75;

    private int moveVal = 0;

    public Shooter(Point2 startingPoint) {
        this.Position = startingPoint;
    }

    public override void Render(Batcher batcher) {
        // TODO: Draw sprite instead of rect
        Rect currentRect = new(0, 0, WIDTH, HEIGHT);
        batcher.Rect(currentRect, Color.Green);

        // todo: draw arrow
    }

    public void TryShoot() {
        // todo: implement
    }

    public void SetMove(int moveVal) {
        this.moveVal = moveVal;
    }

    public override void Update() {
        base.Update();



        // if (MathF.Abs(Velocity.X) > maxspd)
        //     Velocity.X = Calc.Approach(Velocity.X, MathF.Sign(Velocity.X) * maxspd, 2000 * Time.Delta);
        // if (moveVal != 0) {
        //     Console.WriteLine("Moving player controller");
        //     Console.WriteLine($"Direction value: {moveVal}");

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
