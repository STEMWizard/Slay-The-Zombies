using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
    #region Variables
    private string gameID = "3758815";
    public string myPlacementID = "FreeRespawn";
    Button button;
    [SerializeField]
    Player player;
    RespawnPrompt respawnPrompt;
    SpawnManager spawnManager;
    #endregion

    #region Unity Methods
    void Awake()
    {
        player = FindObjectOfType<Player>();
        respawnPrompt = FindObjectOfType<RespawnPrompt>();
        spawnManager = FindObjectOfType<SpawnManager>();
        button = GetComponent<Button>();
        button.interactable = Advertisement.IsReady(myPlacementID);
        if (button)
            button.onClick.AddListener(ShowRewardedVideo);
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, true);
    }

    public void OnUnityAdsReady(string placementID)
    {
        if (placementID == myPlacementID)
        {
            button.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementID, ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            if (!player)
            {
                spawnManager.SpawnPlayer();
                player = FindObjectOfType<Player>();
            }
            player.gameObject.SetActive(true);
            player.Respawn();
            if (!respawnPrompt)
                respawnPrompt = FindObjectOfType<RespawnPrompt>();
            respawnPrompt.gameObject.SetActive(false);
        }
        else if (result == ShowResult.Skipped)
        {
            respawnPrompt.ClosePrompt();
        }
        else if (result == ShowResult.Failed)
        {
            respawnPrompt.ClosePrompt();
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log error
    }

    public void OnUnityAdsDidStart(string placementID)
    {
        // Optional
    }
    #endregion

    #region Public Methods
    public void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementID);
    }
    #endregion
}
