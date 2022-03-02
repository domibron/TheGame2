using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class Scr_Weapon_manager : MonoBehaviour
{
    public GameObject PistolGun;
    public GameObject AkGun;

    GameObject[] Slots;

    public Text WeaponAmmo;

    public GameObject impactEffect;
    public GameObject impactEffectOther;

    public Scr_Ingame_Menu gameCanvasScript;

    public Camera fpsCam;

    //classes
    Pistol pistol = new Pistol();
    AK ak = new AK();

    private void Start()
    {
        Slots = new GameObject[] { PistolGun, AkGun };

        AkGun.SetActive(false);

        PistolGun.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Slots[0].SetActive(true);
            Slots[1].SetActive(false);
        }
        
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Slots[0].SetActive(false);
            Slots[1].SetActive(true);
        }


        //main weapon physics and settings tuned for the weapon
        if (PistolGun.activeSelf)
        {
            pistol.ShootPistolActive(fpsCam, impactEffect, impactEffectOther, gameCanvasScript, PistolGun, WeaponAmmo);
        }
        else if (AkGun.activeSelf)
        {
            ak.ShootAkActive(fpsCam, impactEffect, impactEffectOther, gameCanvasScript, AkGun, WeaponAmmo);
        }
    }
}


//Guns settigs and classes


public class Pistol : MonoBehaviour
{
    float nextTimeToFire = 2f;
    float ammo = 12f;
    float ammoCap = 12f;
    float reserveAmmo = 36f;
    float reserveAmmoCap = 96f;
    float fireRate = 80f;
    float range = 100f;
    float damage = 60f;
    float impactForce = 4f;
    Vector3 WeaponPosition;

    Vector3 weaponADS;
    Vector3 weaponDefault;

    public void ShootPistolActive(Camera fpsCam, GameObject impactEffect, GameObject impactEffectOther, Scr_Ingame_Menu gameCanvasScript, GameObject gun, Text WeaponAmmo)
    {
        weaponADS.Set(0f, -0.146f, 0.35f);
        weaponDefault.Set(0.34f, -0.27f, 0.49f);

        WeaponAmmo.text = ammo + "/" + ammoCap + "  {" + reserveAmmo + "}";

        gun.transform.localPosition = WeaponPosition;

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire && ammo > 0)
        {
            nextTimeToFire = Time.time + 20f / fireRate;
            Shoot(fpsCam, impactEffect, impactEffectOther, gameCanvasScript);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            WeaponPosition = Vector3.Lerp(WeaponPosition, weaponADS, 10 * Time.deltaTime);
        }
        else
        {
            WeaponPosition = Vector3.Lerp(WeaponPosition, weaponDefault, 10 * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void Shoot(Camera fpsCam, GameObject impactEffect, GameObject impactEffectOther, Scr_Ingame_Menu gameCanvasScript)
    {
        Debug.Log("Shot");
        RaycastHit hit;
        ammo = ammo - 1f;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //accessing the zombie for the TakeDamage function
            Zombie target = hit.transform.GetComponent<Zombie>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            //force to push rigidbodies back
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //related to the partical system
            if (hit.transform.tag == "Zombie")
            {
                //adds 10 pints to the score
                gameCanvasScript.IncreassScore(10);

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.2f);
            }

            else
            {
                GameObject impactGO = Instantiate(impactEffectOther, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.25f);
            }
        }
    }

    void Reload()
    {
        //PistolAnimation.Play();

        if (reserveAmmo <= 0)
        {
            return;
        }

        if (reserveAmmo > 0)
        {
            //nothing at the moment.
            float y;

            //y is the invert of what's left in the clip.
            y = ammoCap - ammo;

            if (reserveAmmo >= y)
            {
                //this takes away y from the ammo reserve.
                reserveAmmo = reserveAmmo - y;

                //this puts the ammo from reserve into clip.
                ammo += y;
            }
            else
            {
                //gets whats left in reserve.
                y = reserveAmmo;

                //adds what's left in ammo reserve. 
                ammo += y;

                //sets reserve to 0.
                reserveAmmo = 0f;
            }
        }
    }
}

public class AK : MonoBehaviour
{
    float nextTimeToFire = 2f;
    float ammo = 30f;
    float ammoCap = 30f;
    float reserveAmmo = 120f;
    float reserveAmmoCap = 360f;
    float fireRate = 160f;
    float range = 300f;
    float damage = 20f;
    float impactForce = 4f;
    Vector3 WeaponPosition;

    Vector3 weaponADS;
    Vector3 weaponDefault;

