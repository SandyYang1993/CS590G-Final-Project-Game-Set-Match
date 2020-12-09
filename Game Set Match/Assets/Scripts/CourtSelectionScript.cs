using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SelectionScript;

public class CourtSelectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player_3;
    public GameObject player_4;
    public GameObject bot_1;
    public GameObject bot_2;
    public Vector3 playerlocation = new Vector3(413.428f, 196.727f, -236.66f);
    public Vector3 botlocation = new Vector3(410.35f, 196.727f, -220.88f);
    public GameObject court1;
    //public GameObject court2;
    public GameObject court3;
    //public GameObject court4;
    private Vector3 CourtPosition;
    private Vector3 OffScreen;
   	public static bool Court;
    //private readonly string courtType = "courtType";
    private string[] courts = { "Hard", "Clay" };
    private string[] instructions = { "Hard court is more bouncy.", "Clay court is less bouncy." };
    public Text courttype;
    public Text instruction;
    public Material hard;
    public Material clay;
    // Court Types Map
    // 1 -> court2
    // 2 -> court3
    // 3 -> court4
    // 4 -> court1
    public GameObject court;

    private void Awake(){
        Court = true;

        court1.SetActive(true);
        
        court3.SetActive(false);
        courttype.text = courts[0];
        instruction.text = instructions[0];
        court.GetComponent<MeshRenderer>().material = hard;
        if(SelectionScript.Charecter)
            Instantiate(player_3, playerlocation, Quaternion.identity);
        else
            Instantiate(player_4, playerlocation, Quaternion.identity);
        if(SelectionScript.OpponentCharecter)
            Instantiate(bot_1, botlocation, Quaternion.identity);
        else
            Instantiate(bot_2, botlocation, Quaternion.identity);
        GameObject.FindWithTag("Bot").GetComponent<Transform>().eulerAngles = new Vector3(0, 180, 0);
    }

    public void NextCourt(){    	
        if(Court)
        {
            court1.SetActive(false);
            court3.SetActive(true);
            courttype.text = courts[1];
            court.GetComponent<MeshRenderer>().material = clay;
            instruction.text = instructions[1];
        }
        else
        {
            court1.SetActive(true);
            court3.SetActive(false);
            courttype.text = courts[0];
            court.GetComponent<MeshRenderer>().material = hard;
            instruction.text = instructions[0];
        }
        Court = !Court;
    	
        //switch(CourtInt){
    	//	case 1:
     //           PlayerPrefs.SetInt(courtType,1);
    	//		court1.SetActive(false) ;
    	//		court2.SetActive(true);
    	//		CourtInt++;
    	//		break;
    	//	case 2:
     //           PlayerPrefs.SetInt(courtType,2);
    	//		court2.SetActive(false) ;
    	//		court3.SetActive(true);
    	//		CourtInt++;
    	//		break;
    	//	case 3:   
     //           PlayerPrefs.SetInt(courtType,3);
    	//		court3.SetActive(false) ;
    	//		court4.SetActive(true);
    	//		CourtInt++;
    	//		break;
    	//	case 4:
     //           PlayerPrefs.SetInt(courtType,4);
    	//		court4.SetActive(false) ;
    	//		court1.SetActive(true);
    	//		ResetInt();
    	//		break;
    	//	default:
    	//		ResetInt();
    	//		break;
    	//}

    }

    public void PrevCourt(){
    	//switch(CourtInt){
    	//	case 1:
     //           PlayerPrefs.SetInt(courtType,4);
    	//		court2.SetActive(false) ;
    	//		court1.SetActive(true);
    	//		ResetInt();
    	//		break;
    	//	case 2:
     //           PlayerPrefs.SetInt(courtType,1);   
    	//		court3.SetActive(false) ;
    	//		court2.SetActive(true);
    	//		CourtInt--;
    	//		break;
    	//	case 3:
     //           PlayerPrefs.SetInt(courtType,2);
    	//		court4.SetActive(false) ;
    	//		court3.SetActive(true);
    	//		CourtInt--;
    	//		break;
    	//	case 4:
     //           PlayerPrefs.SetInt(courtType,3);
    	//		court1.SetActive(false) ;
    	//		court4.SetActive(true);
    	//		CourtInt--;
    	//		break;
    	//	default:
    	//		ResetInt();
    	//		break;
    	//}
    	
    }

    private void ResetInt(){
    	//if(CourtInt >= 4){
    	//	CourtInt = 1;
    	//}
    	//else{
    	//	CourtInt = 4;
    	//}
    }

}
