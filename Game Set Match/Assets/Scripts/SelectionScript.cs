using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject avatar1;
    public GameObject avatar2;
    public GameObject avatar3;
    public GameObject avatar4;
    public GameObject opponentAvatar1;
    public GameObject opponentAvatar2;
    public GameObject opponentAvatar3;
    public GameObject opponentAvatar4;
    private Vector3 CharecterPosition;
    private Vector3 OffScreen;
   	private int CharecterInt = 1;
    private Vector3 OpponentCharecterPosition;
    private Vector3 OpponentOffScreen;
    private int OpponentCharecterInt = 1;
    private readonly string playerAvatar = "playerAvatar";
    private readonly string opponentAvatar = "opponentAvatar";

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

        avatar1.SetActive(true);
        avatar2.SetActive(false);
        avatar3.SetActive(false);
        avatar4.SetActive(false);
        opponentAvatar1.SetActive(true);
        opponentAvatar2.SetActive(false);
        opponentAvatar3.SetActive(false);
        opponentAvatar4.SetActive(false);

    }

    public void NextCharecter(){    	

        // Vector3 offset = new Vector3(300.0f,0,0);
    	
        switch(CharecterInt){
    		case 1:
                PlayerPrefs.SetInt(playerAvatar,1);
    			avatar1.SetActive(false) ;
    			avatar2.SetActive(true);
    			CharecterInt++;
    			break;
    		case 2:
                PlayerPrefs.SetInt(playerAvatar,2);
    			avatar2.SetActive(false) ;
    			avatar3.SetActive(true);
    			CharecterInt++;
    			break;
    		case 3:
                PlayerPrefs.SetInt(playerAvatar,3);
    			avatar3.SetActive(false) ;
    			avatar4.SetActive(true);
    			CharecterInt++;
    			break;
    		case 4:
                PlayerPrefs.SetInt(playerAvatar,4);
    			avatar4.SetActive(false) ;
    			avatar1.SetActive(true);
    			ResetInt();
    			break;
    		default:
    			ResetInt();
    			break;
    	}

    }

    public void PrevCharecter(){
        
    	switch(CharecterInt){
    		case 1:
                PlayerPrefs.SetInt(playerAvatar,4);
    			avatar2.SetActive(false) ;
    			avatar1.SetActive(true);
    			ResetInt();
    			break;
    		case 2:
                PlayerPrefs.SetInt(playerAvatar,1);
    			avatar3.SetActive(false) ;
    			avatar2.SetActive(true);
    			CharecterInt--;
    			break;
    		case 3:
                PlayerPrefs.SetInt(playerAvatar,2);
    			avatar4.SetActive(false) ;
    			avatar3.SetActive(true);
    			CharecterInt--;
    			break;
    		case 4:
                PlayerPrefs.SetInt(playerAvatar,3);
    			avatar1.SetActive(false) ;
    			avatar4.SetActive(true);
    			CharecterInt--;
    			break;
    		default:
    			ResetInt();
    			break;
    	}
    	
    }

    public void OpponentNextCharecter(){        

        switch(OpponentCharecterInt){
            case 1:
                PlayerPrefs.SetInt(opponentAvatar,1);
                opponentAvatar1.SetActive(false) ;
                opponentAvatar2.SetActive(true);
                OpponentCharecterInt++;
                break;
            case 2:
                PlayerPrefs.SetInt(opponentAvatar,2);
                opponentAvatar2.SetActive(false) ;
                opponentAvatar3.SetActive(true);
                OpponentCharecterInt++;
                break;
            case 3:
                PlayerPrefs.SetInt(opponentAvatar,3);
                opponentAvatar3.SetActive(false) ;
                opponentAvatar4.SetActive(true);
                OpponentCharecterInt++;
                break;
            case 4:
                PlayerPrefs.SetInt(opponentAvatar,4);
                opponentAvatar4.SetActive(false) ;
                opponentAvatar1.SetActive(true);
                OpponentResetInt();
                break;
            default:
                OpponentResetInt();
                break;
        }

    }

    public void OpponentPrevCharecter(){
        switch(OpponentCharecterInt){
            case 1:
                PlayerPrefs.SetInt(opponentAvatar,4);
                opponentAvatar2.SetActive(false) ;
                opponentAvatar1.SetActive(true);
                OpponentResetInt();
                break;
            case 2:
                PlayerPrefs.SetInt(opponentAvatar,1);
                opponentAvatar3.SetActive(false) ;
                opponentAvatar2.SetActive(true);
                OpponentCharecterInt--;
                break;
            case 3:
                PlayerPrefs.SetInt(opponentAvatar,2);
                opponentAvatar4.SetActive(false) ;
                opponentAvatar3.SetActive(true);
                OpponentCharecterInt--;
                break;
            case 4:
                PlayerPrefs.SetInt(opponentAvatar,3);
                opponentAvatar1.SetActive(false) ;
                opponentAvatar4.SetActive(true);
                OpponentCharecterInt--;
                break;
            default:
                OpponentResetInt();
                break;
        }
        
    }

    private void ResetInt(){
    	if(CharecterInt >= 4){
    		CharecterInt = 1;
    	}
    	else{
    		CharecterInt = 4;
    	}
    }

    private void OpponentResetInt(){
        if(OpponentCharecterInt >= 4){
            OpponentCharecterInt = 1;
        }
        else{
            OpponentCharecterInt = 4;
        }
    }



}
