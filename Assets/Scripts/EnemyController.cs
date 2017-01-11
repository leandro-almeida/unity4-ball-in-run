using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameController gameController;
	public EnemyBehaviour enemy;

	public int maxEnemiesInGame;
	private int currentEnemies = 0;
	public float timeToRespawn;
	private float currentTime = 0;

	public float radiusMin = 10f;
	public float radiusMax = 25f;

	public float difficultyFactor = 1;
	public float difficultyAdd = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (currentEnemies < maxEnemiesInGame) {
			if (currentTime > timeToRespawn && gameController.GetCurrentState() == stateMachine.PLAY) {
				CreateEnemy();
				currentTime = 0;
			}
			else {
				currentTime += Time.deltaTime;
			}
		}
	}

	void CreateEnemy() {
		GameObject temp = Instantiate (enemy.gameObject) as GameObject;
		temp.transform.parent = transform;

		Vector3 newPosition = gameController.player.transform.position;
		newPosition.x = newPosition.x + (Random.Range (radiusMin, radiusMax));
		newPosition.z = newPosition.z + (Random.Range (radiusMin, radiusMax));
		temp.transform.position = newPosition;

		temp.GetComponent<EnemyBehaviour>().enemyController = this;

		currentEnemies++;
	}

	public void AddDifficulty() {
		difficultyFactor += difficultyAdd;
	}

	public void DecreaseEnemiesCount() {
		if (currentEnemies > 0) {
			currentEnemies--;
		}
	}
}
