using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsManager : MonoBehaviour {

    /* Variables */
    // Sprite de interrogación, para cuando no se tiene un arma
    [SerializeField]
    private Sprite interrogationSign;

    // Player
    [SerializeField]
    private PlayerHands playerHands;

    // Lista de sprites de armas
    private List<Sprite> weaponSpriteList;

    /* Métodos */

    private void Awake()
    {
        weaponSpriteList = new List<Sprite>();
    }

    private void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform childTransform = this.transform.GetChild(i);
            Image weaponImage = childTransform.Find("Image").GetComponent<Image>();
            weaponSpriteList.Add(weaponImage.sprite);

            if (!playerHands.IsWeaponOwned(i))
            {
                weaponImage.sprite = interrogationSign;
                childTransform.Find("Text").gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Actualiza la UI de las armas para reflejar cambios a la hora de recibir nuevas armas
    /// </summary>
    /// <param name="ownedWeapons"></param>
    public void RefreshWeaponsGUI(bool[] ownedWeapons)
    {
        for (int i = 0; i < ownedWeapons.Length; i++)
        {
            if (ownedWeapons[i])
            {
                Transform childTransform = this.transform.GetChild(i);
                Image weaponImage = childTransform.Find("Image").GetComponent<Image>();

                weaponImage.sprite = weaponSpriteList[i];
                childTransform.Find("Text").gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Modifica las propiedades de la imagen de fondo del arma seleccionada
    /// </summary>
    /// <param name="id"></param>
    public void ChooseWeapon(int id)
    {
        ResetWeaponsBackground();

        Image childImage = this.transform.GetChild(id).GetComponent<Image>();

        childImage.color = RGBColor(255, 255, 160, 255);
    }

    private void ResetWeaponsBackground()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Image childImage = this.transform.GetChild(i).GetComponent<Image>();

            childImage.color = RGBColor(255, 255, 255, 100);
        }
    }

    private Color RGBColor(float r, float g, float b, float a)
    {
        if (r > 255)
            r = 255f;

        if (g > 255)
            g = 255f;

        if (b > 255)
            b = 255f;

        if (a > 255)
            a = 255f;

        r /= 255f;
        g /= 255f;
        b /= 255f;
        a /= 255f;

        return new Color(r, g, b, a);
    }
}
