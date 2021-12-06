using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCanvas : MonoBehaviour
{
    private void Awake()
    {
        int i = FindObjectsOfType<Canvas>().Length;
        if (i > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }
}
