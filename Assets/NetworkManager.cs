using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		 Connect ();
	}
	void Connect () {
		PhotonNetwork.ConnectUsingSettings("alpha-1");
		Debug.Log ("Connected Successfully");
	}
	// Update is called once per frame
	void Update () {
	
	}
	void onGUI () {
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString() );
	}
}