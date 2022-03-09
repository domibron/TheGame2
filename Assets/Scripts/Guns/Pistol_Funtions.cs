using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_Funtions : MonoBehaviour
{
    public Animation reload;

    public void ReloadAnimation()
    {
        reload.Play();
    }
}
