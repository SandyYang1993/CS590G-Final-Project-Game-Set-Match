using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.Distributions;

public class Bot : MonoBehaviour
{
    public int FSMstate = 0; //3 states, 0:returning to middle position; 1:moving to catch the ball; 2:hitting the ball
    public int moveState = 0;
    private float[] Speeds = { 0.0f, 2.0f, 1.0f, 5.0f };
    public Vector3 Speed = new Vector3(0, 0, -1);
    public Vector3 Direction = new Vector3(0, 0, 1);
    public Vector3 Angle = new Vector3(0, 90, 0);
    public int angleindex = 0;
    public float[] Angles = { 90.0f, 135.0f, 45.0f, 0.0f, -180.0f,  -90.0f, -135.0f, -45.0f };
    public float MouseX = 0;
    public float MouseY = 0;
    public string[] Shots = { "BackhandUpswing", "ForehandUpswing", "BackhandChop", "ForehandChop", "Serve" };
    public float std = 0.02f;
    public int hittercode;
    public float middlereturn = -3.2f;
    public float middlearound = 0.5f;
    public float frontreturn = -1.5f;
    public float backreturn = -4.0f;
    public int framecount = 0;
    public int framerate = 5;
    public float catchspeed = 5.0f;
    public float ball_around = 0.2f;
    public float g = 9.8f;
    public float ybase = 4.52f;
    public float decay_1 = 0.7f;
    public float decay_2 = 0.4f;
    public float xback = -3.13f;
    public float xfront = 4.77f;
    public float zleft = -6.28f;
    public float zright = -0.12f;
    public float[] xrange = { 7.45f, 10.0f, 12.45f };
    public float[] zrange = { -0.12f, -2.1f, -4.2f, -6.28f };
    public float MouseXbase = 15.0f;
    public float MouseYbase = 8.0f;
    public bool serve;

    //public float guessspeedstd = 0.03f;

    public bool hitting = false; // boolean to know if we are hitting the ball or not 

    public Transform ball; // the ball 
    Animator animator;
    public GameObject GameManager;

    ShotManager shotManager; // reference to the shotmanager component
    Shot currentShot; // the current shot we are playing to acces it's attributes

    void Start()
    {
        animator = GetComponent<Animator>();
        shotManager = GetComponent<ShotManager>();
        hittercode = 2;
        GameManager = GameObject.FindWithTag("Manager");
        ball = null;
        serve = false;
    }

