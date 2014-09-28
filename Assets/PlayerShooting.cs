using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {
	
	public float fireRate = 0.5f;
	float cooldown = 0;
	public float damage = 25f;
	
	// Update is called once per frame
	void Update () {
		cooldown -= Time.deltaTime;
		
		if(Input.GetButton("Fire1")) {
			// Player is holding shoot button - Fire!
			Fire ();
			// Note that Fire cannot take excessive (massive) amounts of time to run
		}
		
	}
	
	void Fire() {
		if(cooldown > 0) {
			// if not enough time has passed since the last shot, exit the procedure
			return;
		}

		
		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		Transform hitTransform;
		Vector3   hitPoint;
		
		hitTransform = FindClosestHitObject(ray, out hitPoint);
		
		if(hitTransform != null) {
			Debug.Log ("Hit: " + hitTransform.name);
			
			// Do special effect at hit location (hitPoint) here
			
			Health h = hitTransform.GetComponent<Health>();
			
			while(h == null && hitTransform.parent) {
				hitTransform = hitTransform.parent;
				h = hitTransform.GetComponent<Health>();
			}
			
			// Once we reach here, hitTransform may have been changed inside above while loop
			
			if(h != null) {
				PhotonView pview = h.GetComponent<PhotonView>();
				pview.RPC("TakeDamage", PhotonTargets.AllBuffered, damage);
			}
			
			
		}
		
		cooldown = fireRate;
	}
	
	Transform FindClosestHitObject(Ray ray, out Vector3 hitPoint) {
		
		RaycastHit[] hits = Physics.RaycastAll(ray);
		
		Transform closestHit = null;
		float distance = 0;
		hitPoint = Vector3.zero;
		
		foreach(RaycastHit hit in hits) {
			if(hit.transform != this.transform && ( closestHit==null || hit.distance < distance ) ) {
				// We have hit something that is:
				// a) not us || the first thing we hit (that is not us)|| if !b, is  closer than the previous closest hit
				
				closestHit = hit.transform;
				distance = hit.distance;
				hitPoint = hit.point;
			}
		}

		
		return closestHit;
		
	}
}
