using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    //arrray of possible spawn locations
    public GameObject[] _SpawnLocations;

    //other settings and varibles
    public GameObject Zombie;
    GameObject ZombieClone;
    Zombie ZombieCloneScript;
    public GameObject Player;
    float NextSpawn;
    float SpawnRate = 5f;
    float round = 0f;
    public ZombieManager ZManager;

    private void Update()
    {
        //gets current round
        round = ZManager.ReturnCurrentRound();

        //sets spawn rate depending on round
        SpawnRate = round + 5f;
    }

    //when called it will create a clone/instance of the zombie prefab with a cooldown
    public void ZombieSpawn()
    {
        //cool down if statement
        if (Time.time >= NextSpawn)
        {
            //sets next cooldown
            NextSpawn = Time.time + 20f / SpawnRate;

            //creates a zombie and sets the very important data for the zombie
            ZombieClone = Instantiate(Zombie);
            ZombieClone.transform.SetParent(this.transform);
            ZombieCloneScript = ZombieClone.GetComponent<Zombie>();
            ZombieCloneScript.target = Player.transform;

            //increass the zombie ammount
            ZManager.ZombieCountIncress(1f);

            //this will select a random spawn location from the array for the zombie to spawn
            ZombieClone.transform.position = _SpawnLocations[Random.Range(0, _SpawnLocations.Length)].transform.position;
        }
    }
}
