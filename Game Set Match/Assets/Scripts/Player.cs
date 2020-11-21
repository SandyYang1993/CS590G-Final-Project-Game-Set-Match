﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform aimTarget; // the target where we aim to land the ball
    public int moveState = 0;
    private float[] Speeds = {0.0f,2.0f,1.0f,3.0f};
    public Vector3 Speed = new Vector3(0, 0, 1);
    public Vector3 Direction = new Vector3(0,0,1);
    public Vector3 Angle = new Vector3(0, -90, 0);
    public float[] Angles = { -90.0f, -135.0f, -45.0f, -180.0f, 0.0f, 90.0f, 135.0f, 45.0f };
    public float MouseX = 0;
    public float MouseY = 0;
    public string[] Shots = { "BackhandUpswing", "ForehandUpswing", "BackhandChop", "ForehandChop" };

    float force = 13; // ball impact force

    bool hitting; // boolean to know if we are hitting the ball or not 

    public Transform ball; // the ball 
    Animator animator;

    Vector3 aimTargetInitialPosition; // initial position of the aiming gameObject which is the center of the opposite court

    ShotManager shotManager; // reference to the shotmanager component
    Shot currentShot; // the current shot we are playing to acces it's attributes

    private void Start()
    {
        animator = GetComponent<Animator>(); // referennce out animator
        aimTargetInitialPosition = aimTarget.position; // initialise the aim position to the center( where we placed it in the editor )
        shotManager = GetComponent<ShotManager>(); // accesing our shot manager component 
        //currentShot = shotManager.topSpin; // defaulting our current shot as topspin
        //Debug.Log(Speeds[3]);
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // get the horizontal axis of the keyboard
        float v = Input.GetAxisRaw("Vertical"); // get the vertical axis of the keyboard

        moveState = 0;
        Angle = new Vector3(0, -90, 0);
        Direction = new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.W)){
            if (Input.GetKey(KeyCode.LeftShift))
                moveState = 3;
            else
                moveState = 1;
            TurnToDirection(moveState,1);
        }
        else if (Input.GetKey(KeyCode.S)){
            if (Input.GetKey(KeyCode.LeftShift))
                moveState = 3;
            else
                moveState = 2;
            TurnToDirection(moveState, 2);
        }
        else{
            if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    moveState = 3;
                else
                    moveState = 1; ;
                TurnToDirection(moveState, 3);
            }
            else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    moveState = 3;
                else
                    moveState = 1;
                TurnToDirection(moveState, 4);
            }
        }
        int shotType = DetectShot();
        if (shotType!=-1)
            animator.Play(Shots[shotType]);
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Shot"))
        {
            moveState = 0;
            Direction = new Vector3(0, 0, 1);
            Angle = new Vector3(0, -90, 0);
        }
        animator.SetInteger("MovementState", moveState);
        Speed = Direction.normalized * Speeds[moveState];
        transform.Translate(Speed * Time.deltaTime);
        transform.eulerAngles = Angle;

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            hitting = true; // we are trying to hit the wall and aim where to make it land
            currentShot = shotManager.topSpin; // set our current shot to top spin
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            hitting = false; // we let go of the key so we are not hitting anymore and this 
        }                    // is used to alternate between moving the aim target and ourself

        if (Input.GetKeyDown(KeyCode.E))
        {
            hitting = true; // we are trying to hit the ball and aim where to make it land
            currentShot = shotManager.flat; // set our current shot to top spin
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            hitting = false;
        }


        /*
        if (hitting)  // if we are trying to hit the ball
        {
            animator.Play("movement"); 
            aimTarget.Translate(new Vector3(h, 0, 0) * speed * 2 * Time.deltaTime); //translate the aiming gameObject on the court horizontallly
        }


        if ((h != 0 || v != 0) && !hitting) // if we want to move and we are not hitting the ball
        {
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); // move on the court
        }
        */


    }
    internal void TurnToDirection(int moveState, int dir)
    {
        if(dir == 1)
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                Angle = new Vector3(0, Angles[1], 0);
                //Direction = new Vector3(-1, 0, 1);
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                Angle = new Vector3(0, Angles[2], 0);
                //Direction = new Vector3(1, 0, 1);
            }
            else
            {
                //Direction = new Vector3(0, 0, 1);
                Angle = new Vector3(0, Angles[0], 0);
            }
        }
        else if(dir == 2 && moveState == 3)
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                Angle = new Vector3(0, Angles[6], 0);
                //Direction = new Vector3(-1, 0, -1);
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                Angle = new Vector3(0, Angles[7], 0);
                //Direction = new Vector3(1, 0, -1);
            }
            else
            {
                Angle = new Vector3(0, Angles[5], 0);
                //Direction = new Vector3(0, 0, -1);
            }
        }
        else if(dir == 2 && moveState == 2)
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                Angle = new Vector3(0, Angles[2], 0);
                //Direction = new Vector3(-1, 0, -1);
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                Angle = new Vector3(0, Angles[1], 0);
                //Direction = new Vector3(1, 0, -1);
            }
            else
            {
                Angle = new Vector3(0, Angles[0], 0);
                //Direction = new Vector3(0, 0, -1);
            }
            Direction = new Vector3(0, 0, -1);
        }
        else if(dir ==3)
        {
            Angle = new Vector3(0, Angles[3], 0);
            //Direction = new Vector3(-1, 0, 0);
        }
        else if(dir == 4)
        {
            Angle = new Vector3(0, Angles[4], 0);
            //Direction = new Vector3(1, 0, 0);
        }
    }
    internal int DetectShot()
    {
        int shotType = -1;
        if (Input.GetMouseButton(0))
        {
            MouseX += Input.GetAxis("Mouse X");
            MouseY += Input.GetAxis("Mouse Y");
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(MouseY>=0)
            {
                if (ball.position.z < transform.position.z)
                    shotType = 0;
                else
                    shotType = 1;
            }
            else
            {
                if (ball.position.z < transform.position.z)
                    shotType = 2;
                else
                    shotType = 3;
            }
            MouseX = 0.0f;
            MouseY = 0.0f;
        }
        return shotType;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // if we collide with the ball 
        {
            Vector3 dir = aimTarget.position - transform.position; // get the direction to where we want to send the ball
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
            //add force to the ball plus some upward force according to the shot being played

            Vector3 ballDir = ball.position - transform.position; // get the direction of the ball compared to us to know if it is
            if (ballDir.x >= 0)                                   // on out right or left side 
            {
                animator.Play("forehand");                        // play a forhand animation if the ball is on our right
            }
            else                                                  // otherwise play a backhand animation 
            {
                animator.Play("backhand");
            }

            aimTarget.position = aimTargetInitialPosition; // reset the position of the aiming gameObject to it's original position ( center)

        }
    }


}