    // Update is called once per frame
    void Update()
    {
        //upper FSM logic: 
        //for state 0,3,4, the bot stay idle and not allow to do anything
        //for state 1, if it is bot's turn to serve, then try to serve;
        //for state 2, enter ,low level AI FSM
        if (GameManager.GetComponent<GameManager>().FSMstate == 0 || GameManager.GetComponent<GameManager>().FSMstate == 3 || GameManager.GetComponent<GameManager>().FSMstate == 4)
        {
            moveState = 0;
            Direction = new Vector3(0, 0, 1);
            Angle = new Vector3(0, Angles[0], 0);
            animator.SetInteger("MovementState", moveState);
            Speed = Direction.normalized * Speeds[moveState];
            transform.Translate(Speed * Time.deltaTime);
            transform.eulerAngles = Angle;
            return;
        }
        else if (GameManager.GetComponent<GameManager>().FSMstate == 1)
        {
            if (ball == null)
                return;
            if (ball.GetComponent<Ball>().hitter == 0)
            {
                moveState = 0;
                Direction = new Vector3(0, 0, 1);
                Angle = new Vector3(0, Angles[0], 0);
                animator.SetInteger("MovementState", moveState);
                Speed = Direction.normalized * Speeds[moveState];
                transform.Translate(Speed * Time.deltaTime);
                transform.eulerAngles = Angle;
                return;
            }
            else if (ball.GetComponent<Ball>().hitter == 3)
            {
                Vector2 servetarget;
                Vector3 servedir;
                float servetdis;
                if (serve)
                {
                    animator.Play("ServePrepare");
                    serve = false;
                }
                if(ball.position.y <=6.1f && ball.GetComponent<Rigidbody>().velocity.y<0)
                {
                    if(transform.position.z<-3.2f)
                    {
                        servetarget = new Vector2(Random.Range(7.45f, 8.93f), Random.Range(-3.2f, -0.12f));
                    }
                    else
                    {
                        servetarget = new Vector2(Random.Range(7.45f, 8.93f), Random.Range(-6.28f, -3.2f));
                    }
                    servedir = new Vector3(servetarget.x - ball.position.x, 0.0f, servetarget.y - ball.position.z);
                    //servetdis = servedir.magnitude;
                    MouseY = servedir.x / 11.80f * MouseYbase;
                    MouseX = MouseY / servedir.x * servedir.z * 5.0f;
                    CastShot(4);
                    animator.Play(Shots[4]);
                }
                return;
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Shot")) return; //when performing strokes, stop update;
        //FSM logic
        if(FSMstate == 0)
        {
            if (ball.GetComponent<Ball>().hitter == 1)
                FSMstate = 1;
        }
        else if(FSMstate == 1)
        {
            if (hitting)
            {
                framecount = 0;
                FSMstate = 2;
            }
        }
        else if(FSMstate == 2)
        {
            if(ball.GetComponent<Ball>().hitter == hittercode)
                FSMstate = 0;
        }

        if (FSMstate == 0)
            ReturnToMiddle();
        else if (FSMstate == 1)
            MoveToCatch();
        else if (FSMstate == 2)
            HitTheBall();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
            hitting = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
            hitting = false;
    }
    //jog to the middle point(z = -3.2) and reasonable range for next hit(-1.5>x>-4.0)
    internal void ReturnToMiddle()
    {
        if(transform.position.x<backreturn)
        {
            if(transform.position.z<middlereturn-middlearound)
            {
                moveState = 1;
                angleindex = 2;
            }
            else if(transform.position.z> middlereturn + middlearound)
            {
                moveState = 1;
                angleindex = 1;
            }
            else
            {
                moveState = 1;
                angleindex = 0;
            }
            Direction = new Vector3(0, 0, 1);
        }
        else if(transform.position.x > frontreturn)
        {
            if (transform.position.z < middlereturn - middlearound)
            {
                moveState = 2;
                angleindex = 1;
            }
            else if (transform.position.z > middlereturn + middlearound)
            {
                moveState = 2;
                angleindex = 2;
            }
            else
            {
                moveState = 2;
                angleindex = 0;
            }
            Direction = new Vector3(0, 0, -1);
        }
        else
        {
            if (transform.position.z < middlereturn - middlearound)
            {
                moveState = 1;
                angleindex = 3;
            }
            else if (transform.position.z > middlereturn + middlearound)
            {
                moveState = 1;
                angleindex = 4;
            }
            else
            {
                moveState = 0;
                angleindex = 0;
            }
            Direction = new Vector3(0, 0, 1);
        }
        Angle = new Vector3(0, Angles[angleindex], 0);
        animator.SetInteger("MovementState", moveState);
        Speed = Direction.normalized * Speeds[moveState];
        transform.Translate(Speed * Time.deltaTime);
        transform.eulerAngles = Angle;
    }
    
    //update moving state every 5 frames
    //set two lines around the ball trail, calculate the point when these three lines and the moving directions intersect
    //campare these points, find a feasible point with the most early timestamp; 
    internal void MoveToCatch()
    {
        //if (framecount % framerate != 0)
        //    return;
        //framecount++;
        Vector2 bot = new Vector2(transform.position.x, transform.position.z);
        Vector2 bal_mid = new Vector2(ball.position.x, ball.position.z);
        Vector2 bal_left = new Vector2(ball.position.x, ball.position.z-ball_around);
        Vector2 bal_right = new Vector2(ball.position.x, ball.position.z + ball_around);
        Vector2 bal_speed = new Vector2(ball.GetComponent<Rigidbody>().velocity.x, ball.GetComponent<Rigidbody>().velocity.z);
        bal_speed.x = Guess(bal_speed.x);
        bal_speed.y = Guess(bal_speed.y);
        Vector2[] bals = { bal_left, bal_mid, bal_right };
        //test if the bot is already in the bal trail, if so stop moving
        Vector2 test_1 = IntersectHorizontal(bot, bals[0], bal_speed);
        Vector2 test_2 = IntersectHorizontal(bot, bals[2], bal_speed);
        if(bot.x<bal_mid.x && test_1.y* test_2.y<0.0f)
        {
            moveState = 0;
            Direction = new Vector3(0, 0, 1);
            Angle = new Vector3(0, Angles[0], 0);
            animator.SetInteger("MovementState", moveState);
            Speed = Direction.normalized * Speeds[moveState];
            transform.Translate(Speed * Time.deltaTime);
            transform.eulerAngles = Angle;
            return;
        }

        Vector2 test;
        float bal_time = 100.0f;
        //float bot_time = 100.0f;

        foreach(Vector2 bal in bals)
        {
            test = IntersectHorizontal(bot, bal, bal_speed);
            if(test.x>0.0f && Mathf.Abs(test.y)>test.x && bal.x+test.x* bal_speed.x<4.77f)
            {
                if (test.x < bal_time)
                {
                    bal_time = test.x;
                    if (test.y >= 0.0f)
                        angleindex = 3;
                    else
                        angleindex = 4;
                }
            }
            test = IntersectVertical(bot, bal, bal_speed);
            if (test.x > 0.0f && Mathf.Abs(test.y) > test.x && bal.x + test.x * bal_speed.x < 4.77f)
            {
                if (test.x < bal_time)
                {
                    bal_time = test.x;
                    if (test.y >= 0.0f)
                        angleindex = 0;
                    else
                        angleindex = 5;
                }
            }
            test = IntersectDiagonal_1(bot, bal, bal_speed);
            if (test.x > 0.0f && Mathf.Abs(test.y) > test.x && bal.x + test.x * bal_speed.x < 4.77f)
            {
                if (test.x < bal_time)
                {
                    bal_time = test.x;
                    if (test.y >= 0.0f)
                        angleindex = 2;
                    else
                        angleindex = 6;
                }
            }
            test = IntersectDiagonal_2(bot, bal, bal_speed);
            if (test.x > 0.0f && Mathf.Abs(test.y) > test.x && bal.x + test.x * bal_speed.x < 4.77f)
            {
                if (test.x < bal_time)
                {
                    bal_time = test.x;
                    if (test.y >= 0.0f)
                        angleindex = 7;
                    else
                        angleindex = 1;
                }
            }
        }
        if(bal_time == 100.0f)//no way to catch the ball, time to give up
        {
            moveState = 0;
            Direction = new Vector3(0, 0, 1);
            Angle = new Vector3(0, Angles[0], 0);
            animator.SetInteger("MovementState", moveState);
            Speed = Direction.normalized * Speeds[moveState];
            transform.Translate(Speed * Time.deltaTime);
            transform.eulerAngles = Angle;
            return;
        }
        else
        {
            moveState = 3;
            Direction = new Vector3(0, 0, 1);
            Angle = new Vector3(0, Angles[angleindex], 0);
            animator.SetInteger("MovementState", moveState);
            Speed = Direction.normalized * Speeds[moveState];

            transform.Translate(Speed * Time.deltaTime);
            transform.eulerAngles = Angle;
        }

    }
    //first we check if the ball has fulfilled, if not then predict if it is going to be out of bounds, if so we give up, if not we continue
    //then we get 6 points randomly each from different area, choose one of them (increase weight for the far away points, decrease weight for the points that are too close to the net)
    //calculate the direction, distanse of our shot, blurry them with normal distribution
    //calculate the MouseX and MouseY value with regard to some known value, invoke CastShot function to cast a shot
    //set the animation, speed, angle
    internal void HitTheBall()
    {
        if(!ball.GetComponent<Ball>().fulfill && CheckoutofBound())//predict that the ball will go out of bounds, do nothing
        {
            moveState = 0;
            Direction = new Vector3(0, 0, 1);
            Angle = new Vector3(0, Angles[0], 0);
            animator.SetInteger("MovementState", moveState);
            Speed = Direction.normalized * Speeds[moveState];
            transform.Translate(Speed * Time.deltaTime);
            transform.eulerAngles = Angle;
            return;
        }
        Vector3 position = ball.position;
        Vector2[] targets = new Vector2[6];
        float[] targetdis = new float[6];
        int i = 0;
        for(int j = 0;j<3;j++)
        {
            for(int k = 0;k<2;k++)
            {
                targets[i] = new Vector2(Random.Range(xrange[k], xrange[k + 1]), Random.Range(zrange[j], zrange[j + 1]));
                targetdis[i] = Mathf.Sqrt((targets[i].x - position.x) * (targets[i].x - position.x) + (targets[i].y - position.z) * (targets[i].y - position.z));
                if (targets[i].x >= 9.0f) targetdis[i] = targetdis[i] * targetdis[i];
                i++;
            }
        }
        float sum = 0.0f;
        foreach (float dis in targetdis)
            sum += dis;
        sum = Random.Range(0.0f, sum);
        i = 0;
        for(i = 0;i<6;i++)
        {
            if (sum <= targetdis[i])
                break;
            else
                sum -= targetdis[i];
        }
        Vector2 target = targets[i];//choose a target;
        Vector3 shotdir = new Vector3(target.x - position.x, 0.0f, target.y - position.z);
        float shotdis = shotdir.magnitude;
        shotdis = Guess(shotdis);
        shotdir.x = Guess(shotdir.x);
        shotdir.z = Guess(shotdir.z);
        //shotdir = shotdir.normalized;
        int shotType;
        if (shotdis<=7.5f && position.y>=4.9f)
        {
            if (ball.position.z <= transform.position.z)
                shotType = 3;
            else
                shotType = 2;
        }
        else
        {
            if (ball.position.z <= transform.position.z)
                shotType = 1;
            else
                shotType = 0;
        }
        if (shotType == 0 || shotType == 1)
        {
            MouseY = shotdir.x / 15.58f * MouseYbase;
            MouseX = MouseY / shotdir.x * shotdir.z * 5.0f;
        }
        else
        {
            MouseY = shotdir.x / 7.50f * MouseYbase;
            MouseX = MouseY / shotdir.x * shotdir.z * 5.0f;
        }
        CastShot(shotType);
        moveState = 0;
        Direction = new Vector3(0, 0, 1);
        Angle = new Vector3(0, Angles[0], 0);
        animator.Play(Shots[shotType]);
        Speed = Direction.normalized * Speeds[moveState];
        transform.Translate(Speed * Time.deltaTime);
        transform.eulerAngles = Angle;
        return;
    }
    internal bool CheckoutofBound()
    {
        Vector3 position = ball.position;
        Vector3 v = ball.GetComponent<Rigidbody>().velocity;
        float t = (v.y + Mathf.Sqrt(v.y * v.y + 2 * g * (position.y - ybase))) / g;
        if(ball.GetComponent<Ball>().shotType == 0 || ball.GetComponent<Ball>().shotType == 1)
        {
            t = decay_1 * t;
        }
        else
        {
            t = decay_2 * t;
        }
        float x = position.x + v.x * t;
        float z = position.z + v.z * t;
        if (x <= xfront && x >= xback && z <= zright && z >= zleft)
            return false;
        else return true;
    }
    internal void CastShot(int shotType)
    {
        ball.GetComponent<Ball>().PlayHitSound();
        Vector3 position = ball.position;
        if ((shotType == 0 || shotType == 1) && position.y < 5.2f)
            position.y = 5.2f;
        if (shotType == 4 && position.y < 5.9f)
            position.y = 5.9f;
        MouseX = MouseX / 5.0f;
        if (MouseX > 3.0f) MouseX = 3.0f;
        if (MouseX < -3.0f) MouseX = -3.0f;
        if (MouseY > 8.0f) MouseY = 8.0f;
        if (MouseY < -8.0f) MouseY = -8.0f;
        Normal normalDistX = new Normal(MouseX, std);
        MouseX = (float)normalDistX.Sample();
        Normal normalDistY = new Normal(MouseY, std);
        MouseY = (float)normalDistY.Sample();
        Vector3 ballDir = new Vector3(Mathf.Abs(MouseY), 0, MouseX);
        //Debug.Log(ballDir);
        ballDir = ballDir.normalized;
        float force = Mathf.Sqrt(MouseX * MouseX + MouseY * MouseY);
        float upForce;
        float hitForce;
        float drag;
        Shot current;
        if (shotType == 0 || shotType == 1)
            current = shotManager.upSwing;
        else if (shotType == 2 || shotType == 3)
            current = shotManager.chop;
        else
            current = shotManager.serve;

        upForce = force * current.upForce;
        hitForce = force * current.hitForce + ball.GetComponent<Rigidbody>().velocity.magnitude * current.reflection;
        drag = current.drag;
        if (shotType == 0 || shotType == 1)
        {
            if (upForce > 3.0f) upForce = 3.0f;
        }
        else
        {
            if (upForce > 4.5f) upForce = 4.5f;
        }
        if (shotType != 4)
        {
            if (position.y > 5.2f)
            {
                upForce = upForce - (position.y - 5.2f) / 0.6f * (upForce * 2.0f / 3.0f);
                hitForce = hitForce + (position.y - 5.2f) / 0.6f * (hitForce / 2.0f);
            }
            if (position.y < 5.2f)
            {
                upForce = upForce + (5.2f - position.y) / 0.7f * (upForce / 3.0f);
                hitForce = hitForce - (5.2f - position.y) / 0.7f * (hitForce / 3.0f);
            }
        }
        else
        {
            upForce = upForce - (position.y - 5.6f) / 0.6f * 0.1f;
            hitForce = hitForce + (position.y - 5.6f) / 0.6f * (hitForce / 6.0f);
        }
        //Debug.Log(force);
        //Debug.Log(upForce);
        //Debug.Log(hitForce);
        //Debug.Log(ballDir);

        ball.position = position;
        ball.GetComponent<Rigidbody>().velocity = ballDir * hitForce + new Vector3(0, upForce, 0);
        ball.GetComponent<Rigidbody>().drag = drag;
        ball.GetComponent<Ball>().hitter = hittercode;
        ball.GetComponent<Ball>().fulfill = false;
        if (shotType == 0 || shotType == 1)
            ball.GetComponent<Ball>().shotType = 1;
        else if (shotType == 2 || shotType == 3)
            ball.GetComponent<Ball>().shotType = 2;
        else
            ball.GetComponent<Ball>().shotType = 0;
    }

    //Guess a float base on normal distribution
    internal float Guess(float original)
    {
        Normal normal = new Normal(original, std);
        return (float)normal.Sample();
    }
    internal Vector2 IntersectHorizontal(Vector2 bot, Vector2 bal, Vector2 bal_speed)
    {
        if (bal_speed.x == 0.0f) return new Vector2(-1.0f, 0.0f);
        float bal_time, bot_time;
        bal_time = (bot.x - bal.x) / bal_speed.x;
        bot_time = (bal.y + bal_speed.y * bal_time - bot.y) / catchspeed;
        return new Vector2(bal_time, bot_time);
    }
    internal Vector2 IntersectVertical(Vector2 bot, Vector2 bal, Vector2 bal_speed)
    {
        if (bal_speed.y == 0.0f) return new Vector2(-1.0f, 0.0f);
        float bal_time, bot_time;
        bal_time = (bot.y - bal.y) / bal_speed.y;
        bot_time = (bal.x + bal_speed.x * bal_time - bot.x) / catchspeed;
        return new Vector2(bal_time, bot_time);
    }
    internal Vector2 IntersectDiagonal_1(Vector2 bot, Vector2 bal, Vector2 bal_speed)
    {
        if (bal_speed.x == bal_speed.y) return new Vector2(-1.0f, 0.0f);
        float bal_time, bot_time;
        bal_time = (bot.x - bot.y - bal.x + bal.y) / (bal_speed.x - bal_speed.y);
        bot_time = (bal.x + bal_speed.x * bal_time - bot.x) / catchspeed * Mathf.Sqrt(2.0f);
        return new Vector2(bal_time, bot_time);
    }
    internal Vector2 IntersectDiagonal_2(Vector2 bot, Vector2 bal, Vector2 bal_speed)
    {
        if (bal_speed.x + bal_speed.y == 0) return new Vector2(-1.0f, 0.0f);
        float bal_time, bot_time;
        bal_time = (bot.x + bot.y - bal.x - bal.y) / (bal_speed.x + bal_speed.y);
        bot_time = -(bal.x + bal_speed.x * bal_time - bot.x) / catchspeed * Mathf.Sqrt(2.0f);
        return new Vector2(bal_time, bot_time);
    }
}
