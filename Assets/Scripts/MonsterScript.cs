using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterScript : MonoBehaviour {

	private Animator animator;
	private float minimunHitPeriod = 1f;
	private float hitCounter = 0;
	public float currentHP = 100;

	public float moveSpeed;
	public GameObject followTarget;
	public Rigidbody rigidBody;
	public CollisionListScript playerSensor;
	public CollisionListScript attackSensor;

	public GunManager monsterBullet;

	public void AttackPlayer(){
		if (attackSensor.collisionObjects.Count > 0) {
			attackSensor.collisionObjects [0].transform.GetChild (0).GetChild (0).SendMessage ("Hit", 10);
		}
	}

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		rigidBody = this.GetComponent<Rigidbody> ();
	}

	public void Hit(float value){
		if (hitCounter <= 0) {
			followTarget = GameObject.FindGameObjectWithTag ("Player");
			hitCounter = minimunHitPeriod;
			currentHP -= value;
			animator.SetFloat ("HP", currentHP);
			animator.SetTrigger ("Hit");
			if (currentHP <= 0) {
				BuryTheBody ();
			}
		}
	}

	public void BuryTheBody(){
		this.GetComponent<Rigidbody> ().useGravity = false;
		this.GetComponent<Collider> ().enabled = false;
		this.transform.DOMoveY (-0.8f, 1f).SetRelative (true).SetDelay (1).OnComplete (() => {
			this.transform.DOMoveY (-0.8f, 1f).SetRelative (true).SetDelay (3).OnComplete (() => {
				GameObject.Destroy (this.gameObject);
			});
		});
	}

	// Update is called once per frame
	void Update () {
		if (playerSensor.collisionObjects.Count > 0) {
			followTarget = playerSensor.collisionObjects [0].gameObject;
		}

		if (currentHP > 0 && hitCounter > 0) {
			hitCounter -= Time.deltaTime;
		} else {
			if (currentHP > 0) {
				if (followTarget != null) {
					Vector3 lookAt = followTarget.gameObject.transform.position;
					lookAt.y = this.gameObject.transform.position.y;
					this.transform.LookAt (lookAt);
					animator.SetBool ("Run", true);

					if (attackSensor.collisionObjects.Count > 0) {
						animator.SetBool ("Attack", true);
						rigidBody.velocity = Vector3.zero;
					} else {
						animator.SetBool ("Attack", false);
						//this.transform.position += this.transform.forward * moveSpeed * Time.deltaTime;
						//Shoot
						monsterBullet.TryToTriggerGun ();
						this.GetComponent<Rigidbody>().velocity = this.transform.forward * moveSpeed;
					}
				}
			} else {
				rigidBody.velocity = Vector3.zero;
			}
		}
	}
}
