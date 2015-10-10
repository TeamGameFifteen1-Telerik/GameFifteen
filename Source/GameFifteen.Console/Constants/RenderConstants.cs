namespace GameFifteen.Console
{
    using System;

    using GameFifteen.Common;

    internal class RenderConstants
    {
        internal const int ConsoleWindowWidth = 81;
        internal const int ConsoleWindowHeight = 25;

        internal const int InitialGridX = (ConsoleWindowWidth / 2) - (GlobalConstants.GridSize * 2);
        internal const int InitialGridY = 3;

        internal const int InitialGameOptionsX = 0;
        internal const int InitialGameOptionsY = 0;

        internal const int InitialWelcomeMessageX = 0;
        internal const int InitialWelcomeMessageY = 0;

        internal const int InitialGameOptionsCommandsX = (ConsoleWindowWidth / 2) - 20;

        internal const int GameMessagesY = 11;

        internal const int InitialScoreboardX = (ConsoleWindowWidth / 2) - (GlobalConstants.GridSize * 2);
        internal const int InitialScoreboardY = 0;

        internal const int MenuStartPositionX = 47;
        internal const int MenuStartPositionY = 15;

        internal const string Goal = "Goal: ";
        internal const string Commands = "Commands: ";
        internal const string Scoreboard = "Scoreboard:";
        internal const string NoTopPlayers = "No top players yet.";

        internal const ConsoleColor UserMessagesColor = ConsoleColor.White;
        internal const ConsoleColor GameMessagesColor = ConsoleColor.Yellow;
        internal const ConsoleColor ExplanationsColor = ConsoleColor.Green;
    }
}
