using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target,feetPos;
    [SerializeField] float checkDistanceToDodge,checkGround;
    [SerializeField] LayerMask layerToDodge,whatIsGround;
    [SerializeField] float speed,distanceToShoot,fireRate,speedBullet;
    [SerializeField] PlayerStats stats;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnBullet;
    [SerializeField] NavMeshAgent agent;
    Rigidbody rb;
    bool isGrounded;
    bool ballProche;
    bool canShoot = true;


    private void Start()
    {
       // target = GameObject.Find("P1").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        agent.SetDestination(target.position);
        ballProche = Physics.CheckSphere(transform.position, checkDistanceToDodge, layerToDodge);//verifie ball dans un rayon
        isGrounded = Physics.CheckSphere(feetPos.position, checkGround, whatIsGround);
        if (ballProche && isGrounded==true)
        {
            DodgeBall();
        }

        float step = speed * Time.deltaTime;
        //deplacement jusqu'a la cible
        if (Vector3.Distance(transform.position, target.position) > distanceToShoot)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        // à la distance de tir
        if(Vector3.Distance(transform.position,target.position) < distanceToShoot)
        {
            if(stats.GetAmmo()>0 && canShoot == true)
            {
                Debug.Log("shoot");
                StartCoroutine(Shoot());
            }
            else
            {
                Transform ammo = GameObject.Find("Ammo").transform;
                //float step = speed * Time.fixedDeltaTime;
                if (Vector3.Distance(transform.position, ammo.position) > 0.5f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, ammo.position, step);
                    Debug.Log("SearchAmmo");
                }

                if (Vector3.Distance(transform.position, ammo.position) <= 0.5f)
                {
                    Debug.Log("ammo");
                    stats.GainAmmo();

                }
            }

        }
    }


    void DodgeBall()
    {
        rb.velocity = Vector3.up * 5f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, checkDistanceToDodge);
        Gizmos.DrawWireSphere(feetPos.position, checkGround);
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
