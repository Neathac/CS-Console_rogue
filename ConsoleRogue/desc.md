# The Console RogueLike
## About

The game is meant to provide a dungeon-crawler rogue-like experience. The player spawns on a randomly generated map with a random amount of upgrade packs scattered around the map along with a proportional amount of enemies. A single exit is placed on each map that spawns the player on a newly generated map with stronger enemies.

## Control scheme

 - Arrows - Simple movement by 1 tile in appropriate direction
 - Space - Player action on an object within a range of 2 tiles in any direction. This action can be an attack, picking up upgrades or using the exit into a new level.

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

## The Customizables folder
### Pallete

Using the object **RLColor**, we are able to pre-specify the colours we want our individual elements to be using. The Pallete.cs simply contains their individual definitions generated using "https://paletton.com/#uid=75C1g0knal7dVqXiCohrqisvHfx". These colours were used for menus.

The class has no functions nor does it take any arguments.

### ObjectColoring

To ease customization of individual elements, specific colour values are only assigned within the ObjectColoring.cs. Changes to values here will appear everywhere.

The class has no functions nor does it take any arguments.

## The Components folder
### Map
#### Tileset

This class extends **Map** defined in the RogueSharp package. This object provides us with a sandbox environment for the game area. Namely, it allows us to use the "Cell" object properly.

The package RogueSharp provides us with the class **Cell**. This class allows us to treat the individual parts of the map we will be rendering with greater degree of specificity, such as information about visibility within the player field of view, whether the cell had ever been seen in the first place and whether the player object is allowed to pass it, as well as access individual coordinates of these cells within the Map object.

The class **Tileset** itself is used as a rendering template for individual Cells. It specifies the placeholder characters, their colours and whether they should be rendered at all or not.

##### Tileset Functions and arguments

- Draw( RLConsole console) draws every cell of the map into the console given as an argument
- SetConsoleSymbolForCell( RLConsole, Cell ) decides if and if so then how the individual cell should be rendered
