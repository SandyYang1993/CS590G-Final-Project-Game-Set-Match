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
    public string[] servebounds = { "Serve_1" , "Serve_2", "Serve_3", "Serve_4" };
    public AudioClip tennishit;
    public AudioClip tennisbounce;
    private AudioSource source;

    void Start()
    {
        score = 0;
        shotType = 0;
        //hitter = 0;
        fulfill = false;
        serveindex = -1;
        tennishit = MakeSubclip(tennishit, 0.020f, 0.15f);
        tennisbounce = MakeSubclip(tennisbounce, 0.010f, 0.070f);
        source = GetComponent<AudioSource>();
    }
    private AudioClip MakeSubclip(AudioClip clip, float start, float stop)
    {
        /* Create a new audio clip */
        int frequency = clip.frequency;
        float timeLength = stop - start;
        int samplesLength = (int)(frequency * timeLength);
        AudioClip newClip = AudioClip.Create(clip.name + "-sub", samplesLength, 1, frequency, false);
        /* Create a temporary buffer for the samples */
        float[] data = new float[samplesLength];
        /* Get the data from the original clip */
        clip.GetData(data, (int)(frequency * start));
        /* Transfer the data to the new clip */
        newClip.SetData(data, 0);
        /* Return the sub clip */
        return newClip;
    }
    public void PlayHitSound()
    {
        source.PlayOneShot(tennishit);
    }

    // Update is called once per frame
    void Update()
    {
        if(serveindex == -1)
        {
            if (transform.position.x > 4.77f && transform.position.z > -3.2f)
                serveindex = 0;
            else if (transform.position.x > 4.77f && transform.position.z < -3.2f)
                serveindex = 1;
            else if (transform.position.x < 4.77f && transform.position.z > -3.2f)
                serveindex = 3;
            else
                serveindex = 2;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Bot"))
            source.PlayOneShot(tennisbounce);
        if (score != 0) return;
        if(shotType == 0)
        {
            if (other.CompareTag("Player") || other.CompareTag("Bot"))
                return;
            else if(!fulfill)
            {
                //Debug.Log(transform.position);
                //Debug.Log(other.tag);
                if (other.CompareTag(servebounds[serveindex]))
                {
                    fulfill = true;
                    Debug.Log("true");
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
            if (other.CompareTag("Out") || other.CompareTag("Wall") || other.CompareTag("Net"))
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
            else if (other.CompareTag("In_p")|| other.CompareTag(servebounds[2]) || other.CompareTag(servebounds[3]))
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
            else if (other.CompareTag("In_op") || other.CompareTag(servebounds[0]) || other.CompareTag(servebounds[1]))
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
