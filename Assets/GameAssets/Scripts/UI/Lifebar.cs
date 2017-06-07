using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour {

    /* Variables */
    // Foreground
    private Image lifebarImage;

    // Texto
    private Text lifebarText;

    // Vida del personaje
    [SerializeField]
    private Character character;

    // Color con toda la vida
    [SerializeField]
    private Color fullLifeColor;

    // Color con poca vida
    [SerializeField]
    private Color lowLifeColor;


    /* Métodos */

    private void Awake()
    {
        lifebarImage = this.transform.Find("Foreground").GetComponent<Image>();
        lifebarText = this.transform.Find("BarText").GetComponent<Text>();

        if (character == null)
        {
            character = this.GetComponentInParent<Character>();
        }
    }
	
	void Update () {
        float lifePercent = (float)character.GetCurrentLife() / (float)character.GetMaxLife();

        lifebarImage.fillAmount = lifePercent;

        lifebarImage.color = Color.Lerp(lowLifeColor, fullLifeColor, lifePercent);

        lifebarText.text = character.GetCurrentLife() + "/" + character.GetMaxLife();
    }
}
