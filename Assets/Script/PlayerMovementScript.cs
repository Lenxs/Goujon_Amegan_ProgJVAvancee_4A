using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    //Horizontal movement
    [SerializeField]
    private float speed;


    //Jump
    [SerializeField] private float initJumpSpeed;
    private float currentJumpSpeed;
    [SerializeField] private float speedLossSecond;
    private bool jumping;
    [SerializeField] private float jumpTime;
    private float jumpTimer = 0;
    [SerializeField] private Vector3 gravity = new Vector3(0, -20, 0);

    //Input
    [SerializeField]
    private string inputY;
    [SerializeField]
    private string inputX;

    private CharacterController controller;
    

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

        }else if(jumping == true)//Player stopped jumping
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
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(Input.GetAxis(inputX) < 0){
            move += new Vector3(0f, 0f, -speed) * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //Gravity
        if(controller.isGrounded == false)
        {
            applyGravity = gravity * Time.deltaTime;
        }

        controller.Move(move + applyGravity);
        
    }
}
