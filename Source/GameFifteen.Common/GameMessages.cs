namespace GameFifteen.Common
{
    using System;
    using System.Linq;

    public class GameMessages
    {
        public const string ExitMessage = "Are you sure you want to exit?";
        public const string EnterNumberMessage = "Enter a number to move: ";
        public const string SolvedByDefaultMessage = "Your matrix was solved by default :) Come on - NEXT try";
        public const string WinMessage = "Congratulations! You won the game in {0} moves.";
        public const string EnterYourNameMessage = "Please enter your name for the top scoreboard: ";
        public const string NewGameQuestion = "New game?";
        public const string LoadGameQuestion = "Are you sure you want to load your last saved game?";
        public const string RestartGameQuestion = "Are you sure you want to restart?";
        public const string PressKeyToExit = "Press {0} for yes or enter to continue.";
        public const string GameSaved = "Game saved.";
        public const string InvalidMove = "Invalid move.";
        public const string NoGameToLoad = "No game was saved.";
        public const string WelcomeMessage = "Welcome to the game “15”. Please try to arrange the numbers sequentially. " +
                            "\nUse 'top' to view the top scoreboard, 'restart' to start a new game and 'exit'" +
                            " \nto quit the game.\n";

        // inital screen
        public const string Enter = "Enter: ";
        public const string GameLogo = @"
 .----------------.  .----------------.  .----------------.  .----------------. 
| .--------------. || .--------------. || .--------------. || .--------------. |
| |    ______    | || |      __      | || | ____    ____ | || |  _________   | |
| |  .' ___  |   | || |     /  \     | || ||_   \  /   _|| || | |_   ___  |  | |
| | / .'   \_|   | || |    / /\ \    | || |  |   \/   |  | || |   | |_  \_|  | |
| | | |    ____  | || |   / ____ \   | || |  | |\  /| |  | || |   |  _|  _   | |
| | \ `.___]  _| | || | _/ /    \ \_ | || | _| |_\/_| |_ | || |  _| |___/ |  | |
| |  `._____.'   | || ||____|  |____|| || ||_____||_____|| || | |_________|  | |
| |              | || |              | || |              | || |              | |
| '--------------' || '--------------' || '--------------' || '--------------' |
 '----------------'  '----------------'  '----------------'  '----------------'
 .----------------.  .----------------. 
| .--------------. || .--------------. |
| |     __       | || |   _______    | |
| |    /  |      | || |  |  _____|   | |
| |    `| |      | || |  | |____     | |
| |     | |      | || |  '_.____''.  | |
| |    _| |_     | || |  | \____) |  | |
| |   |_____|    | || |   \______.'  | |
| |              | || |              | |
| '--------------' || '--------------' |
 '----------------'  '----------------' ";

        public const string HowToPlay = @"
START -> Start new game
HOW -> How to play
TOP -> Get top scores
SAVE -> Save game
LOAD -> Load saved game
SOLVE -> Solve puzzle
STYLE=
      ASTERISK -> Get asterisk border
      DEFAULT -> Get default border
      DOTTED -> Get dotted border
      DOUBLE -> Get double border
      FAT -> Get fat border
      MIDDLEFAT -> Get middle fat border
      SOLID -> Get solid border
RESTART -> Restart game
EXIT -> Exit game
";
    }
}
