using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    [SerializeField]
    LayerMask PlayerMask;
    [SerializeField] GameManager gameManagerInstance;


    private void Start()
    {
        gameManagerInstance = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    //private void OnCollisionEnter(Collision col)
    //{
    //    Debug.Log("col");
    //    if (((1 << col.gameObject.layer) & PlayerMask.value) > 0)
    //    {
    //        Debug.Log("ammo++");
    //        gameManagerInstance.AmmoGain(col.gameObject);
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        Debug.Log("no");
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & PlayerMask.value) > 0)
        {
            Debug.Log("ammo++");
            gameManagerInstance.AmmoGain(other.gameObject);
            Destroy(gameObject);
        }
    }
}
