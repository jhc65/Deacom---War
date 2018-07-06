using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    #region Variables and Declarations
    [SerializeField] private List<Card> deck = new List<Card>();

    #region Getters and Setters
    public int Size {
        get { return deck.Count; }
    }

    public Card AddCard {
        set { deck.Add(value); }
    }

    public List<Card> Deck_GS {
        get { return deck; }
        set { deck = value; }
    }

    public Card GetCard(int cardToGet)
    {
        return deck[cardToGet];
    }
    #endregion
    #endregion

    #region Custom Methods
    public void RemoveCard(int posToRemove)
    {
        deck.RemoveAt(posToRemove);
    }

    public void PutCardsOnBottomOfDeck(int cardsOffTop, List<Card> cardsFromOpponent = null)
    {
        if (cardsFromOpponent != null) {
            for (int i = 0; i < cardsOffTop; i++) {
                deck.Add(deck[i]);
                RemoveCard(i);
            }

            for (int i = 0; i < cardsOffTop; i++) {
                deck.Add(cardsFromOpponent[i]);
            }
        }
        else {
            for (int i = 0; i < cardsOffTop; i++) {
                deck.Add(deck[i]);
                RemoveCard(i);
            }
        }
    }
    #endregion

    #region Unity Overrides
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion
}
