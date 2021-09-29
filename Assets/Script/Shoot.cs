using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private string fire = "Fire1";
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnBullet;
    [SerializeField] private float speedBullet = 7f;
    [SerializeField] private Player Owner;
    [SerializeField] PlayerStats stats;
    [SerializeField] Animator charAnimator;
    [SerializeField] Animator bowAnimator;


    private void Start()
    {
        checkOwner();
        stats = GetComponent<PlayerStats>();
               
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
        if (Input.GetButtonDown(fire))
        {
            //Launch animation
            charAnimator.SetTrigger("Fire");
            bowAnimator.SetTrigger("Fire");

            FireShoot();
        }
    }

    void FireShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnBullet.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = transform.forward * speedBullet;
        stats.LostAmmo();
    }
}
