using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int winner; //0 for player, 1 for bot
    public int pScore;
    public int opScore;
    public int FSMstate;
    public Vector3[] ballpositions = { new Vector3(12.92768f, 5.0f, -1.5f), new Vector3(-3.37f, 5.0f, -4.5f), new Vector3(12.92768f, 5.0f, -4.5f), new Vector3(-3.37f, 5.0f, -1.5f) };
    public Vector3[] standings = { new Vector3(12.92768f, 4.5456f, -1.5f), new Vector3(-3.37f, 4.5456f, -4.5f), new Vector3(12.92768f, 4.5456f, -4.5f), new Vector3(-3.37f, 4.5456f, -1.5f) };
    public GameObject playerprefab;
    public GameObject botprefab;
    public GameObject ballprefab;
    public GameObject player;
    public GameObject bot;
    public GameObject ball;
    
    //5 states:
    //0 game set: place player at initial position. no one moves now. destroy the previous ball. leftclick to serve state.
    //1 serve: start serving. initialize the ball. enter play state or game end state.
    //2 play: ball has been served, players start playing. enter game end state.
    //3 game end: someone scored, game end. no one moves now. leftclick to game set or match end state.
    //4 match end: someone scored enough points, match end. enter postmatch scene.
    void Start()
    {
        pScore = 0;
        opScore = 0;
        FSMstate = 0;
        Instantiate(playerprefab, standings[0], Quaternion.identity);
        Instantiate(botprefab, standings[1], Quaternion.identity);
        player = GameObject.FindWithTag("Player");
        bot = GameObject.FindWithTag("Bot");
        ball = null;
    }

    
    void Update()
    {
        if(FSMstate == 0)
        {
            if ((pScore + opScore) % 4 == 0 || (pScore + opScore) % 4 == 1)
            {
                player.GetComponent<Transform>().position = standings[0];
                bot.GetComponent<Transform>().position = standings[1];
            }
            else
            {
                player.GetComponent<Transform>().position = standings[2];
                bot.GetComponent<Transform>().position = standings[3];
            }
            if (ball != null)
            {
                Destroy(ball);
                ball = null;
            }
                
            if (Input.GetMouseButtonUp(0))
                FSMstate = 1;
        }
        else if(FSMstate == 1)
        {
            if(ball == null)
            {
                Instantiate(ballprefab, standings[(pScore + opScore) % 4], Quaternion.identity);
                ball = GameObject.FindWithTag("Ball");
                ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0);
                if ((pScore + opScore) % 2 == 0)
                    ball.GetComponent<Ball>().hitter = 0;
                else
                    ball.GetComponent<Ball>().hitter = 3;
            }

            //if ((pScore + opScore) % 4 == 0)
            //{
            //    Instantiate(ballprefab, standings[0], Quaternion.identity);
            //}
            //else if((pScore + opScore) % 4 == 1)
            //{
            //    Instantiate(ballprefab, standings[1], Quaternion.identity);
            //}
            //else if((pScore + opScore) % 4 == 2)
            //{
            //    Instantiate(ballprefab, standings[2], Quaternion.identity);
            //}
            //else
            //{
            //    Instantiate(ballprefab, standings[3], Quaternion.identity);
            //}
            if (ball.GetComponent<Ball>().hitter == 1 || ball.GetComponent<Ball>().hitter == 2)
                FSMstate = 2;
        }
        else if(FSMstate == 2)
        {
            if(ball.GetComponent<Ball>().score != 0)
            {
                if (ball.GetComponent<Ball>().score == 1)
                   pScore++;
                else
                   opScore++;
                FSMstate = 3;
            }
        }
        else if(FSMstate == 3)
        {
            
            if(pScore >= 7 && pScore-opScore>=2)
            {
                winner = 0;
                if (Input.GetMouseButtonUp(0))
                    FSMstate = 4;
            }
            else if(opScore >= 7 && opScore - pScore >= 2)
            {
                winner = 1;
                if (Input.GetMouseButtonUp(0))
                    FSMstate = 4;
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                    FSMstate = 0;
            }
        }
        else if (FSMstate == 4)
        {
            //go to the postmatch scene;
        }
    }

}
