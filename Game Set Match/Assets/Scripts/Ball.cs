using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int hitter;//0:player not served; 1:player; 2:bot; 3:bot not served;
    public int shotType; //0:serve; 1:upswing; 2:chop;
    public bool fulfill;//whether the ball has hit the opposite court
    public int score;// 0:nobody scored yet; 1;player scored; 2:opponent scored;
    public int serveindex;//-1:unset;
    public float[,] servebounds = { { 0.67f, 4.77f, -6.28f, -3.2f }, { 4.77f, 8.93f, -3.20f, -0.12f }, { 0.67f, 4.77f, -3.2f, -0.12f }, { 4.77f, 8.93f, -6.28f, -3.2f } };

       
    void Start()
    {
        score = 0;
        shotType = 0;
        //hitter = 0;
        fulfill = false;
        serveindex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(serveindex == -1)
        {
            if (transform.position.x > 4.77f && transform.position.z > -3.2f)
                serveindex = 0;
            else if (transform.position.x > 4.77f && transform.position.z < -3.2f)
                serveindex = 2;
            else if (transform.position.x < 4.77f && transform.position.z > -3.2f)
                serveindex = 3;
            else
                serveindex = 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (score != 0) return;
        if(shotType == 0)
        {
            if (other.CompareTag("Player") || other.CompareTag("Bot"))
                return;
            if(!fulfill)
            {
                if (transform.position.x >= servebounds[serveindex,0] && transform.position.x <= servebounds[serveindex,1] && transform.position.z >= servebounds[serveindex,2] && transform.position.z <= servebounds[serveindex,3] && transform.position.y <= 4.7f)
                {
                    fulfill = true;
                }
                else
                {
                    if (hitter == 0 || hitter == 1) score = 2;
                    else score = 1;
                }
            }
            else
            {
                if (hitter == 1) score = 1;
                else score = 2;
            }
            
        }
        else
        {
            if (other.CompareTag("Out") || other.CompareTag("Wall"))
            {
                if (hitter == 1)
                {
                    if (fulfill)
                        score = 1;
                    else
                        score = 2;
                }
                else if (hitter == 2)
                {
                    if (fulfill)
                        score = 2;
                    else
                        score = 1;
                }
            }
            else if (other.CompareTag("In_p"))
            {
                if (hitter == 1)
                {
                    if (!fulfill)
                        score = 2;
                }
                else if (hitter == 2)
                {
                    if (fulfill)
                        score = 2;
                    else
                        fulfill = true;
                }
            }
            else if (other.CompareTag("In_op"))
            {
                if (hitter == 1)
                {
                    if (fulfill)
                        score = 1;
                    else
                        fulfill = true;
                }
                else if (hitter == 2)
                {
                    if (!fulfill)
                        score = 1;
                }
            }
        }
        
    }

}
