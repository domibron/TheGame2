using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killbox : MonoBehaviour
{
    [SerializeField] Scr_Movement_player player;

    //when the somthing enters the collider it will send the player to the main menu
    private void OnTriggerEnter(Collider other)
    {
        player.ReciveDamage(1000000000000000);
    }
}
