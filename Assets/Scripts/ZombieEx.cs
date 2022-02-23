using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEx : MonoBehaviour
{
    public float Health = 50f;

    public void TakeDamage(float ammount)
    {
        Health -= ammount;
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
