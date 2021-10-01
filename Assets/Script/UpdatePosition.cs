using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePosition : MonoBehaviour
{

    private void Start()
    {
        if (transform.position.x != 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.x != 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z); 
        }
    }
}
