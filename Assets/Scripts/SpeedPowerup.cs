using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    #region Variables
    [SerializeField]
    int durationInSeconds;

    bool isActive;
    
    PlayerMovement playerMovement;
    Player player;
    #endregion

    #region Unity Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" ||collision.gameObject.tag == "Bullet")
        {
            playerMovement = collision.transform.GetComponent<PlayerMovement>();
            player = collision.transform.GetComponent<Player>();
            if (player.isSpedUp == false)
            {
                StartCoroutine(DoubleSpeed());
                Destroy(gameObject);
            }
            else
            {
                StopCoroutine(DoubleSpeed());
                player.isSpedUp = false;
                if (playerMovement.CurrentSpeed != playerMovement.StartingSpeed)
                {
                    playerMovement.CurrentSpeed = playerMovement.StartingSpeed;
                    StartCoroutine(DoubleSpeed());
                    Destroy(gameObject);
                }
            }
        }
    }
    #endregion

    #region Private Methods
    private IEnumerator DoubleSpeed()
    {
        player.isSpedUp = true;
        playerMovement.CurrentSpeed = playerMovement.CurrentSpeed * 2;
        yield return new WaitForSeconds(durationInSeconds);
        playerMovement.CurrentSpeed = playerMovement.CurrentSpeed / 2;
        player.isSpedUp = false;
    }
    #endregion
}
