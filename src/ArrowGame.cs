using Foster.Framework;
using System.Numerics;
using System.Runtime.InteropServices.Swift;

namespace arrow_olympics;

public class ArrowGame {
    public const int WIDTH = 384;
    public const int HEIGHT = 216;

    public const int ARENA_TOP = 8;
    public const int ARENA_BOTTOM = 160;

    // Area where the boxes will float
    public const int BOX_SPACING = 28;
    private const int BOX_AREA_WIDTH = Box.WIDTH * 7 + BOX_SPACING * 6;
    private const int BOX_AREA_HEIGHT = 160;
    public static Point2 BoxAreaStartPoint => new((WIDTH - BOX_AREA_WIDTH) / 2, (HEIGHT - BOX_AREA_HEIGHT) / 2);
    public static RectInt BoxArea => new(BoxAreaStartPoint, BOX_AREA_WIDTH, BOX_AREA_HEIGHT);

    public readonly Manager Manager;

    public readonly Controls Controls;
    private readonly Target screen;
    private readonly Batcher batcher;
    private BoxHandler? boxHandler = null;

    private readonly List<Actor> destroying = [];
    public List<Actor> Actors = [];

    private List<ShooterController> controllers = [];
    public Time Time => Manager.Time;

    private Shooter? leftShooter = null;
    private Shooter? rightShooter = null;

    public ArrowGame(Manager manager) {
        this.Manager = manager;
        this.Controls = new(manager.Input);
        this.batcher = new(manager.GraphicsDevice, name: "gameBatcher");
        this.screen = new(manager.GraphicsDevice, WIDTH, HEIGHT, name: "gameScreen");

        resetObjects();
    }

    private void resetObjects() {
        Actors.Clear();
        controllers.Clear();

        leftShooter = new(new(0, HEIGHT / 2));
        rightShooter = new(new(WIDTH - Shooter.WIDTH, HEIGHT / 2), facingRight: false);
        leftShooter.Game = this;
        rightShooter.Game = this;

        Actors.AddRange([leftShooter, rightShooter]);

        var leftController = new PlayerController(leftShooter, this.Controls);

        // TODO: swap back out for computer controller
        var rightController = new PlayerController(rightShooter, this.Controls);
        // var rightController = new ComputerController(rightShooter);

        controllers.AddRange([leftController, rightController]);

        this.boxHandler = new(new VaryingSinePattern(), this);
    }

    public void Destroy(Actor actor) {
        if (!destroying.Contains(actor))
            destroying.Add(actor);
    }

    public T Create<T>(Point2? position = null) where T : Actor, new() {
        var instance = new T {
            Game = this,
            Position = position ?? Point2.Zero,
        };
        instance.Added();
        Actors.Add(instance);
        return instance;
    }

    public void Update() {
        foreach (ShooterController controller in controllers) {
            controller.Update(this);
        }

        // Remove Destroyed Actors
        for (int i = 0; i < destroying.Count; i++) {
            destroying[i].Destroyed();
            Actors.Remove(destroying[i]);
        }
        destroying.Clear();

        // Update Actors
        for (int i = 0; i < Actors.Count; i++)
            Actors[i].Update();

        // Update boxhandler after actors to set "next" box position, to keep consistent with other actors' behavior
        boxHandler?.Update();
    }

    public void Render(in RectInt viewport) {

        // Draw to buffer
        screen.Clear(0x45283c);
        batcher.Clear();

        // Draw actors
        List<Actor> rendering = new();
        rendering.AddRange(Actors);
        foreach (var actor in rendering) {
            // if (!actor.Visible)
            //     continue;
            batcher.PushMatrix(actor.Position);
            actor.Render(batcher);
            // if (RenderHitboxes) {
            //     actor.RenderHitbox(batcher);
            // }
            batcher.PopMatrix();
        }
        rendering.Clear();

        // Draw some debug borders where shooters are limited from going
        batcher.Rect(new(0, 0, WIDTH, ARENA_TOP), Color.FromHexStringRGB("505050"));
        batcher.Rect(new(0, ARENA_BOTTOM, Shooter.WIDTH, HEIGHT - ARENA_BOTTOM), Color.FromHexStringRGB("505050"));
        batcher.Rect(new(WIDTH - Shooter.WIDTH, ARENA_BOTTOM, Shooter.WIDTH, HEIGHT - ARENA_BOTTOM), Color.FromHexStringRGB("505050"));

        // Render contents to screen
        batcher.Render(screen);
        batcher.Clear();

        // Render screen to Manager window
        var size = viewport.Size;
        var center = viewport.Center;
        var scale = Calc.Min(size.X / (float)screen.Width, size.Y / (float)screen.Height);

        batcher.Image(screen, center, screen.Bounds.Size / 2, Vector2.One * scale, 0, Color.White);
        batcher.Render(Manager.Window);
        batcher.Clear();

    }
}
