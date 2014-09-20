using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public GameObject standbyCamera;
	public string versionID;
	public bool offlineMode = false;
	SpawnSpot [] spawnSpots;
	// Use this for initialization
	void Start () {
		 spawnSpots = GameObject.FindObjectsOfType<SpawnSpot> ();
		 Connect ();
	}

	void Connect () {
		if (offlineMode) {
			PhotonNetwork.offlineMode = true;
				} 
		else {
						PhotonNetwork.ConnectUsingSettings (versionID);
						Debug.Log ("Connected Successfully");
				}
	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString() );
	}

	void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed() {
		PhotonNetwork.CreateRoom (null);

	}

	void OnJoinedRoom() {
		Screen.showCursor = false;
		if (spawnSpots == null) {
			Debug.Log("No spawn points found");
			return;
		}

		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];

		GameObject myPlayer = (GameObject) PhotonNetwork.Instantiate ("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		standbyCamera.SetActive(false);
		((MonoBehaviour)myPlayer.GetComponent("MouseLook")).enabled = true;
		((MonoBehaviour)myPlayer.GetComponent("PlayerMovement")).enabled = true;
		myPlayer.transform.FindChild ("Main Camera").gameObject.SetActive (true);
		}
}