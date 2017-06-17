using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    /* Variables */
    // Input text
    private InputField inputField;

    // Start button
    private Button startButton;

    /* Métodos */

    private void Awake()
    {
        inputField = this.GetComponentInChildren<InputField>();
        startButton = this.transform.Find("StartGameButton").GetComponent<Button>();
    }

    private void Update()
    {
        if (inputField.text.Length < 1)
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        }
    }

    public void TurnToUpperCase()
    {
        inputField.text = inputField.text.ToUpper();
    }

    public void StoreInGameManager()
    {
        GameManager.playerName = inputField.text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
