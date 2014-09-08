using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;
	Animator anim = GetComponent<Animator>();

	float lastUpdateTime;
	float realSpeed;
	bool isJumping;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (photonView.isMine) {
						// Do nothing, character scripts have us covered	
				} 
	
	else {
			transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
		}
	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
				// This is for OUR player... We must send our actual position to the network
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(anim.GetBool("Jump"));
			stream.SendNext(anim.GetFloat("Speed"));
		} 

		else {
			// This is for networked players... We need to recieve their location and update our version of them

			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();
			isJumping = (bool)stream.ReceiveNext();
			realSpeed = (float)stream.ReceiveNext();
			
		}
	}
}
