using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] LayerMask PlayerMask,BallMask,OtherMask;
    [SerializeField] GameManager gameManagerInstance;
    [SerializeField] GameObject ammoPf;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip death;


    private void Start()
    {
        gameManagerInstance = GameObject.Find("GameManager").GetComponent<GameManager>();
        source= GameObject.Find("GameManager").GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision col)
    {
        if(((1<<col.gameObject.layer)& PlayerMask.value) > 0)
        {

            source.clip = death;
            source.Play();
            gameManagerInstance.PlayerDeath(col.gameObject);
            Destroy(gameObject);
        }

        if (((1 << col.gameObject.layer) & BallMask.value) > 0)
        {
            Destroy(gameObject);
        }


        if (((1 << col.gameObject.layer) & OtherMask.value) > 0)
        {
            Instantiate(ammoPf, this.gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        


    }
}
