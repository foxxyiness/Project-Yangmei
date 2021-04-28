using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject player;

    public static int money = 0;
    public TextMeshProUGUI moneyText;
    void Start()
    {
        money = 1000;
    }

    void Update()
    {
        moneyText.SetText("$" + money.ToString());
    }


}
