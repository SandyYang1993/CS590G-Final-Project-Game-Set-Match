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

    private void Awake(){

    	CourtPosition = court1.transform.position;
    	OffScreen = court2.transform.position;
    }

    public void NextCourt(){    	

    	switch(CourtInt){
    		case 1:
    			court1.SetActive(false) ;
    			court1.transform.position = OffScreen;
    			court2.transform.position = CourtPosition;
    			court2.SetActive(true);
    			Debug.Log("Court Changed to: " + court2.name);
    			CourtInt++;
    			break;
    		case 2:
    			court2.SetActive(false) ;
    			court2.transform.position = OffScreen;
    			court3.transform.position = CourtPosition;
    			court3.SetActive(true);
    			Debug.Log("Court Changed to: " + court3.name);
    			CourtInt++;
    			break;
    		case 3:
    			court3.SetActive(false) ;
    			court3.transform.position = OffScreen;
    			court4.transform.position = CourtPosition;
    			court4.SetActive(true);
    			Debug.Log("Court Changed to: " + court4.name);
    			CourtInt++;
    			break;
    		case 4:
    			court4.SetActive(false) ;
    			court4.transform.position = OffScreen;
    			court1.transform.position = CourtPosition;
    			court1.SetActive(true);
    			Debug.Log("Court Changed to: " + court1.name);
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
    			court2.SetActive(false) ;
    			court2.transform.position = OffScreen;
    			court1.transform.position = CourtPosition;
    			court1.SetActive(true);
    			Debug.Log("Court Changed to: " + court2.name);
    			ResetInt();
    			break;
    		case 2:
    			court3.SetActive(false) ;
    			court3.transform.position = OffScreen;
    			court2.transform.position = CourtPosition;
    			court2.SetActive(true);
    			Debug.Log("Court Changed to: " + court2.name);
    			CourtInt--;
    			break;
    		case 3:
    			court4.SetActive(false) ;
    			court4.transform.position = OffScreen;
    			court3.transform.position = CourtPosition;
    			court3.SetActive(true);
    			Debug.Log("Court Changed to: " + court2.name);
    			CourtInt--;
    			break;
    		case 4:
    			court1.SetActive(false) ;
    			court1.transform.position = OffScreen;
    			court4.transform.position = CourtPosition;
    			court4.SetActive(true);
    			Debug.Log("Court Changed to: " + court3.name);
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
