using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] LayerMask PlayerMask;
    [SerializeField] GameManager gameManagerInstance;
   

    private void Start()
    {
        gameManagerInstance = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnCollisionEnter(Collision col)
    {
        if(((1<<col.gameObject.layer)& PlayerMask.value) > 0)
        {
                       
            
            gameManagerInstance.PlayerDeath(col.gameObject);
            Destroy(gameObject);
        }
        
    }
}
