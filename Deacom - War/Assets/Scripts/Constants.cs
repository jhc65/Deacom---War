using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    // Globals
    public static class Globals
    {
        public enum suit { HEARTS, SPADES, DIAMONDS, CLUBS };
        public enum value { ACE = 1, TWO = 2, THREE = 3, FOUR = 4, FIVE = 5, SIX = 6, SEVEN = 7, EIGHT = 8, NINE = 9, TEN = 10, JACK = 11, QUEEN = 12, KING = 13 };
    }

    // Display Text
    public static class DisplayText
    {
        public static string roundWinText = "You won that round!";
        public static string gameWinText = "You won the game!";
        public static string roundLossText = "You lost that round.";
        public static string gameLossText = "You lost the game.";
        public static string tieText = "Tie! It's time for war! (Wars fight automatically)";
    }
}
