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

    KeyCode L, R,U;
    float doubleTapTime;
    int side;
    [SerializeField] string dashInput;

    [SerializeField] float dashSpeed;
    [SerializeField] float dashCount;
    [SerializeField] float startDashCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dashCount = startDashCount;
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

        if (side==0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (doubleTapTime > Time.time && L == KeyCode.Q)
                {
                    Debug.Log("dash");
                    side = 1;
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }
                L = KeyCode.Q;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (doubleTapTime > Time.time && R == KeyCode.D)
                {
                    Debug.Log("dash");
                    side = 2;
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }
                R = KeyCode.D;
            }
            /*if (Input.GetKeyDown(KeyCode.Z))
            {
                if (doubleTapTime > Time.time && U == KeyCode.Z)
                {
                    Debug.Log("dash Up");
                    side = 3;
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }
                U = KeyCode.Z;
            }*/

            //if (isTap)
            //{
            //    t1 = Time.time;
            //    isTap = false;
            //    if(t1-t2 < 0.2f)
            //    {
            //        Debug.Log("dash");
            //    }
            //}
        }
        else
        {
            if (dashCount <= 0)
            {
                side = 0;
                dashCount = startDashCount;
                rb.velocity = Vector3.zero;
            }
            else
            {
                dashCount -= Time.deltaTime;
                if (side == 1)
                {
                    rb.velocity = Vector3.back * dashSpeed;
                }
                else if (side == 2)
                {
                    rb.velocity = Vector3.forward * dashSpeed;
                }
                /*else if (side == 3)
                {
                    rb.velocity = Vector3.up * dashSpeed;
                }*/
            }

            

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
