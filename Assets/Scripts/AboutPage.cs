using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutPage : MonoBehaviour
{
    #region Variables
    [SerializeField]
    GameObject menu;
    #endregion

    #region Public Methods
    public void CloseAboutPage()
    {
        gameObject.SetActive(false);
        menu.SetActive(true);
    }
    #endregion
}
