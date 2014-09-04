using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {   
	SpawnSpot[] spawnSpots;
	
	public GameObject standbyCamera;
	
	public bool offlineMode = false;
	
	// Use this for initialization
	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		Connect ();
	}
	
	// Update is called once per frame
	void Connect () {
		if(offlineMode){
			PhotonNetwork.offlineMode = true;
		}
		else{
			PhotonNetwork.ConnectUsingSettings ("alpha-0");
		}
	}
	
	void OnGUI (){
		GUILayout.Label ( PhotonNetwork.connectionStateDetailed.ToString() );
	}
	
	void OnJoinedLobby (){
		Debug.Log ("Joined Lobby");
		PhotonNetwork.JoinRandomRoom ();
	}
	
	void OnPhotonRandomJoinFailed (){
		Debug.Log ("RandomJoin Failed");
		PhotonNetwork.CreateRoom ( null );
	}
	
	void OnJoinedRoom (){
		Debug.Log ("OnJoinRoom");
		
		SpawnPlayer();
	}
	
	void SpawnPlayer(){
		
		if(spawnSpots == null){
			Debug.LogError ("WTF?!? U FORGOT TO MAKE SPAWNPOINTS");
			return;
		}
		PhotonNetwork.Instantiate ("PlayerController", Vector3.zero, Quaternion.identity, 1);
	    SpawnSpot mySpawnSpot = spawnSpots[ Random.Range (0, spawnSpots.Length) ];
		GameObject myPlayerGO = PhotonNetwork.Instantiate ("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 1);

		standbyCamera.SetActive(false);
		
/*		((MonoBehaviour)myPlayerGO.GetComponent("FPSInput Controller")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("Mouse Look")).enabled = true;
		myPlayerGO.transform.FindChild ("Main Camera").gameObject.SetActive(true);
*/
	}
} ﻿