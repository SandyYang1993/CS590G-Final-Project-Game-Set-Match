using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectChar : MonoBehaviour
{
    // Start is called before the first frame update
    public bool instructions;
    private string[] titles = { "Game, Set, Match!", "Instructions" };
    private string[] summaries = { "Welcome to the future of tennis. This is a next generation tennis game where you the player control and play against humanoids with superhuman capabilities. This is tennis like you have never seen before.", "" };
    private string[] rules = { "1. You will now be directed to chosing a player of your choice. Each player has his own set of abilities that make them unique in their own way.\n2. You also have the choice of choosing the surface on which the match will be played. Standard tennis game rules apply and your player will be able to hit the ball in different ways and make various shots.\n3. Each player also his ability shot that can be used when the energy bar is full.", "1. Game rules: it is a tiebreaker game. Game to 7. Win by 2. Normal tennis rules apply except there is no second chance to serve.\n2. How to serve: press left mouse button, move the mouse and release to serve the ball. How much you move left and right roughly controls the direction of the serve. How much you move the mouse roughly controls the force of the serve.\n3. How to perform a upswing: press left mouse button, move the mouse up and release to perform a upswing. How much you move left and right roughly controls the direction of the shot. How much you move the mouse roughly controls the force of the shot.\n4. How to perform a chop: press left mouse button, move the mouse down and release to perform a chop. How much you move left and right roughly controls the direction of the shot. How much you move the mouse roughly controls the force of the shot.\n5. Difference between upswing and chop: upswing is more powerful and quick, chop can slow down more quickly.\n6. How to sprint: hold left shift when you are moving your character." };
    private string[] buttons = { "How To Play", "Back" };
    public Text title;
    public Text summary;
    public Text rule;
    public Text button;
    void Start()
    {
        instructions = false;
        title.text = titles[0];
        summary.text = summaries[0];
        rule.text = rules[0];
        button.text = buttons[0];
    }
    public void changeScene() {
        SceneManager.LoadScene("CharechterSelection");
    }
    public void showInstruction()
    {
        if(instructions)
        {
            title.text = titles[0];
            summary.text = summaries[0];
            rule.text = rules[0];
            button.text = buttons[0];
        }
        else
        {
            title.text = titles[1];
            summary.text = summaries[1];
            rule.text = rules[1];
            button.text = buttons[1];
        }
        instructions = !instructions;
    }
}
