using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour {

	public float damageValue = 1;
	
	void OnTriggerStay(Collider other){
		other.gameObject.SendMessage ("Hit", damageValue);
	}
}
