using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieManager : MonoBehaviour
{
    public ZombieSpawner zombieSpawner;

    float rounds;
    float zombies;

    bool areZombiesDead;

    public Text RoundText;



    private void Start()
    {
        rounds = 0;
        zombies = 0;
        AreZombiesDead();
        StartCoroutine(SpawnManager());
    }

    private void Update()
    {
        AreZombiesDead();

        RoundText.text = rounds.ToString();
    }

    public float ReturnCurrentRound()
    {
        return rounds;
    }

    public void ZombieCountIncress(float ZombieIncreassAmmount)
    {
        zombies += ZombieIncreassAmmount;
    }

    public void ZombieCountDecress(float ZombieIncreassAmmount)
    {
        zombies -= ZombieIncreassAmmount;
    }

    void AreZombiesDead()
    {
        if (GameObject.FindGameObjectsWithTag("Zombie").Length <= 0)
        {
            if (areZombiesDead)
            {
                return;
            }
            RoundEnd();
            areZombiesDead = true;
        }
    }

    void RoundEnd()
    {
        rounds++;
        zombies = 0;
    }

    IEnumerator SpawnManager()
    {
        while (true)
        {
            while (zombies <= rounds * 2f && areZombiesDead)
            {
                if (zombies >= rounds * 2f)
                {
                    areZombiesDead = false;
                }
                else
                {
                    zombieSpawner.ZombieSpawn();
                }

                yield return new WaitForSeconds(0.2f);
            }
            
            yield return new WaitForSeconds(0.2f);
        }
    }
}
