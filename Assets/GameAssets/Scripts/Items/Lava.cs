using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

    /* Variables */
    // Modificador de velocidad que se aplica al entrar en el barro
    [SerializeField]
    private float speedModifier = 1.2f;

    // Daño que hace la lava
    [SerializeField]
    private int lavaDamage = 10;

    // Tiempo que pasa entre ticks de lava
    [SerializeField]
    private float hitCooldown = 1;

    // Tiempo en el que la lava debe hacer daño
    private float timeToHit;

    // Lista de personajes que están en la lava
    private List<Character> characterList;

    /* Métodos */

    private void Awake()
    {
        characterList = new List<Character>();
    }

    private void Update()
    {
        if (Time.time >= timeToHit && characterList.Count > 0)
        {
            foreach (Character character in characterList)
            {
                character.ReceiveDamage(lavaDamage);
            }

            timeToHit = Time.time + hitCooldown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.gameObject.GetComponent<Character>();

        if (character != null)
        {
            character.MoveSlower(speedModifier);

            characterList.Add(character);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character character = other.gameObject.GetComponent<Character>();

        if (character != null)
        {
            character.MoveFaster(speedModifier);

            characterList.Remove(character);
        }
    }

}
