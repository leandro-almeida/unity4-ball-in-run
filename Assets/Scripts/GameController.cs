using UnityEngine;
using System.Collections;

public enum stateMachine {
	START,
	RELOAD,
	PAUSED,
	PLAY,
	WIN,
	LOSE,
	NULL
}

public class GameController : MonoBehaviour {

	private stateMachine currentState = stateMachine.START;
	private stateMachine lastState = stateMachine.NULL;

	private int score;
	private float currentTimeToScore = 0;
	private float currentTimeToResetPosition = 0;
	private Transform currentCheckPoint;

	public int basePoints = 1;
	public int pointsCheckPoint = 1;
	public float timeToScore = 3;
	public float timeToResetPosition = 2;

	public PlayerBehaviour player;
	public HUDController HUD;
	public CheckPointController checkPointController;
	public EnemyController enemyController;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameStateMachine ();
	}

	public void SwitchState(stateMachine nextState) {
		lastState = currentState;
		currentState = nextState;
	}

	void GameStateMachine() {

		switch (currentState) {

		case stateMachine.START:
		{
			score = 0;
			HUD.AddScore(score);
			SwitchState(stateMachine.PLAY);
			currentCheckPoint = checkPointController.SpawnCheckPoint();
		}
		break;
		
		case stateMachine.RELOAD:
		{
			Application.LoadLevel(Application.loadedLevelName);
		}
		break;


		case stateMachine.PAUSED:
		{
			// input
			BasicInputs();
		}
		break;
		
		case stateMachine.PLAY:
		{
			// player
			player.Move();
			player.IncreaseXixi();

			// input
			BasicInputs();

			// Score
			currentTimeToScore += Time.deltaTime;
			if (currentTimeToScore > timeToScore) {
				currentTimeToScore = 0;
				score++;
				HUD.AddScore(score*basePoints);
			}
		}
		break;
		
		case stateMachine.WIN:
		{
			// contabiliza tempo necessario pra iniciar nova fase
			currentTimeToResetPosition += Time.deltaTime;
			if (currentTimeToResetPosition >= timeToResetPosition) {
				// zera contador
				currentTimeToResetPosition = 0;

				// input
				BasicInputs();
				
				// coloca banheiro em nova posicao
				checkPointController.SpawnAtNewPosition(currentCheckPoint);
				
				// atualiza score
				score += pointsCheckPoint;
				HUD.AddScore(score*basePoints);
				
				// coloca player na posicao inicial
				//player.resetPosition();

				// aumenta dificuldade dos inimigos (velocidade)
				enemyController.AddDifficulty();

				// muda game state
				SwitchState(stateMachine.PLAY);
			}
		}
		break;
		
		case stateMachine.LOSE:
		{
			// carrega ranking
			ApplicationController.AddToRanking(score * basePoints);
			Application.LoadLevel("Ranking");
		}
		break;

		}

	}
	
	public stateMachine GetCurrentState() {
		return currentState;
	}

	void BasicInputs() {
		if (Input.GetKeyDown (KeyCode.Escape) && currentState == stateMachine.PLAY) {
			SwitchState(stateMachine.PAUSED);
		}
		else if (Input.GetKeyDown (KeyCode.Escape) && currentState == stateMachine.PAUSED) {
			SwitchState(stateMachine.PLAY);
		}
		else if (Input.GetKeyDown (KeyCode.R)) {
			SwitchState(stateMachine.RELOAD);
		}
	}
}
