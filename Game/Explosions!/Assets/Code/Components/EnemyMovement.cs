using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {



	Vector3 pointA;
	bool moveOut;
	float distanceBetween;
	float currDistance;

	[SerializeField]
	private float xOffset = 3.0f; //Default
	[SerializeField] //TODO: Load maxHealth from AttributeManager
	private float zOffset = 1.0f; //Default 
	[SerializeField]
	private float driftTime = 100.0f; // Default

	// Use this for initialization
	void Start () {
		xOffset = 3.0f;
		zOffset = 1.0f;
		driftTime = 100.0f;
		moveOut = true;
		currDistance = 0.0f;

		pointA = transform.position;
		var pointB = new Vector3(pointA.x + xOffset, pointA.y, pointA.z+zOffset);
		distanceBetween = Vector3.Distance(pointA, pointB);
	}

	// Update is called once per frame
	void Update () {
		var pointB = new Vector3(pointA.x + xOffset, pointA.y, pointA.z+zOffset);

		transform.position = Vector3.Lerp(this.pointA, pointB, currDistance / distanceBetween);
		if (moveOut) {
			currDistance += distanceBetween / driftTime;
			if(currDistance > distanceBetween) {
				moveOut = false;
			}
		} else {
			currDistance -= distanceBetween / driftTime;
			if(currDistance < 0.0f) {
				moveOut = true;
			}
		}
		transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
	}
}

