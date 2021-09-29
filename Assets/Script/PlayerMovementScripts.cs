using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScripts : MonoBehaviour
{
    [SerializeField] string fire = "Fire1";
    [SerializeField] string MoveR = "Right";
    [SerializeField] string MoveL = "Left";
    [SerializeField] string Jump = "Jump";
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 2.5f;
    [SerializeField] float checkSphere = .3f;
    [SerializeField] float speedBullet = 7f;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform feetPos;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnBullet;
    [SerializeField] public Player Owner;
    [SerializeField] PlayerStats stats;
    bool isGrounded;
    bool canShoot =true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        checkOwner();
    }

    void checkOwner()
    {
        if (Owner == Player.Player2)
        {
            fire = "Fire2";
            MoveR = "Right2";
            MoveL = "Left2";
            Jump = "Jump2";
        }
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(feetPos.position, checkSphere, whatIsGround);

        if (Input.GetButton(MoveR))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetButton(MoveL))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        if (Input.GetButtonDown(Jump) && isGrounded==true)
        {
            
            rb.velocity = Vector3.up * jumpForce;
            isGrounded = false;
        }
        if (Input.GetButtonDown(fire)&& stats.GetAmmo()>0&&canShoot==true)
        {
            StartCoroutine(Shoot());
        }

    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, spawnBullet.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = transform.forward * speedBullet;
        stats.LostAmmo();
        yield return new WaitForSeconds(1.5f);
        canShoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(feetPos.position, checkSphere);
    }
}
