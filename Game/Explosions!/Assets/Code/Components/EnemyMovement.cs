using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	[SerializeField]
	private ParticleSystem explosion;
	[SerializeField]
	private float explosionTime = 100.0f;
	[SerializeField]
	private int numExplosionParticles = 50;
	[SerializeField]
	private byte particleYellowness = 0x10;
	[SerializeField]
	private float particleSpeed = 2.0f;
	[SerializeField]
	private float particleSize = 0.5f;
	[SerializeField]
	private float particleLifeTime = 2.0f;

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

		// explosion = (ParticleSystem) gameObject.GetComponent("ParticleSystem");
		explosion.enableEmission = false;
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

	void OnCollisionEnter(Collision collision) {
		explode ();
	}

	void explode() {
		var pos = transform.position;
		explosion.transform.position = pos;
		
		var particles = pointsOnSphere (numExplosionParticles);
		
		foreach( Vector3 particle in particles ) {
			var particleColour = new Color32(0xff, particleYellowness, 0, 0xff);
			
			explosion.Emit (
				pos,
				particle * particleSpeed,
				particleSize,
				particleLifeTime,
				particleColour );
		}
		
		Destroy(gameObject);
	}

	// Distributes point evenly on a sphere. Source: http://www.xsi-blog.com/archives/115
	IList<Vector3> pointsOnSphere(float n) {
		var pts = new List<Vector3>();
		var inc = Mathf.PI * (3 - Mathf.Sqrt (5));
		var off = 2 / n;

		for(int k = 0; k < n; k++) {
			var y = k * off - 1 + (off / 2);
			var r = Mathf.Sqrt(1 - y*y);
			var phi = k * inc;
			var pt = new Vector3(Mathf.Cos(phi) * r, y, Mathf.Sin(phi) * r);
			pts.Add(pt);

		}
		return pts;
	}
}

