using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCharecter : MonoBehaviour
{
    public GameObject playerAvatar;
    public GameObject opponentAvatar;
    public GameObject courtType;
    // Start is called before the first frame update
    void Start()
    {
     	if(PlayerPrefs.GetInt("playerAvatar") == 1){
			
		}   
		else if (PlayerPrefs.GetInt("playerAvatar") == 2){

		}
		else if (PlayerPrefs.GetInt("playerAvatar") == 3){

		}
		else{

		}

		if(PlayerPrefs.GetInt("opponentAvatar") == 1){
			
		}   
		else if (PlayerPrefs.GetInt("opponentAvatar") == 2){

		}
		else if (PlayerPrefs.GetInt("opponentAvatar") == 3){

		}
		else{

		}

		if(PlayerPrefs.GetInt("courtType") == 1){
			
		}   
		else if (PlayerPrefs.GetInt("courtType") == 2){

		}
		else if (PlayerPrefs.GetInt("courtType") == 3){

		}
		else{

		}

    }

}
