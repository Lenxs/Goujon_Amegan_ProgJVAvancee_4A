using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScripts : MonoBehaviour
{
    [SerializeField] string fire = "Fire1";
    [SerializeField] string MoveH = "Horizontal";
    [SerializeField] string Jump = "Jump";
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float checkSphere = .3f;
    [SerializeField] float speedBullet = 15f;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform feetPos;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnBullet;
    [SerializeField] public Player Owner;
    [SerializeField] PlayerStats stats;
    [SerializeField] Animator bowAnim, charAnim;
    float move;
    bool isGrounded;
    bool canShoot =true;

    //KeyCode L, R,L2,R2;
    //float doubleTapTime;
    //int side;
    //[SerializeField] string dashInput;

    //[SerializeField] float dashSpeed;
    //[SerializeField] float dashCount;
    //[SerializeField] float startDashCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //dashCount = startDashCount;
        checkOwner();
    }

    void checkOwner()
    {
        if (Owner == Player.Player2)
        {
            fire = "Fire2";
            MoveH = "Horizontal2";
            Jump = "Jump2";
        }
    }
    private void OnEnable()
    {
        canShoot = true;
    }

    private void FixedUpdate()
    {
        move = Input.GetAxis(MoveH);
        rb.velocity = new Vector3(0, 0, move * speed);
        if (move > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (move < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
       
        isGrounded = Physics.CheckSphere(feetPos.position, checkSphere, whatIsGround);

        
        if (Input.GetButtonDown(Jump) && isGrounded==true)
        {
            Debug.Log("jump");
            rb.velocity = Vector3.up * jumpForce;
            isGrounded = false;
        }
        if (Input.GetButtonDown(fire)&& stats.GetAmmo()>0&&canShoot==true)
        {
            charAnim.SetTrigger("Fire");
            bowAnim.SetTrigger("Fire");
            StartCoroutine(Shoot());
        }

        if (move != 0)
        {
            charAnim.SetBool("Run", true);
        }
        else
        {
            charAnim.SetBool("Run", false);
        }

        //DASH
        //if (side==0)
        //{
        //    if (Input.GetButton(MoveL))
        //    {
        //        if (doubleTapTime > Time.time && L == KeyCode.Q || L2==KeyCode.LeftArrow)
        //        {
        //            Debug.Log("dash");
        //            side = 1;
        //        }
        //        else
        //        {
        //            doubleTapTime = Time.time + 0.5f;
        //        }
        //        L = KeyCode.Q;
        //        L2 = KeyCode.LeftArrow;
        //    }

        //    if (Input.GetButton(MoveR))
        //    {
        //        if (doubleTapTime > Time.time && R == KeyCode.D|| R2==KeyCode.RightArrow)
        //        {
        //            Debug.Log("dash");
        //            side = 2;
        //        }
        //        else
        //        {
        //            doubleTapTime = Time.time + 0.5f;
        //        }
        //        R = KeyCode.D;
        //        R2 = KeyCode.RightArrow;
        //    }
        //}
        //else
        //{
        //    if (dashCount <= 0)
        //    {
        //        side = 0;
        //        dashCount = startDashCount;
        //        rb.velocity = Vector3.zero;
        //    }
        //    else
        //    {
        //        dashCount -= Time.deltaTime;
        //        if (side == 1)
        //        {
        //            rb.velocity = Vector3.back * dashSpeed;
        //        }
        //        else if (side == 2)
        //        {
        //            rb.velocity = Vector3.forward * dashSpeed;
        //        }
        //    }

        //}

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
