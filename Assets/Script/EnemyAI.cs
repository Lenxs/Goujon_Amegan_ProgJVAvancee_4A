using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target,feetPos;
    [SerializeField] float checkDistanceToDodge,checkGround;
    [SerializeField] LayerMask layerToDodge,whatIsGround;
    [SerializeField] float speed,distanceToShoot;
    Rigidbody rb;
    bool isGrounded;
    bool ballProche;


    private void Start()
    {
        target = GameObject.Find("P1").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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
            
            Debug.Log("shoot");
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
}
