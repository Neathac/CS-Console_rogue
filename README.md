# The Console RogueLike
## About

The game is meant to provide a dungeon-crawler rogue-like experience. The player spawns on a randomly generated map with a random amount of upgrade packs scattered around the map along with a proportional amount of enemies. A single exit is placed on each map that spawns the player on a newly generated map with stronger enemies.

### Control scheme

 - Arrows - Simple movement by 1 tile in appropriate direction
 - Space - Player action on an object within a range of 2 tiles in any direction. This action can be an attack, picking up upgrades or using the exit into a new level.

### Mechanics

#### Stats

 - Both goblins and the player have health, attack, defence, luck and agility.
 - Agility is used as a sort of timer that dictates frequency of movement relative to the agility of player.
 - Health can be restored and maximum health can be increased. These events always happen simultaneously. Once health reaches value lesser than 1, the entity dies, player or not.
 - Attack determines the amount of damage dealt
 - Defence lessens the amount of damage dealt by an attack
 - Luck is a random event that doubles either the amount of damage dealt or the amount of damage blocked. Both of these events can occur during a single attack at once and both have a chance of 5% separately. Goblins use the luck stat too.

#### Action

 - Movement, attack or picking up an item is considered an action if performed by the player. Each action prompts every entity capable of acting in some way with the player's agility.
 - When prompted and provided with the player agility - being a positive integer - an internal counter is increased. If this counter surpasses entity's own agility, the entity subtracts own agility from the counter and performs an action.
 - While the player is shown an increasing agility number, in reality the agility is lessening with each upgrade. Same goes for enemies as the levels increase.

## Dependencies
The following packages were installed:
 - RLNET - version 1.0.6
 - RogueSharp - version 4.2.0

The .NET framework also misses the following requirements:
 - System.Drawing.Common - version 5.0.2
 - Microsoft.Win32.SystemEvents - version 5.0.0

_All of the above are direct names of NuGet packages_

## The driver code - Program.cs

The displayed elements will be nested within **RLConsole** objects, where multiple such objects will be nested within a root RLConsole object. This root console has continual event and rendering update functions, which manage the individual RLConsole updates. Overall, the screen is split into consoles for map, inventory, messages used to describe combat and a window displaying the stats of the player.

Along with this, the driver code is responsible for holding the Player object, handling input (passing appropriate input to appropriate handlers rather) and maintanance of current map (Tileset), messages and stats management objects.

### Functions and important variables

As stated above, Program.cs is the entry point of our program. It is responsible for maintanance of the following **objects and variables**:
 - Sizes of consoles - Positive integers with a very straightforward purpose. They are simply used as size parameters for individual consoles.
 - RLConsoles - Several static consoles used to display varying information. Splitting into different consoles serves as both visual and logical separation of displayed information. Every used console is managed by a parent rootConsole.
 - Tileset - A class of type RogueSharp.Map. This class allows us to use the field of view features, specify rendering of individual map cells and access map elements via coordinates. For more information, see below.
 - Messenger - A class used to interact with the console used to display messages.
 - MovementDriver - A player input handler class.

It also uses the following **functions**:

- Main() - The program entry point. it sets up the necessary variables and performs the first wave of renders.
- newLevel() - Using the Generator.cs class, it populates lists of enemy and powerup entities and initializes Tileset.cs class. In short, this method provides us with newly generated map, enemies and powerups.
- onRootConsoleUpdate() - Responsible for surface-level input handling, due to its frequent execution.
- onRootConsoleRender() - Responsible primarily for graphically updating what is necessary.

## The Customizables folder
### Pallete.cs

Using the object **RLColor**, we are able to pre-specify the colours we want our individual elements to be using. The Pallete.cs simply contains their individual definitions generated using "https://paletton.com/#uid=75C1g0knal7dVqXiCohrqisvHfx". These colours were used for menus.

The class has no functions nor does it take any arguments.

### ObjectColoring.cs

To ease customization of individual elements, specific colour values are only assigned within the ObjectColoring.cs. Changes to values here will appear everywhere.

The class has no functions nor does it take any arguments.

### Messages.cs

A class responsible for composing messages for the **messageRLconsole**. At the moment, it can compose messages for dealing and receiving damage, finding of upgrades or the destruction of actors. No complex functions are included, the class contains just simple switches and strings.

## The Components folder
### Map
#### Tileset.cs

This class extends **Map** defined in the RogueSharp package. This object provides us with a sandbox environment for the game area. Namely, it allows us to use the "Cell" object properly.

The package RogueSharp provides us with the class **Cell**. This class allows us to treat the individual parts of the map we will be rendering with greater degree of specificity, such as information about visibility within the player field of view, whether the cell had ever been seen in the first place and whether the player object is allowed to pass it, as well as access individual coordinates of these cells within the Map object.

The class **Tileset** itself is used as a rendering template for individual Cells. It specifies the placeholder characters, their colours and whether they should be rendered at all or not.

##### Tileset Functions and arguments

- Draw( RLConsole console) - Draws every cell of the map into the console given as an argument.
- SetConsoleSymbolForCell( RLConsole, Cell ) - Decides if and if so then how the individual cell should be rendered.
- set/removeGoblin/Pack() - The class is responsible for keeping track of placement and possible removal of enemy/upgrade(pack) entities from the map.
 - Note that exit into the next level is treated as a kind of upgrade pack.
- updatePlayerVisibility() - Takes advantage of the field of view functionality provided by RogueSharp package. It is used to change what the player is able to see when called.
- newActorPosition() - Changes position of the provided actor on the map to provided coordinates.
- getNearbyEntity() - Returns an Actor that is in field of view and within a range of 2. It first checks for nearby goblins, then upgrades and finally an exit. In case an Actor is found early, it won't bother finding the rest.
- getRelativeDirs() - The function finds out in which direction the first Cell is in relation to the second. It returns a direction specified in an enum MovementDirs.cs

### Actors

The folder contains definitions of **Actor, Player, Pack, Enemy, and Goblin**. There is little to say to these classes, except that they provide us with functions for specifying the stats of these objects and self-rendering, as well as manipulating a simple movement system. For details on movement, see "About" section in "Mechanics".

### Drivers

#### Messenger.cs

Another very simple class. It simply holds and swaps out the messages displayed in the provided console.

#### MovementDriver.cs

**Functions**:
 - mapKeyToDir() - A simple switch statement mapping input keys to enum values.
 - movePlayer() - The name suggests the original intention, but it can be used to move any provided actor in any provided direction by 1 cell.
 - goblinAction() - Perhaps the most complicated function of the class, it first updates the internal movement counter of provided goblin and then - if the goblin is to move - moves it in the direction of the player. Modular arithmetic gives the goblins non-uniform movement. They will first try to align with the player either vertically or horizontally, after which they will try to reach the player. If the goblin reaches the player, the player receives damage instead. The movement is done so that goblins don't get stuck on corners. It complements the often narrow corridors of the map well.
 - areAdjecent() - finds if two actors are next to each other in the specified direction.

#### Statistics.cs

The class resolves combat math and player actions - see Mechanics section.

## Interfaces folder

Contains interfaces for a drawable entity and for an actor. Defines attributes these objects would require.

## Misc_Globals folder

Contains only enums. Namely kinds of upgrades, directions, and possible events.
