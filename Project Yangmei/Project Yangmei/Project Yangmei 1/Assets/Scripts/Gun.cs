using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Fire and More")]
    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 15;
    public float impactForce = 30f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading;

    private float nextTimeToFire = 0f;

    [Header("Animations")]
    public Animator animator;

    [Header("Camera")]
    public Camera fpsCam;

    [Header("Effect Stuff lol")]
    public ParticleSystem muzzleFlash;
    public Transform muzzle;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }


    // Update is called once per frame
    void Update()
    {
        //Checks to see if gun is reloading
        if (isReloading)
            return;


        if(currentAmmo <= 0 || Input.GetButtonDown("Reload"))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
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

        currentAmmo = maxAmmo;
        isReloading = false;

    }
    void Shoot()
    {

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
