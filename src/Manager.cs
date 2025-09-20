using Foster.Framework;

namespace arrow_olympics;


class Program {
    public static void Main() {
        using var manager = new Manager();
        manager.Run();
    }
}

public class Manager : App {

    private ArrowGame? game;

    private readonly Batcher batcher;

    public Manager() : base(new AppConfig() {
        ApplicationName = "Arrow Olympics",
        WindowTitle = "Arrow Olympics",
        Width = 1280,
        Height = 720,
        Resizable = true
    }) {
        batcher = new(GraphicsDevice);
    }

    protected override void Shutdown() {
        // throw new NotImplementedException();
    }

    protected override void Startup() {
        // throw new NotImplementedException();

        game = new ArrowGame(this);

    }

    protected override void Update() {
        if (Input.Keyboard.Pressed(Keys.Escape)) {
            Exit();
        }

        // Reset
        if (Input.Keyboard.Pressed(Keys.F1) && game == null) {
            Startup();
            return;
        }

        game?.Update();
    }

    protected override void Render() {
        // draw the main UI first
        Window.Clear(0x2e1426);
        batcher.Render(Window);

        // draw game on top if it exists
        game?.Render(Window.BoundsInPixels());
    }
}
