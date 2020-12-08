using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtSelectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject court1;
    public GameObject court2;
    public GameObject court3;
    public GameObject court4;
    private Vector3 CourtPosition;
    private Vector3 OffScreen;
   	private int CourtInt = 1;
    private readonly string courtType = "courtType";

    // Court Types Map
    // 1 -> court2
    // 2 -> court3
    // 3 -> court4
    // 4 -> court1

    private void Awake(){

    	court1.SetActive(true);
        court2.SetActive(false);
        court3.SetActive(false);
        court4.SetActive(false);
    }

    public void NextCourt(){    	

    	switch(CourtInt){
    		case 1:
                PlayerPrefs.SetInt(courtType,1);
    			court1.SetActive(false) ;
    			court2.SetActive(true);
    			CourtInt++;
    			break;
    		case 2:
                PlayerPrefs.SetInt(courtType,2);
    			court2.SetActive(false) ;
    			court3.SetActive(true);
    			CourtInt++;
    			break;
    		case 3:   
                PlayerPrefs.SetInt(courtType,3);
    			court3.SetActive(false) ;
    			court4.SetActive(true);
    			CourtInt++;
    			break;
    		case 4:
                PlayerPrefs.SetInt(courtType,4);
    			court4.SetActive(false) ;
    			court1.SetActive(true);
    			ResetInt();
    			break;
    		default:
    			ResetInt();
    			break;
    	}

    }

    public void PrevCourt(){
    	switch(CourtInt){
    		case 1:
                PlayerPrefs.SetInt(courtType,4);
    			court2.SetActive(false) ;
    			court1.SetActive(true);
    			ResetInt();
    			break;
    		case 2:
                PlayerPrefs.SetInt(courtType,1);   
    			court3.SetActive(false) ;
    			court2.SetActive(true);
    			CourtInt--;
    			break;
    		case 3:
                PlayerPrefs.SetInt(courtType,2);
    			court4.SetActive(false) ;
    			court3.SetActive(true);
    			CourtInt--;
    			break;
    		case 4:
                PlayerPrefs.SetInt(courtType,3);
    			court1.SetActive(false) ;
    			court4.SetActive(true);
    			CourtInt--;
    			break;
    		default:
    			ResetInt();
    			break;
    	}
    	
    }

    private void ResetInt(){
    	if(CourtInt >= 4){
    		CourtInt = 1;
    	}
    	else{
    		CourtInt = 4;
    	}
    }

}
