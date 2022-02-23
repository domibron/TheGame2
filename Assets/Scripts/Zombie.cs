using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage (float ammount)
    {
        health -= ammount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }



}
