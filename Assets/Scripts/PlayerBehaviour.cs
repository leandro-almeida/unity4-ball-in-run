using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	public GameController gameController;

	public float totalXixi = 10; // maximo de xixi que aguenta
	public float timeToXixi = 4; // tempo para incrementar o xixi
	public float xixiToIncrease = 1; // qtd de xixi a incrementar por vez

	public float speed = 10.0F;
	public float rotateSpeed = 3.0F;

	private Vector3 startPosition;
	private float currentTimeToXixi = 0;
	private float currentXixi = 0; // qtd de xixi acumulado

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	public void Move () {
		CharacterController controller = GetComponent<CharacterController>();
		transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		float curSpeed = speed * Input.GetAxis("Vertical");
		controller.SimpleMove(forward * curSpeed);

		if (transform.position.y < -5) {
			gameController.SwitchState(stateMachine.LOSE);
		}

	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "Enemy") {
			gameController.SwitchState(stateMachine.LOSE);
		}
		if (hit.gameObject.tag == "CheckPoint") {
			currentXixi = 0;
			gameController.SwitchState(stateMachine.WIN);
		}
	}

	public void resetPosition() {
		transform.position = startPosition;
	}

	public void IncreaseXixi() {
		currentTimeToXixi += Time.deltaTime;

		if (currentTimeToXixi >= timeToXixi) {
			currentXixi += xixiToIncrease;
			currentTimeToXixi = 0;
		}

		if (currentXixi > totalXixi) {
			gameController.SwitchState(stateMachine.LOSE);
		}
	}

	public float GetCurrentXixi() {
		return currentXixi;
	}
}
