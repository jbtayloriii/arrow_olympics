using System.Drawing;
using Foster.Framework;

namespace arrow_olympics;

public class BoxHandler {

    private const int BOX_COUNT = 7;
    private const float BOX_RESPAWN_TIME = 1f;

    private readonly Box?[] boxes = new Box[BOX_COUNT];

    private readonly ArrowGame game;
    private readonly BoxPattern pattern;

    private float boxTime = 0;

    private readonly int[] boxXPositions = new int[BOX_COUNT];
    private float[] boxRespawnTimers = new float[BOX_COUNT] { 0, 0, 0, 0, 0, 0, 0 };



    public BoxHandler(BoxPattern pattern, ArrowGame game) {
        this.game = game;
        this.pattern = pattern;

        // Cache box X positions for respawning
        for (int i = 0; i < BOX_COUNT; i++) {
            this.boxXPositions[i] = (ArrowGame.BOX_SPACING + Box.WIDTH) * i;
        }

        // Initialize boxes
        for (int i = 0; i < BOX_COUNT; i++) {
            Point2 position = ArrowGame.BoxAreaStartPoint + new Point2(boxXPositions[i], 0);

            var box = game.Create<Box>(position);
            boxes[i] = box;
        }
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

            Console.WriteLine($"Depleting box respawn at position {i}");
            boxRespawnTimers[i] -= game.Time.Delta;
            if (boxRespawnTimers[i] <= 0) {
                Point2 position = ArrowGame.BoxAreaStartPoint + new Point2(boxXPositions[i], 0);



                var box = game.Create<Box>(position);
                boxes[i] = box;
                Console.WriteLine($"Respawning box at position {i}");
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
                Console.WriteLine($"box claim: {box.ClaimedByPlayer}");
                Console.WriteLine($"Set respawn timer for {i} to {boxRespawnTimers[i]}");
                continue;
            }

            float boxPercent = pattern.GetPositionAtTime(i, boxTime);

            int boxHeight = (int)(ArrowGame.BoxArea.Height * boxPercent);

            // set todo
            boxes[i]!.Position.Y = ArrowGame.BoxArea.Bottom - boxHeight;
        }
    }
}
