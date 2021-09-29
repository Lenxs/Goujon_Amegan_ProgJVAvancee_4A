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
    [SerializeField] float timeNextMove,fireRate;
    bool isGrounded;
    bool canShoot = true;
    int randomChoose;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //StartCoroutine(RandMove());
        NextMove();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(feetPos.position, checkSphere, whatIsGround);
    }

    private void FixedUpdate()
    {
        timeNextMove -= Time.fixedDeltaTime;

        switch (randomChoose)
        {
            case 1:
                //Debug.Log("Move Left");
                transform.eulerAngles = new Vector3(0, 180, 0);
                transform.position += Vector3.back * speed * Time.fixedDeltaTime;

                break;
            case 2:
                //Debug.Log("Move Right");
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.position += Vector3.forward * speed * Time.deltaTime;
                break;
            case 3:
                //Debug.Log("Jump");
                if (isGrounded == true)
                {
                    rb.velocity = Vector3.up * jumpForce;
                    isGrounded = false;
                }
                break;
            case 4:
                //Debug.Log("Shoot");
                if (stats.GetAmmo() > 0 && canShoot == true)
                {
                    StartCoroutine(Shoot());
                }
                else
                {

                    //Transform ammo = GameObject.Find("Ammo").transform;
                    //float step = speed * Time.fixedDeltaTime;
                    //if (Vector3.Distance(transform.position, ammo.position) > 0.5f)
                    //{
                    //    transform.position = Vector3.MoveTowards(new Vector3(0,transform.position.y,transform.position.z), new Vector3(0,ammo.position.y,ammo.position.z), step);
                    Debug.Log("SearchAmmo");
                    //}

                    //if (Vector3.Distance(transform.position, ammo.position) <= 0.5f)
                    //{
                    //    Debug.Log("ammo");
                    //    stats.GainAmmo();

                    //}
                }
                break;
        }
        if (timeNextMove < 0)
        {
            NextMove();
        }
    }

    

    private void OnEnable()
    {
       NextMove();
    }


    void NextMove()
    {
        randomChoose = Random.Range(1, 5);
        timeNextMove = Random.Range(2.0f, 5.5f);
    }

   

    void SearchAmmo()
    {
        Transform ammo = GameObject.Find("Ammo").transform;
        float step = speed * Time.fixedDeltaTime;
        if (Vector3.Distance(transform.position, ammo.position) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, ammo.position, step);

        }
        
        if (Vector3.Distance(transform.position, ammo.position) < 0.5f)
        {
            Debug.Log("ammo");
            stats.GainAmmo();
           
        }
    }
    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, spawnBullet.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = transform.forward * speedBullet;
        stats.LostAmmo();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
        Debug.Log("Bang");
    }
}
