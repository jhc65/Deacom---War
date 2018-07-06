using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    #region Variables and Declarations
    [SerializeField] private Deck gameDeck;
    [SerializeField] private Deck playerDeck;
    [SerializeField] private Deck opponentDeck;

    [SerializeField] private Image opponentCardImage;
    [SerializeField] private Image playerCardImage;

    [SerializeField] private Text winnerTextBox;
    [SerializeField] private Text playerCardCountTextBox;

    [SerializeField] private Button nextCardButton;
    [SerializeField] private Button fightWarButton;
    [SerializeField] private Button newGameButton;

    [SerializeField] private int playerCount = 0;
    [SerializeField] private int opponentCount = 0;
    [SerializeField] private int totalCount = 0;

    private int cardFromEachDeck = 0;
    private bool shouldFightWar = false;
    private bool isGameOver = false;
    private bool isCoroutineRunning = false;
    #endregion

    #region Custom Methods
    public void SetupGame()
    {
        nextCardButton.gameObject.SetActive(true);
        fightWarButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(false);

        // Shuffle Deck and Split it between player and computer
        for (int i = 0; i < 5; i++) {
            gameDeck.Deck_GS = Utilities.ShuffleDeck(gameDeck.Deck_GS);
        }

        // Alternate between decks to further "shuffle" the deck
        for (int i = 0; i < 52; i++) {
            if ((i % 2 == 0)) {
                playerDeck.AddCard = gameDeck.GetCard(i);
            }
            else {
                opponentDeck.AddCard = gameDeck.GetCard(i);
            }
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void NextCard_Button()
    {
        if (!isCoroutineRunning) {
            isCoroutineRunning = true;
            StartCoroutine(NextCard());
        }
    }

    IEnumerator NextCard()
    {
        int cardPos = 0;
        // Deal the Cards for both players
        opponentCardImage.GetComponent<Image>().color = Color.white;
        playerCardImage.GetComponent<Image>().color = Color.white;
        opponentCardImage.GetComponent<Image>().sprite = opponentDeck.GetCard(cardPos).GetComponent<Image>().sprite;
        playerCardImage.GetComponent<Image>().sprite = playerDeck.GetCard(cardPos).GetComponent<Image>().sprite;

        cardFromEachDeck = 1;

        // Check the play and see who won
        int winner = CheckForPlayerWinRound(cardPos);

        // Fight in a war or give cards to winner
        while (winner == 2) {
            // Show winner and swap visible buttons
            winnerTextBox.text = Constants.DisplayText.tieText;
            nextCardButton.GetComponent<Button>().interactable = false;
            cardPos += 2;

            shouldFightWar = false;
            yield return new WaitForSeconds(1.5f);
            nextCardButton.GetComponent<Button>().interactable = true;
            opponentCardImage.GetComponent<Image>().sprite = opponentDeck.GetCard(cardPos).GetComponent<Image>().sprite;
            playerCardImage.GetComponent<Image>().sprite = playerDeck.GetCard(cardPos).GetComponent<Image>().sprite;

            cardFromEachDeck += 2;

            winner = CheckForPlayerWinRound(cardPos);
        }

        if (winner == 0) {
            winnerTextBox.text = Constants.DisplayText.roundLossText;

            List<Card> playerToOpponent = new List<Card>();
            for (int i = 0; i < cardFromEachDeck; i++) {
                playerToOpponent.Add(playerDeck.GetCard(i));
                playerDeck.RemoveCard(i);
            }

            opponentDeck.PutCardsOnBottomOfDeck(cardFromEachDeck, playerToOpponent);
        }
        else if (winner == 1) {
            winnerTextBox.text = Constants.DisplayText.roundWinText;

            List<Card> opponentToPlayer = new List<Card>();
            for (int i = 0; i < cardFromEachDeck; i++) {
                opponentToPlayer.Add(opponentDeck.GetCard(i));
                opponentDeck.RemoveCard(i);
            }

            playerDeck.PutCardsOnBottomOfDeck(cardFromEachDeck, opponentToPlayer);
        }

        // Reset stuff that needeth reset
        shouldFightWar = false;
        cardPos = 0;

        // Check if there are moves left. If not, gray out the button and display a winner
        isGameOver = CheckForGameOver();

        isCoroutineRunning = false;
    }

    private int CheckForPlayerWinRound(int cardPos)
    {
        // return 0 for player loss, 1 for player win, or 2 for a tie
        if (playerDeck.GetCard(cardPos).value == opponentDeck.GetCard(cardPos).value) {
            return 2;
        }
        else if (playerDeck.GetCard(cardPos).value == Constants.Globals.value.ACE || playerDeck.GetCard(cardPos).value > opponentDeck.GetCard(cardPos).value) {
            return 1;
        }
        else {
            return 0;
        }
    }

    private bool CheckForGameOver()
    {
        // Return false if there are cards left to deal, or true if one player has all of the cards
        if (playerDeck.Size == 52) {
            return true;
        }
        else if (opponentDeck.Size == 52) {
            return true;
        }

        return false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Unity Overrides
    // Use this for initialization
    void Start()
    {
        SetupGame();
    }

    // Update is called once per frame
    void Update()
    {
        playerCardCountTextBox.text = playerDeck.Size.ToString() + " Cards";

        playerCount = playerDeck.Size;
        opponentCount = opponentDeck.Size;
        totalCount = (playerCount + opponentCount);

        if (isGameOver) {
            nextCardButton.gameObject.SetActive(false);
            fightWarButton.gameObject.SetActive(false);
            newGameButton.gameObject.SetActive(true);

            if (opponentDeck.Size == 52) {
                winnerTextBox.text = Constants.DisplayText.gameLossText;
            }
            else if (playerDeck.Size == 52) {
                winnerTextBox.text = Constants.DisplayText.gameWinText;
            }
        }
    }
    #endregion
}
