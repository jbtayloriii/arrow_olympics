using System.Drawing;
using Foster.Framework;

namespace arrow_olympics;

public class BoxesHandler {

    public const int BOX_COUNT = 7;
    private const float BOX_RESPAWN_TIME = 1f;

    public readonly Box?[] boxes = new Box[BOX_COUNT];

    private readonly ArrowGame game;


    public readonly int[] boxXPositions = [.. from i in Enumerable.Range(0, BOX_COUNT) select (ArrowGame.BOX_SPACING + Box.WIDTH) * i];
    private readonly float[] boxRespawnTimers = [.. from _ in Enumerable.Range(0, BOX_COUNT) select 0f];


    // Tracks boxes captured by players
    // I could use ints with bitmasking to be more efficient, but whatever
    private bool[] leftPlayerBoxes = [.. from _ in Enumerable.Range(0, BOX_COUNT) select false];
    public bool[] LeftPlayerBoxes => leftPlayerBoxes;
    private bool[] rightPlayerBoxes = [.. from _ in Enumerable.Range(0, BOX_COUNT) select false];
    public bool[] RightPlayerBoxes => rightPlayerBoxes;



    public BoxesHandler(ArrowGame game) {
        this.game = game;

        // Initialize boxes
        for (int i = 0; i < BOX_COUNT; i++) {
            Point2 position = ArrowGame.BoxAreaStartPoint + new Point2(boxXPositions[i], 0);

            var box = new Box(getPattern(i, 0f)) {
                Game = game,
                Position = position,
            };
            game.Register(box);
            boxes[i] = box;
        }
    }

    public Player GetWinningPlayer() {
        return Player.NoPlayer;
    }

    private BoxPattern getPattern(int offset, float startWait) {
        float waitTime = startWait + (0.3f * offset);
        return new WaitingPattern(new WavePattern(), waitTime);
    }



    public void Update() {

        // Respawn boxes
        for (int i = 0; i < boxRespawnTimers.Length; i++) {
            if (boxRespawnTimers[i] <= 0) {
                continue;
            }

            boxRespawnTimers[i] -= game.Time.Delta;
            if (boxRespawnTimers[i] <= 0) {
                Point2 position = ArrowGame.BoxAreaStartPoint + new Point2(boxXPositions[i], 0);

                var box = new Box(getPattern(0, 5.0f)) {
                    Game = game,
                    Position = position,
                };

                game.Register(box);
                boxes[i] = box;
            }
        }

        // Destroy or move boxes
        for (int i = 0; i < boxes.Length; i++) {
            var box = boxes[i];

            // Skip empty boxes
            if (box == null) {
                Console.Write("Skipping box");
                continue;
            }

            // Destroy
            if (box.ClaimedByPlayer != Player.NoPlayer) {
                game.Destroy(box);
                boxes[i] = null;
                boxRespawnTimers[i] = BOX_RESPAWN_TIME;
                if (box.ClaimedByPlayer == Player.LeftPlayer) {
                    leftPlayerBoxes[i] = true;
                } else {
                    rightPlayerBoxes[i] = true;
                }
                continue;
            }

            // Move
            box.UpdatePosition(game.Time.Delta);
        }
    }
}
