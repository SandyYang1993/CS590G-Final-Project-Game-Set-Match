# CS590G-Final-Project-Game-Set-Match
This is a tennis game with normal tennis rules. Player can choose different characters, different court types before entering the game. The core is a tennis game to 7, Player vs Bot. Please check the in game instructions on how to play. This document will focus on describing developing details of this game.
## Qifan(Sandy) Yang Contribution
### Game flow
The game flow of the core game is controlled by a upper level FSM defined in GameManager.cs(attached to GameManager object).The states are defined as follows:
* Set(0): Preparing state before each point. Place Player and Bot in correct position based on serving rotation. Both of them cannot move. Destroy the previous ball.
  * To Serve(1): player leftclick;
* Serve(1): Initialize a new ball and throw it to the sky. Wait for player or bot to serve. Player or Bot can only stay idle or serve the ball, no other movements allowed.
  * To Play(2): Player or Bot have served the ball;
  * To Score(3): Player or Bot failed to serve the ball;(no second chance)
* Play(2): Player and Bot enter normal logic and start playing.
  * To Score(3): Player or Bot scored;
* Score(3): Update the score. Player and Bot cannot move.
  * To Set(0): player leftclick;(if no one has enough points to win)
  * To End(4): player leftclick;(someone has enough points to win)
* End(4): Game ends. Go to the ending scene.
### Game rules
The tennis rules are enforced in Ball.cs(attached to Ball prefab). It is basically done by creating several trigger on the court and using the OnTriggerEnter function.
### Player control and game mechanism(Player.cs attached to Player prefab)
* Player movement control and movement animations:
  * This part is finished by cooperation of Sandy and Mayur;
  * Movement animations are online free assets;
  * Player have 4 movement states: idle, jog, jogback, sprint, each with different speed;
  * Player can move to 8 directions;
  * The control is based on the lower level animator FSM;
* Player shots animation
  * There 5 animations: ForehandUpSwing, BackhandUpSwing, ForehandChop, BackhandChop, Serve;
  * There are no free assets available online. So all the animations above are handmade using the predifined motions which come with the Humanoid asset.
* Player shots mechanism
  * The shots type(chop or upswing), shot direction, shot force are all controlled by mouse movement(left click and swing the mouse).
  * Shot parameters are defined in ShotManager.cs(upforce,hitforce, reflection,drag)
  * The force and direction input by mouse movement is first blurred by sampling from normal distribution(MathNet package);
    * Vertical velocity = force* shot.upforce;
    * Horizontal velocity = force* shot.hitforce + original velocity*shot.reflection;
    * Drag = shot.drag;
    * Then a mechanism is implemented to adjust the Vertical and Horizontal velocities based on the ball position(if the ball is high, increase horizontal velocity and decrease vertical velocity, vise versa)
  * Many trials have been performed to make it quite easy (but not always) to hit the ball in bound.
* Player.cs is the first part of development I worked on and so many things have not been decide at that time, as a result the code is quite messy.
### Game AI(Bot.cs attached to Bot prefab)
The Game AI is basically a middle level FSM. The states are defined as follows:
* Return(0): Bot finished hitting the ball, start to return to a middle region. Lower level animator FSM is used to control the movement.
  * To Catch(1): Player has hit the ball;
* Catch(1): Bot start to catch the player's shot.
  * Bot can only guess the velocity of the ball. Again this is done by sampling from normal distribution;
  * Calculate intersactions of ball trajectory(guessed) and 8 moving directions of Bot(actually we only have to consider 4 of them), choose the most early intersection among the feasible ones(the bot can beat the ball to location). If there are no feasible ones, give up and stay idle.
  * Set the movement and speed via lower level animator FSM.
  * The actual algorithm is more complicated than stated above but that is the main idea;
  * To Hit(2): the ball has entered Bot trigger;
* Hit(2): Bot generates a shot to hit the ball back.
  * First guess if the ball will be out of bounds, if so do nothing; if not prepare a shot;
  * Randomly generates 6 target points each from different region of the player court;
  * Based on the distanse to Player, give more weight to the far away points; the points that are too close to the net will get a smaller weight(reduce the possibility of the ball hitting the net)
  * Randomly choose one of the points as the targets, calculate the force and direction(use empirial method instead of physics), use the same way to cast a shot as in Player.cs
  * To Return(0): the shot has been performed
### Sound Effect and UI(prompts and scores)
* This part is finished by cooperation of Sandy and Mayur;
* Sound Effect uses free audio assets;
* Prompts and scores are enforced in GameManager.cs;

## Mayur Shastri Contribution
### Charecter Selection
