# Arrow Olympics game

A small game in the spirit of UFO 50.

Brainstorming ideas:

The main game has two players, one on the right and one on the left. Each player is represented by an alien that is inside a platform that can move
up or down. Each player's platform also has a giant arrow/harpoon gun attached to it. A fixed number of boxes move up and down with gravity between
the players.

The goal of the game is to "pop" all boxes before the other player does. A player pops a box by hitting it twice with their arrow. If a box is popped,
it despawns for a small amount of time and then respawns. Player hits against a box that has been popped by an opponent will not count; new boxes
must be hit twice by a single player to pop again. This will create some tension between playing offense by targeting boxes that you have not cleared
yet or playing defense by targeting boxes that are about to be popped by an opponent.

Players also have access to a number of powerups in the game. Powerups generally enhance a player's ability to win quickly; however, some powerups
instead hinder opponents.

The single player experience is set in an intergalactic tournament. There are 16 (TBD) representatives from different planets, each with their own
avatar, animations, and platform art. There may also be additional character specific pieces like a locker room or a victory parade if the player
wins the tournament. Each alien planet is also associated with different game attributes (like precision, speed, etc.) and will have powerups
associated with them.

The main game mode is a tournament played first in groups for a round robin, the top 2 of a group of 4 advance to the single elimination bracket.
The bracket is then played out with the player advancing all the way to the final round. Each game is a best of 3 in the round robin, or a best of 5
in the single elimination bracket.

The goal of the game is to have tension by creating different levels of reflex difficulty in performing increasingly hard shots. The player will
generally find that popping boxes closer is easier. Boxes that are farther away are more difficult both because of needing to time one's shots and
because of needing to thread shots to not hit closer boxes.

The computer player should be simulated to feel semi-realistic and difficult but beatable. There will be different knobs that are tunable to both
increase difficulty and to make the computer feel like a human. There's also potential to tune these knobs differently for each alien planet to
further lean into them having identities.

There will be some way to get new powerups. This will likely start as a shop between rounds; however, I don't have a good idea for how to price out
powerups, and I know powerups will not be balanced against each other. One option could be to have coins that spawn bewteen boxes that can be hit by
arrows to either accumulate currency for spending or to give points towards using powerups in the current round (or match).

## Computer AI

The computer AI will simulate human-ish behavior by moving around and being somewhat imperfect with their shots. The core flow will be to have the
computer cycle between idling (where it will move around) and attempting to make a shot on a box that it has not cleared yet. The idling section will
have the computer either simply sit for a predetermined amount of time, or move up/down to a random location. The random location should probably be
normalized as well or have some amount of "flattening" to the computer's movement.

When a computer then moves into their shot aiming phase, they will precompute the locations of boxes some amount of time in the future. If there is a box available for the computer to hit, they will move to the right spot, wait until their aiming timer expires, and then shoot. The computer will generally start at the center of an aiming spot (which may not be a box's full hitbox depending on how many other boxes are blocking it) but will introduce some randomness to show that it is "imperfect". As computer difficulty increases, the computer will aim faster and will have smaller amounts of maximum possible aiming error.

The box calculation for aiming for a computer will be done per-frame and will look X frame in the future. For example, 5 seconds in the future means we will have to look 300 frame ahead. I may need to find a way to more easily simulate where a box will be in X steps as running 7 boxes through hundreds of simulation iterations per frame will be expensive. I could alternatively run two simulations, one in the future and one in the present; this will need to reset when a box is changed (e.g. through an earthquake powerup). Maybe I should not have powerups that affect the boxes?

Once the positions of the boxes are obtained, there will need to be some adjustment for arrow movement (so each box will move an additional number of frames). This will generally be fixed per box, so I can probably just move farther boxes the right number of frames ahead. I'll then need to stack the boxes on top of each other in a sort of one dimensional paper stacking problem to see which box can be hit.

Computer AI knob ideas:

- Determining shots to take by difficulty (e.g. better computers will shoot at targets that are in smaller windows)
- Powerup use, both in time and potentially in strategy. Examples include using the aiming shot to always hit far targets.
- Shot accurancy, represented as a flat randon amount of vertical +/- introduced when a computer determines a shot to take
- Aiming time

## Powerups

TODO
