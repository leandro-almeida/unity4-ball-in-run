using UnityEngine;
using System.Collections;

public class CheckPointController : MonoBehaviour {

	public Transform checkPointPrefab;
	public float radiusSpawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Transform SpawnCheckPoint() {
		Transform tempCheckPoint = Instantiate(checkPointPrefab) as Transform;
		tempCheckPoint.transform.position = GetNewPosition();
		return tempCheckPoint;
	}

	public void SpawnAtNewPosition(Transform checkPoint) {
		checkPoint.transform.position = GetNewPosition();
	}

	private Vector3 GetNewPosition() {
		Vector3 newPosition = transform.position;
		newPosition.x += Random.Range (-radiusSpawn, radiusSpawn);
		newPosition.z += Random.Range (-radiusSpawn, radiusSpawn);
		return newPosition;
	}

}
