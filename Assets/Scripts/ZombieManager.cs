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
        //sets round count and zombies to 0 and starts the zombie spawner
        rounds = 0;
        zombies = 0;
        AreZombiesDead();
        StartCoroutine(SpawnManager());
    }

    private void Update()
    {
        //checks if all zombies are dead
        AreZombiesDead();

        //updates the round text on screen
        RoundText.text = rounds.ToString();
    }

    //returns the current round when called
    public float ReturnCurrentRound()
    {
        return rounds;
    }

    //when called it will increass the zombie count by ZombieIncreassAmmount
    public void ZombieCountIncress(float ZombieIncreassAmmount)
    {
        zombies += ZombieIncreassAmmount;
    }

    //when called it will decreassed the zombie ammount by ZombieDecreassAmmount
    public void ZombieCountDecress(float ZombieDecreassAmmount)
    {
        zombies -= ZombieDecreassAmmount;
    }

    //when called it will check if all zombies are gone by using tags
    void AreZombiesDead()
    {
        if (GameObject.FindGameObjectsWithTag("Zombie").Length <= 0)
        {
            //returns if the prosess is already running
            if (areZombiesDead)
            {
                return;
            }
            //calls round end and stops looping (it can skip 7-12 rounds)
            RoundEnd();
            areZombiesDead = true;
        }
    }

    //when called it will incress the round ammount by 1 and set zombies to 0
    void RoundEnd()
    {
        rounds++;
        zombies = 0;
    }

    //the spawn manager with a varible spawn delay
    IEnumerator SpawnManager()
    {
        while (true)
        {
            //dynamic zombie spawn ammount that gradually increasses as round progress
            while (zombies <= rounds * 2f && areZombiesDead)
            {
                if (zombies >= rounds * 2f)
                {
                    //stops the prosses
                    areZombiesDead = false;
                }
                else
                {
                    //spawns a zombie
                    zombieSpawner.ZombieSpawn();
                }

                //waits 0.2 seconds
                yield return new WaitForSeconds(0.2f);
            }

            //waits 0.2 seconds
            yield return new WaitForSeconds(0.2f);
        }
    }
}
