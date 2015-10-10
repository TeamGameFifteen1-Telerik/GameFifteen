namespace GameFifteen.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Contains public constants (as strings) as sentences to the Player.
    /// </summary>
    public class GameMessages
    {
        public const string Exit = "Are you sure you want to exit?";
        public const string EnterCommand = "Enter command: ";
        public const string EnterNumberToMove = "Enter a number to move: ";
        public const string EnterHow = "For other commands enter \"how\".";
        public const string SolvedByDefault = "Your matrix was solved by default :) Come on - NEXT try";
        public const string Win = "Congratulations! You won the game in {0} moves.";
        public const string EnterYourName = "Please enter your name for the top scoreboard: ";
        public const string NewGameQuestion = "New game?";
        public const string LoadGameQuestion = "Are you sure you want to load your last saved game?";
        public const string RestartGameQuestion = "Are you sure you want to restart?";
        public const string PressKeyToExit = "Press {0} for yes or enter to continue.";        
        public const string GameSaved = "Game saved.";
        public const string Welcome = "Welcome to the game “15”. Try to arrange the numbers sequentially. " + EnterHow;
        public const string Goal = "You're given a grid of 15 numbered tiles and one empty one. Try to arrange the numbers sequentially by moving numbered tiles to the empty space. To move a tile, enter its number.";
        public const string Enter = "Enter: ";

        public static readonly IDictionary<string, string> CommandsDescription = new Dictionary<string, string>
        {
            { "START", "Start a new game" },
            { "HOW", "How to play" },
            { "TOP", "Get top scores" },
            { "GAME", "Go to game screen" },
            { "SAVE", "Save game" },
            { "LOAD", "Load saved game" },
            { "SOLVE", "Solve puzzle" },
            { "RESTART", "Restart game" },
            { "EXIT", "Exit game" },
            { "STYLE={{style}}", "Change game appearance" }
        };

        public static readonly IDictionary<string, string> StyleCommandsDescription = new Dictionary<string, string>
        {
            { "ASTERISK", "Get asterisk border" },
            { "DEFAULT", "Get default border" },
            { "DOTTED", "Get dotted border" },
            { "DOUBLE", "Get double border" },
            { "FAT", "Get fat border" },
            { "MIDDLEFAT", "Get middle fat border" },
            { "SOLID", "Get solid border" },
        };

        public static readonly IDictionary<string, string> MenuOptions = new Dictionary<string, string>()
            {
                { "START", "Start new game" },
                { "HOW", "See game options" },
                { "TOP", "Get top scores" },
                { "EXIT", "Quit" }
            };
    }
}
