using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {
    // Init random
    static System.Random r = new System.Random();

    // Deck shuffle
    public static List<Card> ShuffleDeck(List<Card> deck)
    {
        // Shuffle!
        for (int i = deck.Count - 1; i > 0; --i) {
            int j = r.Next(i + 1);
            Card temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }

        return deck;
    }
}
