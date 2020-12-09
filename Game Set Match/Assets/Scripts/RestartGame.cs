using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GameManager;


public class RestartGame : MonoBehaviour
{
    public Text summary;
    public Text rule;
    private string[] summaries = { "You Won!", "You Lost!" };
    private string[] rules = { "Good job. Now try it again.", "Tough luck. The bot prevailed this time." };
    public void changeScene(){
    	SceneManager.LoadScene("StartUpScene");
    }
    void Start()
    {
        if(GameManager.winner == 0)
        {
            summary.text = summaries[0];
            rule.text = rules[0];
        }
        else
        {
            summary.text = summaries[1];
            rule.text = rules[1];
        }
    }
}
