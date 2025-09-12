using Foster.Framework;

namespace arrow_olympics;


class Program {
    public static void Main() {
        using var manager = new Manager();
        manager.Run();
    }
}

class Manager : App {
    public Manager() : base(new AppConfig() {
        ApplicationName = "Arrow Olympics",
        WindowTitle = "Arrow Olympics",
        Width = 1280,
        Height = 720,
        Resizable = true
    }) {

    }

    protected override void Render() {
        // throw new NotImplementedException();
    }

    protected override void Shutdown() {
        // throw new NotImplementedException();
    }

    protected override void Startup() {
        // throw new NotImplementedException();
    }

    protected override void Update() {
        // throw new NotImplementedException();
    }
}
