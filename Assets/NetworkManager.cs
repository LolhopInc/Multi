using UnityEngine;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {

	public GameObject standbyCamera;
	SpawnSpot [] spawnSpots;

	public string versionID;
	public bool offlineMode = false;
	bool connecting = false;

	List<string> chatMessages;
	int maxMessages = 7;
	// Use this for initialization
	void Start () {
		 spawnSpots = GameObject.FindObjectsOfType<SpawnSpot> ();
		PhotonNetwork.player.name = PlayerPrefs.GetString ("Username", "Scrublord420");
		chatMessages = new List<string> ();
	}

	void Connect () {
		if (offlineMode) {

				} 
		else {
						PhotonNetwork.ConnectUsingSettings (versionID);
						Debug.Log ("Connected Successfully");
				}
	}

	void OnDestroy() {
		PlayerPrefs.SetString ("Username", PhotonNetwork.player.name);
	}

	public void addChatMessage (string message) {

		while (chatMessages.Count >= maxMessages) {
			chatMessages.RemoveAt(0);
		}

		chatMessages.Add(message);
	}
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString() );

		if (PhotonNetwork.connected == false && connecting == false) {
			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();

			GUILayout.BeginHorizontal();

			GUILayout.Label("Username:");
			PhotonNetwork.player.name = GUILayout.TextField(PhotonNetwork.player.name);

			GUILayout.EndHorizontal();

			GUILayout.FlexibleSpace();

			if (GUILayout.Button("Singleplayer Testing")) {
				PhotonNetwork.offlineMode = true;
				OnJoinedLobby();
			}

			if (GUILayout.Button("Multiplayer")) {
				Connect ();
			}

			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}

		if (PhotonNetwork.connected == true && connecting == false) {

			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();

			foreach(string msg in chatMessages) {
				GUILayout.Label (msg);
			}

			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
	}

	void OnJoinedLobby() {
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed() {
		PhotonNetwork.CreateRoom (null);

	}
	void OnJoinedRoom() {

		connecting = false;

		SpawnMyPlayer ();
	}
	void SpawnMyPlayer() {
		addChatMessage ("" + PhotonNetwork.player.name + " Joined");

		Screen.showCursor = false;
		if (spawnSpots == null) {
			Debug.Log("No spawn points found");
			return;
		}

		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];

		GameObject myPlayer = (GameObject) PhotonNetwork.Instantiate ("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		standbyCamera.SetActive(false);
		((MonoBehaviour)myPlayer.GetComponent("MouseLook")).enabled = true;
		((MonoBehaviour)myPlayer.GetComponent("PlayerShooting")).enabled = true;
		((MonoBehaviour)myPlayer.GetComponent("PlayerMovement")).enabled = true;
		myPlayer.transform.FindChild ("Main Camera").gameObject.SetActive (true);
		}
}