namespace arrow_olympics;

public class ComputerController : ShooterController {

    private const int BOX_TARGET_DELAY_SEC = 5;

    private readonly Shooter shooter;
    private readonly BoxesHandler boxHandler;
    private readonly ArrowGame game;

    private readonly int[] boxFrameLag;

    private float fireDelay = float.MaxValue;

    public ComputerController(Shooter shooter, ArrowGame game, BoxesHandler boxHandler) {
        this.shooter = shooter;
        this.boxHandler = boxHandler;
        this.game = game;

        // Initialize box lag
        boxFrameLag = new int[BoxesHandler.BOX_COUNT];
        for (int i = 0; i < BoxesHandler.BOX_COUNT; i++) {
            int arrowStart = shooter.ArrowPosition.X;
            int targetX = shooter.player == Player.LeftPlayer ? boxHandler.boxXPositions[i] : boxHandler.boxXPositions[i] + Box.WIDTH;
            boxFrameLag[i] = (int)Math.Ceiling((targetX - arrowStart) / (Arrow.SPEED * game.UpdateRatePerSecond));
        }
    }

    public void TargetBox() {
        int boxTarget = 2; // Testing; only target box 2 for now

        // Keep track of lower and upper bounds of where boxes will be
        // We can assume that ranges aren't overlapping because boxes are the
        // same size.
        (float, float)[] boxYPositions = new (float X, float Y)[BoxesHandler.BOX_COUNT];

        // Work outward in, so that overlapping boxes overwrite each other
        for (int i = 0; i < BoxesHandler.BOX_COUNT; i++) {
            if (boxHandler.boxes[i] == null) {
                continue;
            }
            int nextBoxIndex = shooter.player == Player.RightPlayer ? i : BoxesHandler.BOX_COUNT - (i + 1);
            float arrowLag = (float)(boxFrameLag[i] * game.Manager.UpdateMode.FixedTargetTime.TotalSeconds);
            float nextTime = BOX_TARGET_DELAY_SEC + arrowLag;


        }

    }



    public void Update(ArrowGame game) {
        // todo: computer logic
    }
}
