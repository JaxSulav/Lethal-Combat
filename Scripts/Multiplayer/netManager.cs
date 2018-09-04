using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class netManager : MonoBehaviour {

	[SerializeField] Text connectionText;
	SpawnPoint [] spawnPoints;
	public GameObject playerPrefab;
	public GameObject standbyCamera;
	public GameObject mainAudio;

	[SerializeField] GameObject lobbyWindow;
	[SerializeField] GameObject GuideDisplay;
	[SerializeField] GameObject RadiculeAim;
	[SerializeField] InputField userName;
	[SerializeField] InputField roomName;
	[SerializeField] InputField roomList;
	[SerializeField] InputField messageWindow;

	Queue<string> messages;
	const int messageCount = 5;
	PhotonView photonView;

	void Start () {
		photonView = GetComponent<PhotonView> ();
		messages = new Queue<string> (messageCount);

		standbyCamera.GetComponent<AudioListener> ().enabled = true;
	    AudioSource BOG = mainAudio.GetComponent<AudioSource> ();
		BOG.Play ();

		PhotonNetwork.ConnectUsingSettings ("version LC0.01");

		spawnPoints = GameObject.FindObjectsOfType<SpawnPoint> ();
		StartCoroutine ("UpdateConnect");
	}

	IEnumerator UpdateConnect (){
		while (true) {
			connectionText.text = PhotonNetwork.connectionStateDetailed.ToString ();
			yield return null;
		}
	}

	void OnJoinedLobby()
	{
		GuideDisplay.SetActive (true);
		lobbyWindow.SetActive (true);

	}

	void OnReceivedRoomListUpdate()
	{
		roomList.text = "";
		RoomInfo[] rooms = PhotonNetwork.GetRoomList ();
		foreach (RoomInfo room in rooms)
			roomList.text += room.name + "\n";
	}
	public void JoinRoom()
	{
		PhotonNetwork.player.NickName = userName.text; 
		Debug.Log ("Connecting....");
		RoomOptions ro = new RoomOptions () { IsVisible = true, MaxPlayers = 10 };
		PhotonNetwork.JoinOrCreateRoom (roomName.text, ro, TypedLobby.Default);
	}

	void OnJoinedRoom()
	{
		AudioSource BG = GetComponent<AudioSource> ();
		BG.Play ();
		GuideDisplay.SetActive (false);
		lobbyWindow.SetActive (false);
		Debug.Log ("Connected!!");
		StartSpawnProcess (0f);
		StopCoroutine("UpdateConnect");
		connectionText.text = "";
	}

	void StartSpawnProcess (float respawnTime)
	{
		standbyCamera.SetActive (true);
		StartCoroutine ("SpawnPlayer", respawnTime);
	}

	IEnumerator SpawnPlayer(float respawnTime)
	{
		yield return new WaitForSeconds (respawnTime);

		SpawnPoint thePoint = spawnPoints [Random.Range (0, spawnPoints.Length)];
		GameObject pl = PhotonNetwork.Instantiate(playerPrefab.name, thePoint.transform.position, thePoint.transform.rotation, 0) as GameObject;
		AudioSource BOG = mainAudio.GetComponent<AudioSource> ();
		BOG.Stop ();


		standbyCamera.SetActive(false);
		RadiculeAim.SetActive (true);

		pl.GetComponent<NetworkPlayerAndSmoothing> ().RespawnMe += StartSpawnProcess;
 
		AddMessage ("Spawned Player: " + PhotonNetwork.player.NickName + "\n");
	}

	void AddMessage(string message)
	{
		photonView.RPC ("AddMessage_RPC", PhotonTargets.All, message);
	}

	[PunRPC]
	void AddMessage_RPC(string message)
	{
		messages.Enqueue (message);
		if (messages.Count > messageCount)
			messages.Dequeue ();

		messageWindow.text = "";
		foreach(string m in messages)
			messageWindow.text += m + "\n";
	}
}
