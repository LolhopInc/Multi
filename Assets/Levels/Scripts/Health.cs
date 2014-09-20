using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float hitPoints = 100f;
	float currentHitPoints;
	

	// Use this for initialization
	void Start () {
		currentHitPoints = hitPoints;
	}
	

	void TakeDamage (float amt) {
		currentHitPoints -= amt;

		if (currentHitPoints <= 0) {
			Die();
		}
	}
	void Die() {
		Destroy (gameObject);
}
}