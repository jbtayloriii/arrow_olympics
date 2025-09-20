namespace arrow_olympics;

public class ComputerController : ShooterController {

    private readonly Shooter shooter;

    public ComputerController(Shooter shooter) {
        this.shooter = shooter;
    }

    public void Update(ArrowGame game) {
        // todo: computer logic
    }
}
