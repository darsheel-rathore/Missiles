using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidatePlayer : MonoBehaviour {

	public bool isPlayerAlive()
	{
		if(GameObject.FindGameObjectWithTag("Player") == null) 
		{ 
			return false; 
		}
		else 
		{ 
			return true; 
		}
	}

}
