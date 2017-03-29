using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

	public GameObject monsterCandidate;
	public List<Transform> spawnPoint;
	public GameObject initFollowTarget;

	public float spawnMonsterTime = 10;
	private float spawnCounter = 0;

	// Update is called once per frame
	void Update () {
		spawnCounter += Time.deltaTime;
		if (spawnCounter >= spawnMonsterTime) {
			spawnCounter = 0;

			GameObject newMonster = GameObject.Instantiate (monsterCandidate);
			newMonster.GetComponent<MonsterScript> ().followTarget = initFollowTarget;
			newMonster.transform.position = spawnPoint [Random.Range (0, spawnPoint.Count)].position;
		}
	}
}
