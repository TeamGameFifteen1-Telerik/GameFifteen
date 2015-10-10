Refactoring Documentation for Project “Game Fifteen"                                                                                                                          
------------------------------------------------------

1.  Redesigned the project structure: **Team “Game-Fifteen-1”**
	- Renamed the solution to *GameFiteen*.
	- Created separate projects to separate the logic: *GameFifteen.Common*, *GameFifteen.Console*, *GameFifteen.Logic*, *GameFifteen.Models*.
	- Renamed classes: 
		- *Gameplay* to *Engine*
		- *MatrixGenerator* to *Grid*
		- *Program* to *GameFifteenMain*
	- Moved classes:
		-  *Engine*, *Command* to project *GameFifteen.Logic*
		-  *Player*, *Tile*, *Grid*, *Scoreboard* to *GameFifteen.Models*
		-  *GameFifteenMain* to *GameFifteen.Console*
	- *Command* class turned into *Command* enum. Method *CommandType()* moved from class *Command* to *Engine*
	- Class *Grid* and class *Scoreboard* are no longer static.
2.  Introduced new classes:
	- *ConsoleRenderer* in project *GameFifteen.Console*
		- Method *PrintScoreboard()* moved from *Scoreboard* to *ConsoleRenderer*
		- Method *PrintMatrix()* moved from *Grid* to *ConsoleRenderer*
	- *ConsoleInterface* in project *GameFifteen.Console*
	- *GlobalConstants* in project *GameFifteen.Common*	
		- Global constants moved to *GlobalConstants* class.
3.  Redesigned project logic:
	- Class *Grid*:
		- Introduced new properties *EmptyTile* and *IsSorted*
		- Introduced new field *tiles* to hold all tiles
		- Refactored methods:
			- *GenerateMatrix()* (renamed to *Initialize()*) - removed new List of tiles initialization, made method void, renamed variables;
			- *ShuffleMatrix()* (renamed to *Shuffle()*) - moved random initialization to the static constructor, removed new matrix initialization, made method void, renamed variables;
			- *MoveFreeTile()* (renamed to *MoveEmptyTileRandomly()*) - extracted logic to separate methods (*SwapTiles()* and (*GetNeighbours()*); 
			- *GenerateNeighbourTilesList()* (renamed to *GetNeighbours()*) - now returns a full list of valid neighbours; 
			- *DetermineFreeTile()* (renamed to *GetEmptyTile()*) - removed unneeded variable initialization, uses new field *tiles*; 
			- *AreValidNeighbourTiles()* (renamed to *ValidateNeighbour()*) - conditions assigned to variables to make code more readable.
		- Extracted methods *GetTilePosition()*, *GetTileAtPosition()*, *GetTileFromLable()*, *CheckIfSorted()*, *SwapTiles()*			
	- Class *Engine*
		- Removed methods *GetFreeTilePosition()*, *AreValidNeighbourTiles()*, *TilePositionValidation()*, *GetDestinationTilePosition()* - logic moved to class *Grid*
		- Refactored method *IsMatrixSolved()*
		- Method *MoveTiles()* moved from class *Engine* to class *Grid* and refactored (variables renamed, methods extracted)
	- Class *GameFifteenMain*:
		- Logic from method *Menu* moved to method Run in class *Engine*
	- Added interfaces:
		- *IRenderer* to use in class *Engine*
		- *IUserInterface* for user input, to use in class *Engine*
4.	Implemented Pattens
	- Creation
		- Singleton [link](https://github.com/TeamGameFifteen1-Telerik/GameFifteen/blob/master/Source/GameFifteen.Models/Scoreboard.cs), GameFifteenStarter
		- Prototype [link](https://github.com/TeamGameFifteen1-Telerik/GameFifteen/blob/master/Source/GameFifteen.Models/Tile.cs#L69)
		- Simple Factory - GridStyleFactory Enum.
		- Object Pool - Werehouse Bindings.cs
	- Structural
		- Facaade - GameFifteenStarter.cs and GameFifteenMain.cs
		- Decorator concrete decorator GridWithBorder.cs,  abstract decorator Decorator.cs 
		- Bridge between IGameInitializer and IEngine (engine.cs)
	- Behaivor
		- Memento [link](https://github.com/TeamGameFifteen1-Telerik/GameFifteen/blob/master/Source/GameFifteen.Models/Grid.cs#L105)
		- Strategy - StandartFifteenTilesEngine.ctor() IRenderer renderer, IUserInterface userInterface
		- Iterator - Grid.cs  IEnumerator GetEnumerator(), Foreach implements iterator 
		
		S - GameFifteenMain
		O - EveryWhere , add style
		L - Styles
		I - Logic.Contracts 
		D - (Ninject) loosely-coupled, highly-cohesive https://en.wikipedia.org/wiki/Dependency_injection#Advantages
		
		__if game logic changes to more tiles and grid size is bigger it still works.__
		
Sample Refactoring Documentation for Project “Game 15”                                                                                                                          
------------------------------------------------------

1.  Redesigned the project structure: Team “…”
	-   Renamed the project to `Game-15`.
	-   Renamed the main class `Program` to `GameFifteen`.
	-   Extracted each class in a separate file with a good name: `GameFifteen.cs`, `Board.cs`, `Point.cs`.
	-   …
2.  Reformatted the source code:
	-   Removed all unneeded empty lines, e.g. in the method `PlayGame()`.
	-   Inserted empty lines between the methods.
	-   Split the lines containing several statements into several simple lines, e.g.:
	
	Before:
	
		if (input\[i\] != ' ') break;
		
	After:

		if (input\[i\] != ' ')
		{
			break;
		}
	
	-   Formatted the curly braces **{** and **}** according to the best practices for the C\# language.
	-   Put **{** and **}** after all conditionals and loops (when missing).
	-   Character casing: variables and fields made **camelCase**; types and methods made **PascalCase**.
	-   Formatted all other elements of the source code according to the best practices introduced in the course “[High-Quality Programming Code](http://telerikacademy.com/Courses/Courses/Details/244)”.
	-   …
3.  Renamed variables:
	-   In class `Fifteen`: `number` to `numberOfMoves`.
	-   In `Main(string\[\] args)`: `g` to `gameFifteen`.
4.  Introduced constants:
	-   `GAME\_BOARD\_SIZE = 4`
	-   `SCORE\_BOARD\_SIZE = 5`. 
5.  Extracted the method `GenerateRandomGame()` from the method `Main()`.
6.  Introduced class `ScoreBoard` and moved all related functionality in it.
7.  Moved method `GenerateRandomNumber(int start, int end)` to separate class `RandomUtils`.
8.  …