    public void ShootAkActive(Camera fpsCam, GameObject impactEffect, GameObject impactEffectOther, Scr_Ingame_Menu gameCanvasScript, GameObject gun, Text WeaponAmmo)
    {
        weaponADS.Set(0f, -0.2177f, -0.4f);
        weaponDefault.Set(0.45f, -0.4f, 0.49f);

        WeaponAmmo.text = ammo + "/" + ammoCap + "  {" + reserveAmmo + "}";

        gun.transform.localPosition = WeaponPosition;

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire && ammo > 0)
        {
            nextTimeToFire = Time.time + 20f / fireRate;
            Shoot(fpsCam, impactEffect, impactEffectOther, gameCanvasScript);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            WeaponPosition = Vector3.Lerp(WeaponPosition, weaponADS, 10 * Time.deltaTime);
        }
        else
        {
            WeaponPosition = Vector3.Lerp(WeaponPosition, weaponDefault, 10 * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void Shoot(Camera fpsCam, GameObject impactEffect, GameObject impactEffectOther, Scr_Ingame_Menu gameCanvasScript)
    {
        Debug.Log("Shot");
        RaycastHit hit;
        ammo = ammo - 1f;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //accessing the zombie for the TakeDamage function
            Zombie target = hit.transform.GetComponent<Zombie>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            //force to push rigidbodies back
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //related to the partical system
            if (hit.transform.tag == "Zombie")
            {
                //adds 10 pints to the score
                gameCanvasScript.IncreassScore(10);

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.2f);
            }

            else
            {
                GameObject impactGO = Instantiate(impactEffectOther, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.25f);
            }
        }
    }

    void Reload()
    {
        //PistolAnimation.Play();

        if (reserveAmmo <= 0)
        {
            return;
        }

        if (reserveAmmo > 0)
        {
            //nothing at the moment.
            float y;

            //y is the invert of what's left in the clip.
            y = ammoCap - ammo;

            if (reserveAmmo >= y)
            {
                //this takes away y from the ammo reserve.
                reserveAmmo = reserveAmmo - y;

                //this puts the ammo from reserve into clip.
                ammo += y;
            }
            else
            {
                //gets whats left in reserve.
                y = reserveAmmo;

                //adds what's left in ammo reserve. 
                ammo += y;

                //sets reserve to 0.
                reserveAmmo = 0f;
            }
        }
    }
}








/*
    //moved down here
    public Scr_Ingame_Menu gameUI;

    public Canvas canvas;
    public Text WeaponAmmo;
    public Vector3 weaponDefault;
    public Vector3 weaponADS;
    Vector3 WeaponPosition;
    public GameObject gun;


    [Header("Animations")]
    public Animation PistolAnimation;

    [Header("Weapon settings")]
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 1000000f;

    [Header("Weapon Ammo settings")]
    public float ammo = 12f;
    public float ammoCap = 12f;
    public float reserveAmmo = 36f;
    public float reserveAmmoCap = 96f;

    [Header("Particles")]
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject impactEffectOther;

    [Header("Other Settings")]
    private float nextTimeToFire = 0f;
    public AudioSource shot;
    public Camera fpsCam;

    private void Start()
    {
        WeaponPosition = weaponDefault;

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
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ammo > 0)
        {
            nextTimeToFire = Time.time + (20f / fireRate);
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        WeaponAmmo.text = ammo + "/" + ammoCap + "  {" + reserveAmmo + "}";

        gun.transform.position = WeaponPosition;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            WeaponPosition = Vector3.Lerp(WeaponPosition, weaponADS, 10 * Time.deltaTime);
        }
        else
        {
            WeaponPosition = Vector3.Lerp(WeaponPosition, weaponDefault, 10 * Time.deltaTime);
        }

        transform.localPosition = WeaponPosition;

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

    void Shoot()
    {

        muzzleFlash.Play();
        shot.Play();
        ammo = ammo - 1f;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Zombie target = hit.transform.GetComponent<Zombie>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if (hit.transform.tag == "Zombie")
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.2f);

                gameUI.IncreassScore(10);
            }
            else
            {
                GameObject impactGO = Instantiate(impactEffectOther, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.25f);
            }
        }
    }

    void Reload()
    {
        PistolAnimation.Play();

        if (reserveAmmo <= 0)
        {
            //isReloading = false;
            return;
        }

        if (reserveAmmo > 0)
        {
            //nothing at the moment.
            float y;

            //y is the invert of what's left in the clip.
            y = 12f - ammo;

            if (reserveAmmo >= y)
            {
                //this takes away y from the ammo reserve.
                reserveAmmo = reserveAmmo - y;

                //this puts the ammo from reserve into clip.
                ammo += y;
            }
            else
            {
                //gets whats left in reserve.
                y = reserveAmmo;

                //adds what's left in ammo reserve. 
                ammo += y;

                //sets reserve to 0.
                reserveAmmo = 0f;
            }
        }
    }
}

*/