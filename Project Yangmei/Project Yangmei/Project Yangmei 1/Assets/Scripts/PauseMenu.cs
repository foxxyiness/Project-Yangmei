using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI, shopUI;
    public int rifleCost = 3000;
    public int grenadeCost = 2000;
    public static bool canBuyRifle;
    public static bool canBuyGrenade;

    void Start()
    {
        canBuyGrenade = false;
        canBuyRifle = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle(pauseUI);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Toggle(shopUI);
        }

        if(pauseUI.activeSelf && shopUI.activeSelf)
        {
            Toggle(shopUI);
            Time.timeScale = 0f;
        }
    }

    public void Toggle(GameObject ui)
    {
        ui.SetActive(!ui.activeSelf);
        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Gun.gun.ableToShoot = false;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            Gun.gun.ableToShoot = true;
        }
           
    }

    public void Retry()
    {
        Toggle(pauseUI);
        SceneManager.LoadScene(0);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void BuyAssaultRifle()
    {
        if(rifleCost > PlayerManager.money)
        {
            Debug.Log("Cannot buy weapon");
        }
        else
        {
            PlayerManager.money -= rifleCost;
            Debug.Log("Bought Rifle");
            canBuyRifle = true;
        }
        
    }

    public void BuyGrenade()
    {
        if (grenadeCost > PlayerManager.money)
        {
            Debug.Log("Cannot buy weapon");
        }
        else
        {
            PlayerManager.money -= grenadeCost;
            Debug.Log("Bought Grenade");
            canBuyGrenade = true;
        }
    }
}
