using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoopUIManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    Text goldCounter;
    [SerializeField]
    Text ammoCount;
    [SerializeField]
    Slider healthBar;
    //respawn test
    Player player;
    [SerializeField]
    GameObject respawnPrompt, billableRespawnPrompt;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        //player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        Player.PlayerHasDied += ShowRespawnPrompt;
        player = FindObjectOfType<Player>();
        UpdateGold(player.Gold);
        if(!player)
        {
            StartCoroutine(Wait(3f));
        }
    }

    private void OnDestroy()
    {
        Player.PlayerHasDied -= ShowRespawnPrompt;
    }
    #endregion

    #region Public Methods
    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        player = FindObjectOfType<Player>();
        UpdateGold(player.Gold);
    }

    public void ShowRespawnPrompt()
    {
        if (!player)
            player = FindObjectOfType<Player>();
        respawnPrompt.SetActive(true);
    }

    public void UpdateAmmo(int ammoRemaining, int ammoMax)
    {
        ammoCount.text = $"{ammoRemaining} / {ammoMax}";
    }

    public void UpdateGold(int gold)
    {
        goldCounter.text = "$" + gold;
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }
    #endregion
}
