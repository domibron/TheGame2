using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class Scr_Pistol : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ammo > 0)
        {
            nextTimeToFire = Time.time + (20f/fireRate);
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        WeaponAmmo.text = ammo + "/" + ammoCap + "  {" + reserveAmmo + "}";

        if (Input.GetKey(KeyCode.Mouse1))
        {
            WeaponPosition = Vector3.Lerp(WeaponPosition, weaponADS, 10 * Time.deltaTime);
        }
        else
        {
            WeaponPosition = Vector3.Lerp(WeaponPosition, weaponDefault, 10 * Time.deltaTime);
        }
        
        transform.localPosition = WeaponPosition;

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

        if (reserveAmmo <= 0 )
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
