using System.Drawing;
using Foster.Framework;

namespace arrow_olympics;

public class BoxesHandler {

    public const int BOX_COUNT = 7;
    private const float BOX_RESPAWN_TIME = 1f;

    public readonly Box?[] boxes = new Box[BOX_COUNT];

    private readonly ArrowGame game;
    private readonly BoxPattern pattern;

    private float boxTime = 0;

    public readonly int[] boxXPositions = [.. from i in Enumerable.Range(0, BOX_COUNT) select (ArrowGame.BOX_SPACING + Box.WIDTH) * i];
    private readonly float[] boxRespawnTimers = [.. from _ in Enumerable.Range(0, BOX_COUNT) select 0f];


    // Tracks boxes captured by players
    // I could use ints with bitmasking to be more efficient, but whatever
    private bool[] leftPlayerBoxes = [.. from _ in Enumerable.Range(0, BOX_COUNT) select false];
    public bool[] LeftPlayerBoxes => leftPlayerBoxes;
    private bool[] rightPlayerBoxes = [.. from _ in Enumerable.Range(0, BOX_COUNT) select false];
    public bool[] RightPlayerBoxes => rightPlayerBoxes;



    public BoxesHandler(BoxPattern pattern, ArrowGame game) {
        this.game = game;
        this.pattern = pattern;

        // Initialize boxes
        for (int i = 0; i < BOX_COUNT; i++) {
            Point2 position = ArrowGame.BoxAreaStartPoint + new Point2(boxXPositions[i], 0);

            var box = game.Create<Box>(position);
            box.boxId = i;
            boxes[i] = box;
        }
    }

    public Player GetWinningPlayer() {
        return Player.NoPlayer;
    }


    public void Update() {
        boxTime += game.Time.Delta;
        if (boxTime > pattern.GetDuration()) {
            boxTime -= pattern.GetDuration();
        }

        // Respawn boxes
        for (int i = 0; i < boxRespawnTimers.Length; i++) {
            if (boxRespawnTimers[i] <= 0) {
                continue;
            }

            boxRespawnTimers[i] -= game.Time.Delta;
            if (boxRespawnTimers[i] <= 0) {
                Point2 position = ArrowGame.BoxAreaStartPoint + new Point2(boxXPositions[i], 0);

                var box = game.Create<Box>(position);
                boxes[i] = box;
            }
        }

        // Destroy or move boxes
        for (int i = 0; i < boxes.Length; i++) {
            var box = boxes[i];
            if (box == null) {
                continue;
            }
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

            float boxPercent = pattern.GetVerticalPosPercent(i, boxTime);

            int boxHeight = (int)(ArrowGame.BoxArea.Height * boxPercent);

            // set todo
            boxes[i]!.Position.Y = ArrowGame.BoxArea.Bottom - boxHeight;
        }
    }
}
