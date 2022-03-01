using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Weapon_manager : MonoBehaviour
{

    public GameObject[] _Weapons;
    int y = 0;


    // Start is called before the first frame update
    void Start()
    {
        int LenthOfWeaponArray = _Weapons.Length;

        _Weapons[0].SetActive(true);

        for (int x = 1; x <= LenthOfWeaponArray; x++)
        {
            _Weapons[x].SetActive(false);
        }

        y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _Weapons[1].SetActive(false);
            _Weapons[0].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _Weapons[1].SetActive(true);
            _Weapons[0].SetActive(false);
        }
    }
}
