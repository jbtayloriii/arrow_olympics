using System.Drawing;
using Foster.Framework;

namespace arrow_olympics;

public class BoxHandler {

    private const int BOX_COUNT = 7;

    private List<Box> boxes = new();

    private readonly ArrowGame game;
    private readonly BoxPattern pattern;

    private float boxTime = 0;



    public BoxHandler(BoxPattern pattern, ArrowGame game) {
        this.game = game;
        this.pattern = pattern;

        // Initialize boxes
        for (int i = 0; i < BOX_COUNT; i++) {
            Point2 position = ArrowGame.BoxAreaStartPoint + new Point2((ArrowGame.BOX_SPACING + Box.WIDTH) * i, 0);

            var box = game.Create<Box>(position);
            boxes.Add(box);
        }
    }


    public void Update() {
        boxTime += game.Time.Delta;
        if (boxTime > pattern.GetDuration()) {
            boxTime -= pattern.GetDuration();
        }

        for (int i = 0; i < boxes.Count; i++) {
            float boxPercent = pattern.GetPositionAtTime(i, boxTime);

            int boxHeight = (int)(ArrowGame.BoxArea.Height * boxPercent);

            // set todo
            boxes[i].Position.Y = ArrowGame.BoxArea.Bottom - boxHeight;
        }
    }
}
