using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] _SpawnLocations;

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
        round = ZManager.ReturnCurrentRound();

        SpawnRate = round + 5f;
    }

    public void ZombieSpawn()
    {
        if (Time.time >= NextSpawn)
        {
            NextSpawn = Time.time + 20f / SpawnRate;

            ZombieClone = Instantiate(Zombie);
            ZombieClone.transform.SetParent(this.transform);
            ZombieCloneScript = ZombieClone.GetComponent<Zombie>();
            ZombieCloneScript.target = Player.transform;

            ZManager.ZombieCountIncress(1f);

            ZombieClone.transform.position = _SpawnLocations[Random.Range(0, _SpawnLocations.Length)].transform.position;
        }
    }
}
