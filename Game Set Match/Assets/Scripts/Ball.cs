using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int hitter;//0:not served; 1:player; 2:opponent;
    public int shotType; //0:serve; 1:upswing; 2:chop;
    public bool fulfill;//whether the ball has hit the opposite court
    public int score;// 0:nobody scored yet; 1;player scored; 2:opponent scored;
    void Start()
    {
        score = 0;
        hitter = 0;
        fulfill = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (score != 0) return;
        if (other.CompareTag("Out") || other.CompareTag("Wall"))
        {
            if(hitter == 1)
            {
                if (fulfill)
                    score = 1;
                else
                    score = 2;
            }
            else if(hitter == 2)
            {
                if (fulfill)
                    score = 2;
                else
                    score = 1;
            }
        }
        else if(other.CompareTag("In_p"))
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
