using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Gun : MonoBehaviour
{
    [Header("Gun Fire and More")]
    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 15;
    public float impactForce = 30f;

    [Header("Ammo Stuff")]
    public int ammoInClip = 10;
    public int maxAmmo = 70;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading;

    private float nextTimeToFire = 0f;

    private bool ableToShoot;

    [Header("Text UI Stuff")]
    public TextMeshProUGUI cAmmo;
    public TextMeshProUGUI mAmmo;

    [Header("Animations")]
    public Animator animator;

    [Header("Camera")]
    public Camera fpsCam;

    [Header("Effect Stuff lol")]
    public ParticleSystem muzzleFlash;
    public Transform muzzle;

    void Start()
    {
        currentAmmo = ammoInClip;
        ableToShoot = true;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }


    // Update is called once per frame
    void Update()
    {
        cAmmo.SetText(currentAmmo.ToString());
        mAmmo.SetText(maxAmmo.ToString());

        //Checks to see if gun is reloading
        if (isReloading)
            return;

        //Reloads if clip is empty
        if(currentAmmo <= 0 && ableToShoot == true)
        {
            StartCoroutine(Reload());
            maxAmmo -= ammoInClip;
            return;
        }
        //Reloads if Ammo has been shot and player presses reload button "r"
        if(currentAmmo < ammoInClip && Input.GetButtonDown("Reload") && ableToShoot == true)
        {
            StartCoroutine(Reload());
            maxAmmo = maxAmmo - (ammoInClip - currentAmmo);
            return;
        }

        //Shoots weapon if Left Clip is presses with the rate of fire for the weapon
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            if(ableToShoot == true)
                Shoot();

        }


        
        if(maxAmmo <= 0 && currentAmmo <= 0)
        {
            ableToShoot = false;
        }
        if(ableToShoot == false)
        {
            currentAmmo = 0;
            maxAmmo = 0;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = ammoInClip;
        isReloading = false;

    }
    void Shoot()
    {
       // muzzleFlash.Play();
        currentAmmo --;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Debug.Log(hit.transform.name);
          Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }

           Instantiate(muzzleFlash, muzzle.position, muzzle.rotation);
        }
    }
}
