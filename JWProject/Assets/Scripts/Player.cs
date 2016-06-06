using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private float inputDirection; // x value of our MmoveVector
	private float verticalVelocity; // y value of our MoveVector

	private float speed = 5;
	private float gravity = 30;
	private float jumpForce = 10;
	private bool secondJumpAvl = false;

	private Vector3 moveVector; 
	private Vector3 lastVector;
	private CharacterController controller;

	void Start () {
		controller = GetComponent<CharacterController> ();
	}

	void Update() {

		moveVector = Vector3.zero;
		//get input direction between 1, -1
		inputDirection = Input.GetAxis ("Horizontal") * speed;
		//check player grounded
		if (IsControllerGrounded ()) {
			verticalVelocity = 0;

			//jump
			if (Input.GetKeyDown(KeyCode.Space)) {
				verticalVelocity = jumpForce;
				secondJumpAvl = true;
			}

			moveVector.x = inputDirection;

		} else {

			//jump
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (secondJumpAvl) {
					verticalVelocity = jumpForce;
					secondJumpAvl = false;
				}
			}
			verticalVelocity -= gravity * Time.deltaTime;
			moveVector.x = lastVector.x;
		}

		moveVector.y = verticalVelocity;
		//moveVector = new Vector3 (inputDirection,verticalVelocity,0);
		//controlller move using vector
		controller.Move(moveVector * Time.deltaTime);
		lastVector = moveVector;

	}

	private bool IsControllerGrounded(){

		Vector3 leftRayStart;
		Vector3 rightRayStart;

		leftRayStart = controller.bounds.center;
		rightRayStart = controller.bounds.center;

		leftRayStart.x -= controller.bounds.extents.x;
		rightRayStart.x += controller.bounds.extents.x;

		Debug.DrawRay (leftRayStart, Vector3.down, Color.green);
		Debug.DrawRay (rightRayStart, Vector3.down, Color.blue);

		if (Physics.Raycast (leftRayStart, Vector3.down, (controller.height / 2) + 0.1f))
			return true;

		if (Physics.Raycast (rightRayStart, Vector3.down, (controller.height / 2) + 0.1f))
			return true;

		return false;
	}

	private void OnControllerColliderHit(ControllerColliderHit hit){
		if (controller.collisionFlags == CollisionFlags.Sides) {
			
			if (Input.GetKeyDown(KeyCode.Space)) {
				Debug.DrawRay (hit.point, hit.normal, Color.red, 2.0f);
				moveVector = hit.normal * speed;
				verticalVelocity = jumpForce; 
				secondJumpAvl = true;
			}
		}

		//Collectables
		switch(hit.gameObject.tag){
		case "Coin":
			LevelManager.Instance.CollectCoin ();
			Destroy (hit.gameObject);
			break;
		case "JumpPad":
			verticalVelocity = 20;
			break;
		case "Teleport":
			transform.position = hit.transform.GetChild (0).position;
			break;
		case "Win":
			LevelManager.Instance.Win ();
			Destroy (hit.gameObject);
			break;
		default:
			break;
		}
	} 
}
