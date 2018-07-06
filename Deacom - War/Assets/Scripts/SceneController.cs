using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour {
    #region Variables and Declarations
    #endregion

    #region Custom Methods
    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
