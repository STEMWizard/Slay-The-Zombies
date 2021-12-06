using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    
    public bool IsAlive { get; private set; }
    [SerializeField] public int startingHP, currentHP; // Serialized for debugging & testing
    public bool isSpedUp;

    [SerializeField]
    GameObject gun;
    [SerializeField]
    Shop shop;
    GameLoopUIManager uiManager;
    SpawnManager spawnManager;
    public int deathCount;
    private int gold;
    public int Gold
    {
        get{ return gold; }

        set 
        {   if (gold > 999999)
                gold = 999999;
            else
                gold = value;
        }
    }
    public delegate void PlayerDeath();
    public static event PlayerDeath PlayerHasDied;
    public delegate void PlayerSpawn();
    public static event PlayerSpawn PlayerHasSpawned;

    #endregion

    #region Unity Methods
    private void Awake()
    {
        uiManager = FindObjectOfType<GameLoopUIManager>();
        shop = FindObjectOfType<Shop>();
        spawnManager = FindObjectOfType<SpawnManager>();
        if (spawnManager.ongoingSpawnProcesses == 0)
        {
            if (PlayerHasSpawned == null)
            {
                spawnManager.AddToPlayerEvent();
                Respawn();
            }
            else
            {
                Respawn();
            }
        }
    }

    private void Start()
    {
        gold = shop.LoadGold();   
    }

    #endregion

    #region Public Methods
    public void Die()
    {
        shop.SaveGold(gold);
        deathCount++;
        currentHP = 0;
        IsAlive = false;
        uiManager.UpdateHealth(currentHP, startingHP);
        if (PlayerHasDied != null) PlayerHasDied();
        gameObject.SetActive(false);
    }

    public void HealSelf(int healValue)
    {
        currentHP += healValue;
    }

    public void Respawn()
    {
        transform.position = new Vector2(0, -3);
        // Not used yet. Will probably be useful
        IsAlive = true;
        startingHP = shop.DecidePlayerHP();
        currentHP = startingHP;
        uiManager.UpdateHealth(currentHP, startingHP);
        if (PlayerHasSpawned != null) PlayerHasSpawned();
    }

    // Will change current sprite also. More bloodied version of base. Will be handled by PlayerHP scriptable object
    public void TakeDamage(int damage)
    {
        // Takes damage & handles death
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
        uiManager.UpdateHealth(currentHP, startingHP);
    }
    #endregion
}