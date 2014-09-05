using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public Camera standbyCamera;
	SpawnSpot [] spawnSpots;
	// Use this for initialization
	void Start () {
		 spawnSpots = GameObject.FindObjectsOfType<SpawnSpot> ();
		 Connect ();
	}

	void Connect () {
		PhotonNetwork.ConnectUsingSettings("alpha-1");
		Debug.Log ("Connected Successfully");
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

		if (spawnSpots == null) {
			Debug.Log("No spawn points found");
			return;
		}

		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];

		PhotonNetwork.Instantiate ("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		standbyCamera.enabled = false;
		}
}