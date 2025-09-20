using Foster.Framework;

namespace arrow_olympics;

public class PlayerController : ShooterController {

    private readonly Shooter shooter;
    private readonly Controls controls;

    public PlayerController(Shooter shooter, Controls controls) {
        this.shooter = shooter;
        this.controls = controls;
    }


    public void Update(ArrowGame game) {
        if (controls.Attack.Down)
            this.shooter.TryShoot();

        this.shooter.SetMove(controls.Move.IntValue.Y);

        // todo: special selection and firing
    }
}
