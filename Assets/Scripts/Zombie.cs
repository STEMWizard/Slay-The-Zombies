using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Zombie : MonoBehaviour
{
    #region Variables
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float attackSpeed = 2f;
    [SerializeField]
    int damage;
    [SerializeField]
    int hp;
    [SerializeField] [Range(0f, 100f)]
    float powerupDropChance;
    [SerializeField]
    int goldDropped;
    [SerializeField]
    Animator animator;

    bool isAlive;

    Player player;
    float nextHit;
    [SerializeField]
    GameObject[] powerups;
    GameLoopUIManager uiManager;
    [SerializeField]
    AudioClip growl;
    #endregion

    #region Unity Methods
    void Start()
    {
        // Will need rework due to player respawning etc
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        uiManager = FindObjectOfType<GameLoopUIManager>();
        isAlive = true;
        StartCoroutine(Growl());
    }

    void FixedUpdate()
    {
        if (player.gameObject.activeInHierarchy == true && player.IsAlive && isAlive)
        {
            animator.SetBool("PlayerAlive", true);
            MoveTowardPlayer();
        }
        else if (!isAlive)
        {
            Destroy(gameObject.GetComponent<Collider2D>());
            StopMoving();
        }
        else
        {
            animator.SetBool("PlayerAlive", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isAlive)
        {
            AttackPlayer();
        }
    }
    #endregion

    #region Private Methods
    private IEnumerator Growl()
    {
        yield return new WaitForSeconds(Random.Range(0f, 5f));
        AudioSource.PlayClipAtPoint(growl, transform.position);
    }

    private void AttackPlayer()
    {
        // Attack speed implementation
        if (Time.time > nextHit)
        {
            nextHit = Time.time + attackSpeed;
            int rNG = Random.Range(1, 4);
            animator.SetInteger("AttackRNG", rNG);
            animator.SetTrigger("Attack");
            player.TakeDamage(damage);
        }
    }

    private void DropPowerup()
    {
        int index = Random.Range(0, powerups.Length - 1);
        Instantiate(powerups[index], transform.position, Quaternion.identity);
    }

    // Math stuff which causes zombie to face player & move down (prefab faces down so zombie moves forward)
    private void MoveTowardPlayer()
    {
        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objPos = transform.position;
        targ.x = targ.x - objPos.x;
        targ.y = targ.y - objPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8.41f, 8.41f), transform.position.y);
    }
    #endregion

    #region Public Methods
    public void ReceiveDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            isAlive = false;
            int rNG = Random.Range(1, 3);
            animator.SetInteger("DeathRNG", rNG);
            animator.SetTrigger("Death");
            player.Gold += Random.Range(goldDropped - 10, goldDropped + 11);
            uiManager.UpdateGold(player.Gold);
            float powerupDrop = Random.Range(0f, 100f);
            if (powerupDrop <= powerupDropChance)
                DropPowerup();
            StopMoving();
            Destroy(gameObject, 1f);
        }
    }

    private void StopMoving()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, transform.position.x, transform.position.x), Mathf.Clamp(transform.position.y,
            transform.position.y, transform.position.y));
    }
    #endregion
}