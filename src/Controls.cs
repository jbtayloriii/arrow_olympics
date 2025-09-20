
using Foster.Framework;

namespace arrow_olympics;

public class Controls(Input input) {
    public readonly VirtualStick Move = new(input, "Move",
        new StickBindingSet()
            .AddArrowKeys()
            .AddDPad()
            .Add(Axes.LeftX, 0.25f, Axes.LeftY, 0.50f, 0.25f)
    );

    public readonly VirtualAction Special = new(input, "Special",
        new ActionBindingSet()
            .Add(Keys.X)
            .Add(Buttons.South)
    );

    public readonly VirtualAction Attack = new(input, "Attack",
        new ActionBindingSet()
            .Add(Keys.C)
            .Add(Buttons.West)
    );
}
