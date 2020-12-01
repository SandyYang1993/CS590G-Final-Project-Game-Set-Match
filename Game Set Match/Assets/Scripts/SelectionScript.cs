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

    private void Awake(){

    	CharecterPosition = avatar1.transform.position;
    	OffScreen = avatar2.transform.position;
        OpponentCharecterPosition = opponentAvatar1.transform.position;
        OpponentOffScreen = opponentAvatar2.transform.position;

    }

    public void NextCharecter(){    	

    	switch(CharecterInt){
    		case 1:
    			avatar1.SetActive(false) ;
    			avatar1.transform.position = OffScreen;
    			avatar2.transform.position = CharecterPosition;
    			avatar2.SetActive(true);
    			Debug.Log("Player Changed to: " + avatar2.name);
    			CharecterInt++;
    			break;
    		case 2:
    			avatar2.SetActive(false) ;
    			avatar2.transform.position = OffScreen;
    			avatar3.transform.position = CharecterPosition;
    			avatar3.SetActive(true);
    			Debug.Log("Player Changed to: " + avatar3.name);
    			CharecterInt++;
    			break;
    		case 3:
    			avatar3.SetActive(false) ;
    			avatar3.transform.position = OffScreen;
    			avatar4.transform.position = CharecterPosition;
    			avatar4.SetActive(true);
    			Debug.Log("Player Changed to: " + avatar4.name);
    			CharecterInt++;
    			break;
    		case 4:
    			avatar4.SetActive(false) ;
    			avatar4.transform.position = OffScreen;
    			avatar1.transform.position = CharecterPosition;
    			avatar1.SetActive(true);
    			Debug.Log("Player Changed to: " + avatar1.name);
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
    			avatar2.SetActive(false) ;
    			avatar2.transform.position = OffScreen;
    			avatar1.transform.position = CharecterPosition;
    			avatar1.SetActive(true);
    			Debug.Log("Player Changed to: " + avatar2.name);
    			ResetInt();
    			break;
    		case 2:
    			avatar3.SetActive(false) ;
    			avatar3.transform.position = OffScreen;
    			avatar2.transform.position = CharecterPosition;
    			avatar2.SetActive(true);
    			Debug.Log("Player Changed to: " + avatar2.name);
    			CharecterInt--;
    			break;
    		case 3:
    			avatar4.SetActive(false) ;
    			avatar4.transform.position = OffScreen;
    			avatar3.transform.position = CharecterPosition;
    			avatar3.SetActive(true);
    			Debug.Log("Player Changed to: " + avatar2.name);
    			CharecterInt--;
    			break;
    		case 4:
    			avatar1.SetActive(false) ;
    			avatar1.transform.position = OffScreen;
    			avatar4.transform.position = CharecterPosition;
    			avatar4.SetActive(true);
    			Debug.Log("Player Changed to: " + avatar3.name);
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
                opponentAvatar1.SetActive(false) ;
                opponentAvatar1.transform.position = OpponentOffScreen;
                opponentAvatar2.transform.position = OpponentCharecterPosition;
                opponentAvatar2.SetActive(true);
                Debug.Log("Player Changed to: " + opponentAvatar2.name);
                OpponentCharecterInt++;
                break;
            case 2:
                opponentAvatar2.SetActive(false) ;
                opponentAvatar2.transform.position = OpponentOffScreen;
                opponentAvatar3.transform.position = OpponentCharecterPosition;
                opponentAvatar3.SetActive(true);
                Debug.Log("Player Changed to: " + opponentAvatar3.name);
                OpponentCharecterInt++;
                break;
            case 3:
                opponentAvatar3.SetActive(false) ;
                opponentAvatar3.transform.position = OpponentOffScreen;
                opponentAvatar4.transform.position = OpponentCharecterPosition;
                opponentAvatar4.SetActive(true);
                Debug.Log("Player Changed to: " + opponentAvatar4.name);
                OpponentCharecterInt++;
                break;
            case 4:
                opponentAvatar4.SetActive(false) ;
                opponentAvatar4.transform.position = OpponentOffScreen;
                opponentAvatar1.transform.position = OpponentCharecterPosition;
                opponentAvatar1.SetActive(true);
                Debug.Log("Player Changed to: " + opponentAvatar1.name);
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
                opponentAvatar2.SetActive(false) ;
                opponentAvatar2.transform.position = OpponentOffScreen;
                opponentAvatar1.transform.position = OpponentCharecterPosition;
                opponentAvatar1.SetActive(true);
                Debug.Log("Player Changed to: " + opponentAvatar2.name);
                OpponentResetInt();
                break;
            case 2:
                opponentAvatar3.SetActive(false) ;
                opponentAvatar3.transform.position = OpponentOffScreen;
                opponentAvatar2.transform.position = OpponentCharecterPosition;
                opponentAvatar2.SetActive(true);
                Debug.Log("Player Changed to: " + opponentAvatar2.name);
                OpponentCharecterInt--;
                break;
            case 3:
                opponentAvatar4.SetActive(false) ;
                opponentAvatar4.transform.position = OpponentOffScreen;
                opponentAvatar3.transform.position = OpponentCharecterPosition;
                opponentAvatar3.SetActive(true);
                Debug.Log("Player Changed to: " + opponentAvatar2.name);
                OpponentCharecterInt--;
                break;
            case 4:
                opponentAvatar1.SetActive(false) ;
                opponentAvatar1.transform.position = OpponentOffScreen;
                opponentAvatar4.transform.position = OpponentCharecterPosition;
                opponentAvatar4.SetActive(true);
                Debug.Log("Player Changed to: " + opponentAvatar3.name);
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
