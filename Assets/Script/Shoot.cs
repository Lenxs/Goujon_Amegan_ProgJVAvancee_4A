using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private string fire = "Fire1";
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnBullet;
    [SerializeField] private float speedBullet = 7f,fireRate;
    [SerializeField] private Player Owner;
    [SerializeField] PlayerStats stats;
    [SerializeField] Animator bowAnim, charAnim;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip arrow;
    PlayerMovementScript movementScript;
    bool canShoot = true;


    private void Start()
    {
        checkOwner();
        stats = GetComponent<PlayerStats>();
        movementScript = GetComponent<PlayerMovementScript>();
        charAnim = GetComponent<Animator>();
        bowAnim = GetComponent<Animator>();

    }

    private void OnEnable()
    {
        canShoot = true;
    }

    void checkOwner()
    {
        if (Owner == Player.Player2)
        {
            fire = "Fire2";
        }
    }
    void Update()
    {
        if (Input.GetButtonDown(fire) && stats.GetAmmo() > 0 && canShoot == true)
        {
            charAnim.SetTrigger("Fire");
            bowAnim.SetTrigger("Fire");
            source.clip = arrow;
            source.Play();
            StartCoroutine(FireShoot());
        }
    }

    IEnumerator FireShoot()
    {
        
        canShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, spawnBullet.position, Quaternion.identity);
        
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = transform.forward * speedBullet;
        stats.LostAmmo();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
