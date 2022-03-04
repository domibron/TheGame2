using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class Scr_Weapon_manager : MonoBehaviour
{
    //gets the main script and makes a float called current health
    public Scr_Movement_player MainScript;
    float currentHealth;

    //gets the weapon modles/prefabs as game objects
    public GameObject PistolGun;
    public GameObject AkGun;

    //creates a game object array called Slots
    GameObject[] Slots;

    //cavas objects, Game Objects, camera and audio file
    public Text WeaponAmmo;
    public GameObject impactEffect;
    public GameObject impactEffectOther;
    public Scr_Ingame_Menu gameCanvasScript;
    public Camera fpsCam;
    public AudioSource shot;

    //flashlight varibles and settings
    public Light FlashLight;
    bool toggleLight = false;

    //funtions for the weapons
    Pistol_Funtions pistolFuntions;
    Ak_Functions akFunctions;

    //classes within a class
    Pistol pistol = new Pistol();
    Ak ak = new Ak();

    private void Start()
    {
        //sets the defalt inventory
        Slots = new GameObject[] { PistolGun, AkGun };

        //set the pistol to be active
        AkGun.SetActive(false);
        PistolGun.SetActive(true);

        //gets the weapons' funtions
        pistolFuntions = PistolGun.GetComponent<Pistol_Funtions>();
        akFunctions = AkGun.GetComponent<Ak_Functions>();
    }

    private void Update()
    {
        //sets the current health by culling the Scr_Movement_player GetCurrentHealth funtion and setting the returned value
        currentHealth = MainScript.GetCurrentHealth();

        //swiches to primary weapon slot when the 1 key is pressed
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Slots[0].SetActive(true);
            Slots[1].SetActive(false);
        }

        //swiches to secondary weapon slot when the 1 key is pressed
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Slots[0].SetActive(false);
            Slots[1].SetActive(true);
        }

        //flashlight toggle set
        FlashLight.enabled = toggleLight;

        //toggles the flashlight when f key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            toggleLight = !toggleLight;
            FlashLight.enabled = toggleLight;
        }

        
        //weapon funtions are called when they're active
        if (PistolGun.activeSelf && currentHealth > 0)
        {
            pistol.ShootPistolActive(fpsCam, impactEffect, impactEffectOther, gameCanvasScript, PistolGun, WeaponAmmo, shot, pistolFuntions);
        }
        else if (AkGun.activeSelf && currentHealth > 0)
        {
            ak.ShootAkActive(fpsCam, impactEffect, impactEffectOther, gameCanvasScript, AkGun, WeaponAmmo, shot, akFunctions);
        }
    }

    //when called is fills all weapon's ammo
    public void AmmoFill()
    {
        pistol.MaxAmmo();

        ak.MaxAmmo();
    }


    class Pistol : Scr_Weapon_manager
    {
        //varibles for weapon
        float nextTimeToFire = 2f;
        float ammo = 12f;
        readonly float ammoCap = 12f;
        float reserveAmmo = 36f;
        float reserveAmmoCap = 96f;
        readonly float fireRate = 80f;
        readonly float range = 100f;
        readonly float damage = 20f;
        readonly float impactForce = 4f;

        //weapon ads varibles Vector 3s
        Vector3 WeaponPosition;
        Vector3 weaponADS;
        Vector3 weaponDefault;

        //When this is called the weapon shoots
        //you will need to pass a Camera for raycasts, imppactEffect for when the raycast hits a zombie,
        //imppactEffect for when the raycast hits anything other than a zombie, The in game screen cavas script to access some varibles,
        //The weapon game object for transformation, The ammo Text to eddit the text, audio shot for the SFX sounds when the weapon shoots,
        //the weapon's funtion script so any weapon dependent script can be accessed.
        public void ShootPistolActive(Camera fpsCam, GameObject impactEffect, GameObject impactEffectOther, Scr_Ingame_Menu gameCanvasScript, GameObject gun, Text WeaponAmmo, AudioSource shot, Pistol_Funtions pistolFuntions)
        {
            //weapon positions
            weaponADS.Set(0f, -0.146f, 0.35f);
            weaponDefault.Set(0.34f, -0.27f, 0.49f);

            //weapon text to display on screen
            WeaponAmmo.text = ammo + "/" + ammoCap + "  {" + reserveAmmo + "}";

            //transform the gun positions 
            gun.transform.localPosition = WeaponPosition;

            //shoots the weapon if the coditions are right (if left click && Time is grater than next time to fire && ammo is grater than 0)
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire && ammo > 0)
            {
                //delay is set
                nextTimeToFire = Time.time + 20f / fireRate;

                //this is where the fpsCam, impactEffects and impactEffectsOther, In game menu and shot audio file are passed
                Shoot(fpsCam, impactEffect, impactEffectOther, gameCanvasScript, shot);
            }

            //ads the weapon is the codions are correct (if mouse right)
            if (Input.GetKey(KeyCode.Mouse1))
            {
                WeaponPosition = Vector3.Lerp(WeaponPosition, weaponADS, 10 * Time.deltaTime);
            }
            //set the weapon back to normal if right mouse is lifted
            else
            {
                WeaponPosition = Vector3.Lerp(WeaponPosition, weaponDefault, 10 * Time.deltaTime);
            }

            //reloads the weapon if right key is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                //this is where the ak funtions are passed
                Reload(pistolFuntions);
            }
        }

        //when called the weapon sends a racast and is checks
        //this is where the fpsCam, impactEffects and impactEffectsOther, In game menu and shot audio file are passed
        public void Shoot(Camera fpsCam, GameObject impactEffect, GameObject impactEffectOther, Scr_Ingame_Menu gameCanvasScript, AudioSource shot)
        {
            //reduces ammo
            ammo--;

            //plays audio file
            shot.Play();

            //send out raycast and returns hit as target
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, range))
            {
                //accessing the zombie for the TakeDamage function using target
                Zombie target = hit.transform.GetComponent<Zombie>();

                //runs the take damage funtion wich removes the zombie's health ammount by the weapon damage if the target is not null
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                //force to push rigidbodies back if target is null
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                //related to the partical system
                if (hit.transform.tag == "Zombie")
                {
                    //adds 10 pints to the score
                    gameCanvasScript.IncreassScore(10);

                    //this creats a impact effect at the zombie and then destroys it.
                    GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGO, 2f);
                }

                else
                {
                    //this creats a impact effect at anything but a zombie and then destroys it.
                    GameObject impactGO = Instantiate(impactEffectOther, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGO, 2f);
                }
            }
        }


        //when called the weapon reloads also where the weapon funtions are passed
        void Reload(Pistol_Funtions pistolFuntions)
        {
            //cancels the funtion if there is no ammo in the weapon
            if (reserveAmmo <= 0)
            {
                return;
            }

            //plays the weapon's reload animation
            pistolFuntions.ReloadAnimation();


            //nothing at the moment.
            float y;

            //y is the invert of what's left in the clip.
            y = ammoCap - ammo;

            if (reserveAmmo >= y)
            {
                //this takes away y from the ammo reserve.
                reserveAmmo -= y;

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

        //when called the weapon ammo is filled to the max
        public void MaxAmmo()
        {
            reserveAmmo = reserveAmmoCap;
            ammo = ammoCap;
        }
    }

    class Ak : Scr_Weapon_manager
    {
        //varibles for weapon
        float nextTimeToFire = 2f;
        float ammo = 30f;
        readonly float ammoCap = 30f;
        float reserveAmmo = 120f;
        float reserveAmmoCap = 360f;
        readonly float fireRate = 160f;
        readonly float range = 300f;
        readonly float damage = 60f;
        readonly float impactForce = 4f;

        //weapon ads varibles Vector 3s
        Vector3 WeaponPosition;
        Vector3 weaponADS;
        Vector3 weaponDefault;

        //When this is called the weapon shoots
        //you will need to pass a Camera for raycasts, imppactEffect for when the raycast hits a zombie,
        //imppactEffect for when the raycast hits anything other than a zombie, The in game screen cavas script to access some varibles,
        //The weapon game object for transformation, The ammo Text to eddit the text, audio shot for the SFX sounds when the weapon shoots,
        //the weapon's funtion script so any weapon dependent script can be accessed.
        public void ShootAkActive(Camera fpsCam, GameObject impactEffect, GameObject impactEffectOther, Scr_Ingame_Menu gameCanvasScript, GameObject gun, Text WeaponAmmo, AudioSource shot, Ak_Functions akFunctions)
        {
            //hard set weapon positions
            weaponADS.Set(0f, -0.2177f, -0.4f);
            weaponDefault.Set(0.45f, -0.4f, 0.49f);

            //weapon text to display on screen
            WeaponAmmo.text = ammo + "/" + ammoCap + "  {" + reserveAmmo + "}";

            //transform the gun positions 
            gun.transform.localPosition = WeaponPosition;

            //shoots the weapon if the coditions are right (if left click && Time is grater than next time to fire && ammo is grater than 0)
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire && ammo > 0)
            {
                //delay is set
                nextTimeToFire = Time.time + 20f / fireRate;

                //this is where the fpsCam, impactEffects and impactEffectsOther, In game menu and shot audio file are passed
                Shoot(fpsCam, impactEffect, impactEffectOther, gameCanvasScript, shot);
            }

            //ads the weapon is the codions are correct (if mouse right)
            if (Input.GetKey(KeyCode.Mouse1))
            {
                WeaponPosition = Vector3.Lerp(WeaponPosition, weaponADS, 10 * Time.deltaTime);
            }
            //set the weapon back to normal if right mouse is lifted
            else
            {
                WeaponPosition = Vector3.Lerp(WeaponPosition, weaponDefault, 10 * Time.deltaTime);
            }

            //reloads the weapon if right key is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                //this is where the ak funtions are passed
                Reload(akFunctions);
            }
        }

        //when called the weapon sends a racast and is checks
        //this is where the fpsCam, impactEffects and impactEffectsOther, In game menu and shot audio file are passed
        public void Shoot(Camera fpsCam, GameObject impactEffect, GameObject impactEffectOther, Scr_Ingame_Menu gameCanvasScript, AudioSource shot)
        {
            //reduces ammo
            ammo--;

            //plays audio file
            shot.Play();

            //send out raycast and returns hit as target
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, range))
            {
                //accessing the zombie for the TakeDamage function using target
                Zombie target = hit.transform.GetComponent<Zombie>();

                //runs the take damage funtion wich removes the zombie's health ammount by the weapon damage if the target is not null
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                //force to push rigidbodies back if target is null
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                //related to the partical system
                if (hit.transform.tag == "Zombie")
                {
                    //adds 10 pints to the score
                    gameCanvasScript.IncreassScore(10);

                    //this creats a impact effect at the zombie and then destroys it.
                    GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGO, 2f);
                }

                else
                {
                    //this creats a impact effect at anything but a zombie and then destroys it.
                    GameObject impactGO = Instantiate(impactEffectOther, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGO, 2f);
                }
            }
        }


        //when called the weapon reloads also where the weapon funtions are passed
        void Reload(Ak_Functions akFunctions)
        {
            //cancels the funtion if there is no ammo in the weapon
            if (reserveAmmo <= 0)
            {
                return;
            }

            //plays the weapon's reload animation
            akFunctions.ReloadAnimation();

            
            //nothing at the moment.
            float y;

            //y is the invert of what's left in the clip.
            y = ammoCap - ammo;

            if (reserveAmmo >= y)
            {
                //this takes away y from the ammo reserve.
                reserveAmmo -= y;

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

        //when called the weapon ammo is filled to the max
        public void MaxAmmo()
        {
            reserveAmmo = reserveAmmoCap;
            ammo = ammoCap;
        }
    }
}