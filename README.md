# CS590G-Final-Project-Game-Set-Match
The game we have built is a tennis game with normal tennis rules. The player will control one of the Character provided to play against a game AI controlled opponent in a tennis court. Both the players and AI controlled opponent will be able to move freely around the court with the singular goal of hitting the ball in a manner such that it cannot be returned. The goal is to win the game based on standard tennis rules. The idea is to make it as realistic to an actual tennis game simulation as possible with an array of difficulty modes that change how well the AI player plays.

Player can choose different characters, different court types before entering the game. The core is a tennis game to 7, Player vs Bot. Please check the in game instructions on how to play. This document will focus on describing developing details of this game.

## Qifan (Sandy) Yang Contribution
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
### Charecter and Court Selection(assistance)
* Help to instantiate game objects in the background of the selection screen so that the user choices are reflected in real time.
### __New:__ Player Super Powers
* The super powers require energy to release. The energy bar is added at the bottom left of the screen. Successfully hitting the ball will increase the energy by 25%. Using the super power will require 100% energy level.
* The red character have a teleport power. The user can press space and any of W,A,S,D to release it. There will be a partical effect appear at the original position, but somehow it always comes late.
* The blue character have an auto upswing power. The user can press space while the ball is in the charater's trigger area. The character will perform an upswing to hit the ball to the corner far away from the bot. While performing it, the racket of the player will change color.
## Mayur Shastri Contribution
### Charecter and Court Selection
The user is given an opporutnity to choose from a selection of avatars (player types) and courts before starting the game. The idea of these pages are to give a sense of customizability to the user. Later iterations of the game would build on this to be able to give different charecteristics and strengths to the different avatars. As the game AI develops, the different courts can have different charecteristics that affects the way the ball bounces on the courts. 

* Both Charecter Selection and Court Selection scripts use PlayerPrefs to store user choice. 
  * CharecterInt and OpponentCharecterInt are variables used in the CharecterSelection script.
  * They are simple integer objects that is used to denote the choice of the user.
  * The choices are toggled using a switch case.
  * CourtInt is used in the CourtSelection script and uses a similar logic to the above to store the choice of the user.

### Player animation control and Game mechanism
After selecting a skeleton mesh to embody player movement, we had to build the player movement up from scratch. Among the different motion animations freely available, we chose ones that worked well witht the asset we had and worked on building the animaition controller to handle player movement. This part is finished by cooperation of Sandy and Mayur

* Player movement control and movement animations:
  * Movement animations are online free assets;
  * Player have 4 movement states: idle, jog, jogback, sprint, each with different speed;
  * Player can move to 8 directions;
  * The control is based on the lower level animator FSM;
* Player shots animation
  * There 5 animations: ForehandUpSwing, BackhandUpSwing, ForehandChop, BackhandChop, Serve;
  * There are no free assets available online. So all the animations above are handmade using the predifined motions which come with the Humanoid asset.  

### 3D World and Charecters

Since the game required a playground upon which the entire game could take place, we had to build the complete stadium 3D model from scratch.

* The stadium asset includes:
  * The main court along with the court set to dimensions.
  * A transparent net 
  * The stands model upon which the fans/audience can be seated.

### Sound Effect and UI(prompts and scores)

Different sound assets were searched for and chosen to be used in the game. This part is finished by cooperation of Sandy and Mayur.

The sound animations used and integrated are:
* Sounds for the swing animations to be used when the ball is hit by either the player or the bot
* Sounds for whenever the ball bounes on the ball
*  Sounds for artificial crowd noise which cheers and boos whenever the player wins or loses the point.
* Sound Effect uses free audio assets;
* Prompts and scores are enforced in GameManager.cs;

## Final Thoughts
* Sports games can be hard to design and implement in that:
  * Special animation required: we cannot found free animation assets for tennis so we have to handmade them.
  * Maybe better physics engine required: while testing, I noticed that sometimes the onTriggerEnter function did not work which broke the tennis rules. After countless testings, I finally found out it is probabaly because the ball was bouncing to quick from the ground and it escaped fixed time update.(I have two sets of colliders for the court, one for physics material, one for checking bounds) I changed the fix update time to 0.005s and raise the trigger colliders up but the issue can still happen sometimes.
  * The AI can be hard to design: For this game, the function that drives Bot to catch the ball is the hardest one. The algorithm I described above roughly solve the problem, but the Bot would look very twinkling because it is calculating each frame. I changed it to update its moving state every 20 frames but it still looks unhuman.
* Finally, the game is not quite successful largely due to the control design (click the button, swing it and release to perform a shot) which is simply too hard to perform in real time. In the end I have to increase the space of the player trigger collider and slow the time to 0.5 to get a chance against Bot. It can be really shameful to cheat in my own game.
