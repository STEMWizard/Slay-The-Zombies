using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Variables
    [SerializeField]// For debugging
    int magazineCount;
    [SerializeField]
    Weapon weaponStats;
    [SerializeField]
    float offsetX, offsetY;
    [SerializeField]
    Transform bulletSpawn;

    [SerializeField]
    GameObject ammo;
    bool isReloading;
    bool isOwned;

    Joystick joystick;
    float nextShot;
    PlayerMovement playerMovement;
    [SerializeField]
    GameObject muzzleFlash;
    Vector3 bloomMechanic;
    GameLoopUIManager uiManager;
    Animator animator;
    GameObject h;
    [SerializeField]
    AudioClip reload, shoot;
    #endregion

    #region Unity Methods
    private void Start()
    {
        magazineCount = weaponStats.maxAmmo;
        joystick = GameObject.FindGameObjectWithTag("Right Stick").GetComponent<Joystick>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        animator = GetComponentInParent<Animator>();
        uiManager = FindObjectOfType<GameLoopUIManager>();
        uiManager.UpdateAmmo(magazineCount, weaponStats.maxAmmo);
    }

    void Update()
    {
        // Method called when right joystick is moved
        if (joystick)
            if (joystick.Horizontal != 0 || joystick.Vertical != 0) 
                Shoot();
        if (h)
            h.transform.position = bulletSpawn.position;
    }
    #endregion

    #region Private Methods
    private void Shoot()
    {
        // Shoots in the direction player is facing
        // Fire rate implemented here
        if (Time.time > nextShot && magazineCount > 0 && !isReloading)
        {
            nextShot = Time.time + weaponStats.fireRate;

            bloomMechanic = transform.up * 25f;
            Vector3.Normalize(bloomMechanic);
            RaycastHit2D hit = Physics2D.Raycast(bulletSpawn.position, bloomMechanic);
            Debug.DrawRay(bulletSpawn.position, bloomMechanic);
            BulletTrailSFX();
            magazineCount--;
            uiManager.UpdateAmmo(magazineCount, weaponStats.maxAmmo);
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<Zombie>().ReceiveDamage(weaponStats.damage);
            }
        }
        if (magazineCount <= 0)
            StartCoroutine(Reload());
    }

    // Reload mechanic
    private IEnumerator Reload()
    {
        isReloading = true;
        animator.SetTrigger("Reload");
        yield return new WaitForSeconds(weaponStats.reloadSpeed);
        magazineCount = weaponStats.maxAmmo;
        uiManager.UpdateAmmo(magazineCount, weaponStats.maxAmmo);
        isReloading = false;
    }

    private void BulletTrailSFX()
    {
        Quaternion playerRotation = GetComponentInParent<Player>().transform.rotation;
        animator.SetTrigger("ShotFired");
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
        h = Instantiate(muzzleFlash, bulletSpawn.position + new Vector3(offsetX, offsetY), 
            playerRotation);
        //h.transform.rotation = Quaternion.Euler(0f, 0f, -playerMovement.dir);
        Destroy(h.gameObject, 0.05f);
    }
    #endregion
}
