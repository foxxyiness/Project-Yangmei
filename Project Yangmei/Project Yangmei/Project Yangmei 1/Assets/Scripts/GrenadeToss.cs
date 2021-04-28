using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrenadeToss : MonoBehaviour
{
    public int grenadeAmmo = 0;
    public int maxGrenade = 3;
    public float throwForce = 40f;
    public GameObject grenadePrefab;
    public bool ableToThrow;
    public TextMeshProUGUI grenadeAmmoText;

    void Start()
    {
        ableToThrow = false;
    }
    // Update is called once per frame
    void Update()
    {
        grenadeAmmoText.SetText(grenadeAmmo.ToString() + "x Grenades");
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
            grenadeAmmo--;
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
