using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject avatar1;
    //public GameObject avatar2;
    public GameObject avatar3;
    public GameObject avatar4;
    public GameObject opponentAvatar1;
    public GameObject opponentAvatar2;
    public GameObject player_3;
    public GameObject player_4;
    public GameObject bot_1;
    public GameObject bot_2;
    //public GameObject opponentAvatar3;
    //public GameObject opponentAvatar4;
    private Vector3 CharecterPosition;
    private Vector3 OffScreen;
   	public static bool Charecter;
    private Vector3 OpponentCharecterPosition;
    private Vector3 OpponentOffScreen;
    public static bool OpponentCharecter;
    private readonly string playerAvatar = "playerAvatar";
    private readonly string opponentAvatar = "opponentAvatar";
    private string[] playertexts = { "Red is a great defensive player. His super power is teleport. Press space while moving character to release it.", "Blue is a fierce offensive player. His super power is a massive upswing. Press space to release it. It will automatically strike the ball to the far corner." };
    private string[] bottexts = { "Yellow is the standard opponent, a average player.", "Orange is a better opponent, with lower turnover rate and quicker running speed." };
    public Text playertext;
    public Text bottext;
    public Vector3 playerlocation = new Vector3(413.428f, 196.727f, -236.66f);
    public Vector3 botlocation = new Vector3(410.35f, 196.727f, -220.88f);
    public GameObject player;
    public GameObject bot;
    // playerAvatar Map 
    // 1 -> avatar2
    // 2 -> avatar3
    // 3 -> avatar4
    // 4 -> avatar1

    // opponentAvatar Map 
    // 1 -> avatar2
    // 2 -> avatar3
    // 3 -> avatar4
    // 4 -> avatar1

    private void Awake(){

        //avatar1.SetActive(true);
        //avatar2.SetActive(false);
        Charecter = true;
        OpponentCharecter = true;
        avatar3.SetActive(true);
        avatar4.SetActive(false);
        opponentAvatar1.SetActive(true);
        opponentAvatar2.SetActive(false);
        //opponentAvatar3.SetActive(false);
        //opponentAvatar4.SetActive(false);
        playertext.text = playertexts[0];
        bottext.text = bottexts[0];
        Instantiate(player_3, playerlocation, Quaternion.identity);
        Instantiate(bot_1, botlocation, Quaternion.identity);
        player = GameObject.FindWithTag("Player");
        bot = GameObject.FindWithTag("Bot");
        bot.GetComponent<Transform>().eulerAngles = new Vector3(0,180,0);
    }
    void Update()
    {
        if(player == null)
            player = GameObject.FindWithTag("Player");
        if(bot == null)
        {
            bot = GameObject.FindWithTag("Bot");
            bot.GetComponent<Transform>().eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void NextCharecter(){

        // Vector3 offset = new Vector3(300.0f,0,0);
        //Debug.Log(Charecter);
        if(Charecter)
        {
            avatar3.SetActive(false);
            avatar4.SetActive(true);
            playertext.text = playertexts[1];
            Destroy(player);
            Instantiate(player_4, playerlocation, Quaternion.identity);
            //player = GameObject.FindWithTag("Player");
        }
        else
        {
            avatar3.SetActive(true);
            avatar4.SetActive(false);
            playertext.text = playertexts[0];
            Destroy(player);
            Instantiate(player_3, playerlocation, Quaternion.identity);
            //player = GameObject.FindWithTag("Player");
        }
        Charecter = !Charecter;


     //   switch (CharecterInt){
    	//	case 1:
     //           PlayerPrefs.SetInt(playerAvatar,1);
    	//		avatar1.SetActive(false);
    	//		avatar2.SetActive(true);
    	//		CharecterInt++;
    	//		break;
    	//	case 2:
     //           PlayerPrefs.SetInt(playerAvatar,2);
    	//		avatar2.SetActive(false) ;
    	//		avatar3.SetActive(true);
    	//		CharecterInt++;
    	//		break;
    	//	case 3:
     //           PlayerPrefs.SetInt(playerAvatar,3);
    	//		avatar3.SetActive(false);
    	//		avatar4.SetActive(true);
    	//		CharecterInt++;
    	//		break;
    	//	case 4:
     //           PlayerPrefs.SetInt(playerAvatar,4);
    	//		avatar4.SetActive(false) ;
    	//		avatar1.SetActive(true);
    	//		ResetInt();
    	//		break;
    	//	default:
    	//		ResetInt();
    	//		break;
    	//}

    }

    public void PrevCharecter(){
        
    	//switch(CharecterInt){
    	//	case 1:
     //           PlayerPrefs.SetInt(playerAvatar,4);
    	//		avatar2.SetActive(false) ;
    	//		avatar1.SetActive(true);
    	//		ResetInt();
    	//		break;
    	//	case 2:
     //           PlayerPrefs.SetInt(playerAvatar,1);
    	//		avatar3.SetActive(false) ;
    	//		avatar2.SetActive(true);
    	//		CharecterInt--;
    	//		break;
    	//	case 3:
     //           PlayerPrefs.SetInt(playerAvatar,2);
    	//		avatar4.SetActive(false) ;
    	//		avatar3.SetActive(true);
    	//		CharecterInt--;
    	//		break;
    	//	case 4:
     //           PlayerPrefs.SetInt(playerAvatar,3);
    	//		avatar1.SetActive(false);
    	//		avatar4.SetActive(true);
    	//		CharecterInt--;
    	//		break;
    	//	default:
    	//		ResetInt();
    	//		break;
    	//}
    	
    }

    public void OpponentNextCharecter(){

        if (OpponentCharecter)
        {
            opponentAvatar1.SetActive(false);
            opponentAvatar2.SetActive(true);
            bottext.text = bottexts[1];
            Destroy(bot);
            Instantiate(bot_2, botlocation, Quaternion.identity);
            //bot = GameObject.FindWithTag("Bot");
            //bot.GetComponent<Transform>().eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            opponentAvatar1.SetActive(true);
            opponentAvatar2.SetActive(false);
            bottext.text = bottexts[0];
            Destroy(bot);
            Instantiate(bot_1, botlocation, Quaternion.identity);
            //bot = GameObject.FindWithTag("Bot");
            //bot.GetComponent<Transform>().eulerAngles = new Vector3(0, 180, 0);
        }
        OpponentCharecter = !OpponentCharecter;
        //switch(OpponentCharecterInt){
        //    case 1:
        //        PlayerPrefs.SetInt(opponentAvatar,1);
        //        opponentAvatar1.SetActive(false) ;
        //        opponentAvatar2.SetActive(true);
        //        OpponentCharecterInt++;
        //        break;
        //    case 2:
        //        PlayerPrefs.SetInt(opponentAvatar,2);
        //        opponentAvatar2.SetActive(false) ;
        //        opponentAvatar3.SetActive(true);
        //        OpponentCharecterInt++;
        //        break;
        //    case 3:
        //        PlayerPrefs.SetInt(opponentAvatar,3);
        //        opponentAvatar3.SetActive(false) ;
        //        opponentAvatar4.SetActive(true);
        //        OpponentCharecterInt++;
        //        break;
        //    case 4:
        //        PlayerPrefs.SetInt(opponentAvatar,4);
        //        opponentAvatar4.SetActive(false) ;
        //        opponentAvatar1.SetActive(true);
        //        OpponentResetInt();
        //        break;
        //    default:
        //        OpponentResetInt();
        //        break;
        //}

    }

    public void OpponentPrevCharecter(){
        //switch(OpponentCharecterInt){
        //    case 1:
        //        PlayerPrefs.SetInt(opponentAvatar,4);
        //        opponentAvatar2.SetActive(false) ;
        //        opponentAvatar1.SetActive(true);
        //        OpponentResetInt();
        //        break;
        //    case 2:
        //        PlayerPrefs.SetInt(opponentAvatar,1);
        //        opponentAvatar3.SetActive(false) ;
        //        opponentAvatar2.SetActive(true);
        //        OpponentCharecterInt--;
        //        break;
        //    case 3:
        //        PlayerPrefs.SetInt(opponentAvatar,2);
        //        opponentAvatar4.SetActive(false) ;
        //        opponentAvatar3.SetActive(true);
        //        OpponentCharecterInt--;
        //        break;
        //    case 4:
        //        PlayerPrefs.SetInt(opponentAvatar,3);
        //        opponentAvatar1.SetActive(false) ;
        //        opponentAvatar4.SetActive(true);
        //        OpponentCharecterInt--;
        //        break;
        //    default:
        //        OpponentResetInt();
        //        break;
        //}
        
    }

    private void ResetInt(){
    	//if(CharecterInt >= 4){
    	//	CharecterInt = 1;
    	//}
    	//else{
    	//	CharecterInt = 4;
    	//}
    }

    private void OpponentResetInt(){
        //if(OpponentCharecterInt >= 4){
        //    OpponentCharecterInt = 1;
        //}
        //else{
        //    OpponentCharecterInt = 4;
        //}
    }



}
