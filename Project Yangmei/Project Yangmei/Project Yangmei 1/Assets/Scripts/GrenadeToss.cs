using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeToss : MonoBehaviour
{
    public int grenadeAmmo = 0;
    public int maxGrenade = 3;
    public float throwForce = 40f;
    public GameObject grenadePrefab;
    public bool ableToThrow;

    void Start()
    {
        ableToThrow = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(grenadeAmmo == 0)
        {
            ableToThrow = false;
        }
        if(PauseMenu.canBuyGrenade == true && grenadeAmmo > 0)
        {
            ableToThrow = true;
        }
        if(Input.GetKeyDown(KeyCode.Q) && ableToThrow == true)
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
