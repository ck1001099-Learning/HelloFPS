using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {
	
	private Animator animatorController;

	public Transform rotateYTransform;
	public Transform rotateXTransform;
	public float rotateSpeed;
	public float currentRotateX = 0;
	public float MoveSpeed;
	float currentSpeed = 0;

	public Rigidbody rigidBody;

	public JumpSensor jumpSensor;
	public float jumpSpeed;
	public GunManager gunManager;

	public GameUIManager uiManager;
	public int hp = 100;

	// Use this for initialization
	void Start () {
		animatorController = this.GetComponent<Animator> ();
	}

	public void Hit (int value){
		if (hp <= 0) {
			return;
		}

		hp -= value;
		uiManager.SetHP (hp);

		if (hp > 0) {
			uiManager.PlayHitAnimation ();
		} else {
			uiManager.PlayerDiedAnimation ();

			rigidBody.gameObject.GetComponent<Collider> ().enabled = false;
			rigidBody.useGravity = false;
			rigidBody.velocity = Vector3.zero;
			this.enabled = false;
			rotateXTransform.transform.DOLocalRotate (new Vector3 (-60, 0, 0), 0.5f);
			rotateYTransform.transform.DOLocalMoveY (-1.5f, 0.5f).SetRelative (true);
		}

	}

	// Update is called once per frame
	void Update () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		if (Input.GetMouseButton (0)) {
			gunManager.TryToTriggerGun ();
		}

		Vector3 moveDirection = Vector3.zero;
		if (Input.GetKey (KeyCode.W)) {
			moveDirection.z += 1;
		}
		if (Input.GetKey (KeyCode.S)) {
			moveDirection.z -= 1;
		}
		if (Input.GetKey (KeyCode.D)) {
			moveDirection.x += 1;
		}
		if (Input.GetKey (KeyCode.A)) {
			moveDirection.x -= 1;
		}
		moveDirection = moveDirection.normalized;

		if (moveDirection.magnitude == 0 || !jumpSensor.IsCanJump ()) {
			currentSpeed = 0;
		} else {
			if (moveDirection.z < 0) {
				currentSpeed = -MoveSpeed;
			} else {
				currentSpeed = MoveSpeed;
			}
		}
		animatorController.SetFloat ("Speed", currentSpeed);

		Vector3 worldSpaceDirection = moveDirection.z * rotateYTransform.transform.forward + moveDirection.x * rotateYTransform.transform.right;
		Vector3 velocity = rigidBody.velocity;
		velocity.x = worldSpaceDirection.x * MoveSpeed;
		velocity.z = worldSpaceDirection.z * MoveSpeed;

		if (Input.GetKey (KeyCode.Space) && jumpSensor.IsCanJump ()) {
			velocity.y = jumpSpeed;
		}

		rigidBody.velocity = velocity;

		rotateYTransform.transform.localEulerAngles += new Vector3 (0, Input.GetAxis ("Horizontal"), 0) * rotateSpeed;
		currentRotateX += Input.GetAxis ("Vertical") * rotateSpeed;

		if (currentRotateX > 90) {
			currentRotateX = 90;
		} else if (currentRotateX < -90) {
			currentRotateX = -90;
		}
		rotateXTransform.transform.localEulerAngles = new Vector3 (-currentRotateX, 0, 0);
	}
}
