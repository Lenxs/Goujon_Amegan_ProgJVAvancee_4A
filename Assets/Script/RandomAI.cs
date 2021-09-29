using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : MonoBehaviour
{

    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 2.5f;
    [SerializeField] float checkSphere = .3f;
    [SerializeField] float speedBullet = 7f;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform feetPos;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnBullet;
    [SerializeField] PlayerStats stats;
    bool isGrounded;
    bool canShoot;
    int randomChoose;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(RandMove());
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(feetPos.position, checkSphere, whatIsGround);
    }

    private void OnEnable()
    {
        StartCoroutine(RandMove());
    }

    IEnumerator RandMove()
    {
        yield return new WaitForSeconds(.2f);
        randomChoose = Random.Range(1, 5);
        //Debug.Log(randomChoose);
        switch (randomChoose)
        {
            case 1:
                MoveL();
                yield return new WaitForSeconds(3f);
                break;
            case 2:
                MoveR();
                yield return new WaitForSeconds(3f);
                break;
            case 3:
                Jump();
                break;
            case 4:
                Fire();
                break;
        }
        //RandomChoose();
    }
    void RandomChoose()
    {
        randomChoose = Random.Range(1, 5);
        //Debug.Log(randomChoose);
        switch (randomChoose)
        {
            case 1:MoveL();
                break;
            case 2:MoveR();
                break;
            case 3:Jump();
                break;
            case 4:Fire();
                break;
        }
    }

    void MoveL()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.position += Vector3.back * speed * Time.deltaTime;
        StartCoroutine(RandMove());
    }

    void MoveR()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        transform.position += Vector3.forward * speed * Time.deltaTime;
        StartCoroutine(RandMove());
    }

    void Jump()
    {
        if (isGrounded == true)
        {
            rb.velocity = Vector3.up * jumpForce;
            isGrounded = false;
        }
        StartCoroutine(RandMove());
    }

    void Fire()
    {
        
        if (stats.GetAmmo() > 0&& canShoot==true)
        {
            StartCoroutine(Shoot());
        }
        else
        {
            SearchAmmo();
        }
       StartCoroutine(RandMove());
    }

    void SearchAmmo()
    {
        Transform ammo = GameObject.Find("Ammo").transform;
        Vector3.MoveTowards(transform.position, ammo.position, 30f);
        if (Vector3.Distance(transform.position, ammo.position) < 0.5f)
        {
            Debug.Log("ammo");
            stats.GainAmmo();
            StartCoroutine(RandMove());
        }
    }
    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, spawnBullet.position, Quaternion.identity);
        bullet.SetActive(true);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = transform.forward * speedBullet;
        stats.LostAmmo();
        yield return new WaitForSeconds(1.5f);
        canShoot = true;
        Debug.Log("Bang");
        StartCoroutine(RandMove());
    }
}
