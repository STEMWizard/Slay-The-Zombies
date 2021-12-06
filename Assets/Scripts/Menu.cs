using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    #region Variables
    [SerializeField]
    GameObject settings, aboutPage;
    [SerializeField]
    GameObject menuUI, gameloopUI, shopUI;
    int indexPosition;
    #endregion

    #region Unity Methods
    private void Start()
    {
        indexPosition = SceneManager.GetActiveScene().buildIndex;
        OpenMenu();
    }
    #endregion

    #region Public Methods
    // Scene index order: Menu, Game, Shop. About page will be in menu scene
    public void StartGame()
    {
        menuUI.SetActive(false);
        gameloopUI.SetActive(true);
        SceneManager.LoadScene(indexPosition + 1);
    }

    public void OpenShop()
    {
        menuUI.SetActive(false);
        SceneManager.LoadScene(indexPosition + 2);
    }

    public void OpenAboutPage()
    {
        aboutPage.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        gameloopUI.SetActive(false);
        menuUI.SetActive(true);
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
        gameObject.SetActive(false);
    }
    #endregion
}
