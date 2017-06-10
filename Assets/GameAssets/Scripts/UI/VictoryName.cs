using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryName : MonoBehaviour {

    /* Variables */

    /* Métodos */

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        this.transform.Find("Name").GetComponent<Text>().text = GameManager.playerName.ToUpper();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("menu");
        }
    }
}
