using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RespawnPrompt : MonoBehaviour
{
    #region Variables
    [SerializeField]
    Button freeYesButtons;
    [SerializeField]
    SpawnManager spawnManager;
    [SerializeField]
    Menu menu;
    [SerializeField]
    CanvasMenu canvasMenu;
    Player player;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        Player.PlayerHasDied += OpenPrompt;
        player = FindObjectOfType<Player>();
        int i = FindObjectsOfType<RespawnPrompt>().Length;
        if (i > 1) Destroy(gameObject);
        else DontDestroyOnLoad(transform.root.gameObject);
    }

    private void OnDestroy()
    {
        Player.PlayerHasDied -= OpenPrompt;
    }
    #endregion

    #region Public Variables
    public void ClosePrompt()
    {
        // Must reset pulsating effect
        if (!spawnManager)
            spawnManager = FindObjectOfType<SpawnManager>();
        spawnManager.ongoingSpawnProcesses = 0;
        gameObject.SetActive(false);
        canvasMenu = GetComponentInParent<CanvasMenu>();
        canvasMenu.OpenMenu();
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index - 1);
    }

    public void OpenPrompt()
    {
        gameObject.SetActive(true);
        // Start pulstating effect method
    }
    #endregion
}
