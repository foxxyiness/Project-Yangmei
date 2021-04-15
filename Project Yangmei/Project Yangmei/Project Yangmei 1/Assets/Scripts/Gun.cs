using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Fire and More")]
    public float damage = 25f;
    public float range = 100f;

    [Header("Camera")]
    public Camera fpsCam;

    [Header("Effect Stuff lol")]
    public ParticleSystem muzzleFlash;
    public Transform muzzle;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
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
