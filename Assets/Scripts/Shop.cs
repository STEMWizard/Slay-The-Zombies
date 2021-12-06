using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Shop : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private int hpLevel0, hpLevel1, hpLevel2, hpLevel3, hpLevel4, hpLevel5;
    [SerializeField]
    private float speedLevel0, speedLevel1, speedLevel2, speedLevel3, speedLevel4, speedLevel5;
    [SerializeField]
    GameObject[] guns;

    public int gold;
    public int hpLevel = 0;
    public int speedLevel = 0;
    public int weaponSelection = 0;
    #endregion

    public void SaveStats()
    {
        PlayerPrefs.SetInt("hp level", hpLevel);
        PlayerPrefs.SetInt("speed level", speedLevel);
        PlayerPrefs.SetInt("weapon selection", weaponSelection);
        PlayerPrefs.Save();
    }    

    public void LoadStats()
    {
        hpLevel = PlayerPrefs.GetInt("hp level");
        speedLevel = PlayerPrefs.GetInt("speed level");
        weaponSelection = PlayerPrefs.GetInt("weapon selection");
    }

    public void SaveGold()
    {
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.Save();
    }

    public void SaveGold(int gold)
    {
        this.gold = gold;
        PlayerPrefs.SetInt("gold", this.gold);
        PlayerPrefs.Save();
    }


    public int LoadGold()
    {
        gold = PlayerPrefs.GetInt("gold");
        return gold;
    }

    #region Unity Methods
    private void Awake()
    {
        int shops = FindObjectsOfType<Shop>().Length;
        if (shops > 1)
            Destroy(gameObject);
        else 
            DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Public Methods
    public int DecidePlayerHP()
    {
        switch (hpLevel)
        {
            case 0:
                return hpLevel0;
            case 1:
                return hpLevel1;
            case 2:
                return hpLevel2;
            case 3:
                return hpLevel3;
            case 4:
                return hpLevel4;
            case 5:
                return hpLevel5;
            default:
                Debug.LogError("HP Level does not exist");
                return hpLevel0;
        }
    }

    public float DecidePlayerMovementSpeed()
    {
        switch (speedLevel)
        {
            case 0:
                return speedLevel0;
            case 1:
                return speedLevel1;
            case 2:
                return speedLevel2;
            case 3:
                return speedLevel3;
            case 4:
                return speedLevel4;
            case 5:
                return speedLevel5;
            default:
                Debug.LogError("Speed level does not exist");
                return speedLevel0;
        }
    }
    #endregion
}
