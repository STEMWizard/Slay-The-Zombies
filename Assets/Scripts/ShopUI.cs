using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopUI : MonoBehaviour
{
    #region Variables
    [SerializeField]
    GameObject weaponsButton, playerButton, primaryExitButton, secondaryExitButton, playerShop, weaponShop;
    [SerializeField]
    Slider hPSlider, speedSlider;
    [SerializeField]
    Text hPLevelText, speedLevelText;
    [SerializeField]
    CanvasMenu canvasMenu;
    [SerializeField]
    GameObject pistolButton, rifleButton;
    [SerializeField]
    Text goldCount;
    Shop shop;
    #endregion

    #region Unity Methods
    private void Start()
    {
        shop = FindObjectOfType<Shop>();
        shop.LoadStats();
        UpdateHPBar();
        UpdateSpeedBar();
        shop.gold = shop.LoadGold();
        UpdateGoldCount();
    }
    #endregion

    #region Private Methods
    private void HidePlayerAndWeaponsButtons()
    {

        playerButton.SetActive(false);
        primaryExitButton.SetActive(false);
        secondaryExitButton.SetActive(true);
    }

    private void ShowPlayerShop()
    {
        secondaryExitButton.gameObject.SetActive(true);
        playerShop.SetActive(true);
    }

    private void ShowWeaponsShop()
    {
        secondaryExitButton.gameObject.SetActive(true);
        weaponShop.SetActive(true);
    }

    #endregion

    #region Public Methods
    public void PrimaryExitButtonPressed()
    {
        canvasMenu = FindObjectOfType<CanvasMenu>();
        canvasMenu.OpenMenu();
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex - 2);
    }

    public void SecondaryExitButtonPressed()
    {
        secondaryExitButton.SetActive(false);
        playerShop.SetActive(false);
        playerButton.SetActive(true);
        primaryExitButton.SetActive(true);
    }

    public void PistolSelected()
    {
        pistolButton.transform.localScale = new Vector3(1f, 1f, 0f);
        shop.weaponSelection = 0;
        rifleButton.transform.localScale = new Vector3(0.75f, 0.75f, 0f);
        shop.SaveStats();
    }

    public void PlayerButtonPressed()
    {
        HidePlayerAndWeaponsButtons();
        ShowPlayerShop();
    }

    public void RifleSelected()
    {
        rifleButton.transform.localScale = new Vector3(1f, 1f, 0f);
        shop.weaponSelection = 1;
        pistolButton.transform.localScale = new Vector3(0.75f, 0.75f, 0f);
        shop.SaveStats();
    }

    public void WeaponsButtonPressed()
    {
        HidePlayerAndWeaponsButtons();
        ShowWeaponsShop();
    }

    public void UpdateGoldCount()
    {
        goldCount.text = $"${shop.gold}";
    }

    public void UpdateHPBar()
    {
        hPSlider.value = shop.hpLevel;
        hPLevelText.text = $"HP Level {shop.hpLevel}";
    }

    public void UpdateSpeedBar()
    {
        speedSlider.value = shop.speedLevel;
        speedLevelText.text = $"Speed Level {shop.speedLevel}";
    }

    public void UpgradeHP()
    {
        if (shop.hpLevel < 5 && shop.gold >= 500)
        {
            shop.hpLevel++;
            shop.SaveStats();
            shop.gold -= 500;
            shop.SaveGold();
            UpdateHPBar();
            UpdateGoldCount();
        }
    }

    public void UpgradeSpeed()
    {
        if(shop.speedLevel < 5 && shop.gold >= 500)
        {
            shop.speedLevel++;
            shop.SaveStats();
            shop.gold -= 500;
            shop.SaveGold();
            UpdateSpeedBar();
            UpdateGoldCount();
        }
    }
    #endregion
}
