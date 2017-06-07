using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {


    /* Variables */
    // Vida
    [SerializeField]
    protected int currentLife = 100;
    [SerializeField]
    protected int maxLife = 200;

    // Invulnerable
    [SerializeField]
    protected bool invulnerable = false;

    public bool hasBlood = true;

    /* Métodos */

    /// <summary>
    /// Devuelve la vida actual
    /// </summary>
    /// <returns></returns>
    public int GetCurrentLife()
    {
        return currentLife;
    }

    /// <summary>
    /// Devuelve la vida máxima
    /// </summary>
    /// <returns></returns>
    public int GetMaxLife()
    {
        return maxLife;
    }

    /// <summary>
    /// Devuelve la vida actual
    /// </summary>
    /// <returns></returns>
    public bool GetInvulnerable()
    {
        return invulnerable;
    }

    /// <summary>
    /// Devuelve la vida máxima
    /// </summary>
    /// <returns></returns>
    public void SetInvulnerable(bool newInvulnerable)
    {
        invulnerable = newInvulnerable;
    }

    /// <summary>
    /// Recibe daño
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(int damage)
    {
        if (invulnerable)
        {
            return;
        }

        currentLife -= damage;
        
        // Limitar valor de la vida al rango entre 0 y maxLife
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);

        if (currentLife == 0)
        {
            KillMe();
        }
    }

    /// <summary>
    /// Añade vida
    /// </summary>
    /// <param name="life"></param>
    public void AddLife(int life)
    {
        currentLife += life;

        if (currentLife > maxLife)
        {
            currentLife = maxLife;
        }
    }

    /// <summary>
    /// Mata al propio objeto
    /// </summary>
    protected virtual void KillMe()
    {
        // Se sobrescribe el método en el objeto que hereda de esta clase
    }

    /// <summary>
    /// Hace que el objeto se mueva más lento
    /// </summary>
    public virtual void MoveSlower(float speedModifier)
    {
        // Se sobreescribe en los objetos que heredan
    }

    /// <summary>
    /// Hace que el objeto se mueva más rápido
    /// </summary>
    public virtual void MoveFaster(float speedModifier)
    {
        // Se sobreescribe en los objetos que heredan
    }
}
