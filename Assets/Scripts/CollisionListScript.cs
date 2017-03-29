using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListScript : MonoBehaviour {

	public List<Collider> collisionObjects;

	public void OnTriggerEnter(Collider other){
		collisionObjects.Add (other);
	}

	public void OnTriggerExit(Collider other){
		collisionObjects.Remove (other);
	}
}
