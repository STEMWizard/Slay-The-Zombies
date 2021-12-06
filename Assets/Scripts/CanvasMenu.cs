using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMenu : MonoBehaviour
{
    [SerializeField]
    GameObject gameloopUI, menuUI, shopUI;
    
    public void OpenMenu()
    {
        gameloopUI.SetActive(false);
        menuUI.SetActive(true);
    }
}
