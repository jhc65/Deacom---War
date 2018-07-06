using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    #region Variables and Declarations
    [SerializeField] private Constants.Globals.suit cardSuit;
    [SerializeField] private Constants.Globals.value cardValue;

    #region Getters and Setters
    public Constants.Globals.suit suit {
        get { return cardSuit; }
    }

    public Constants.Globals.value value {
        get { return cardValue; }
    }
    #endregion
    #endregion

    #region Custom Methods
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
