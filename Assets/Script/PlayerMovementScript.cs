using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float initJumpSpeed;
    private float currentJumpSpeed;
    [SerializeField]private float speedLossSecond;

    [SerializeField]
    private string inputY;
    [SerializeField]
    private string inputX;

    [SerializeField] private LayerMask FloorLayerMask;

    private bool jumping;

    [SerializeField] private float jumpTime;
    private float jumpTimer = 0;

    private CharacterController controller;
    private Vector3 gravity = new Vector3(0, -20, 0);

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        Vector3 applyGravity = Vector3.zero;

        //Jump
        if(Input.GetAxis(inputY) > 0 && (controller.isGrounded == true || jumping == true))
        {
            if(controller.isGrounded == true)
            {
                currentJumpSpeed = initJumpSpeed;
                jumpTimer = 0;

                Debug.Log("Start jump");
            }

            move += new Vector3(0f, currentJumpSpeed, 0f) * Time.deltaTime;

            

            currentJumpSpeed -= speedLossSecond * Time.deltaTime;

            jumping = true;

        }else if(jumping == true)
        {
            jumping = false;
            Debug.Log("End jump");
        }

        //Jump Timer
        if(jumping == true)
        {
            jumpTimer += Time.deltaTime;

            if(jumpTimer > jumpTime)
            {
                jumping = false;
                Debug.Log("End jump");
            }
        }

        //Horizontal movement
        if(Input.GetAxis(inputX) > 0)
        {
            move += new Vector3(0f, 0f, speed) * Time.deltaTime;
        }else if(Input.GetAxis(inputX) < 0){
            move += new Vector3(0f, 0f, -speed) * Time.deltaTime;
        }

        //Gravity
        if(controller.isGrounded == false)
        {
            applyGravity = gravity * Time.deltaTime;
        }

        controller.Move(move + applyGravity);
        
    }
}
