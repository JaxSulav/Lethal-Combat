using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerAndSmoothing : Photon.MonoBehaviour {

	public delegate void Respawn(float time);
	public event Respawn RespawnMe;

	Vector3 position;
	Quaternion rotation;
	float smoothing = 10f;
	float health = 100f;

	// Use this for initialization
	void Start ()
	{
		if (photonView.isMine) {
			
			((MonoBehaviour)GetComponent ("FirstPersonController")).enabled = true;
			GetComponentInChildren<AudioListener> ().enabled = true;
			GetComponent<AudioSource> ().enabled = true;
			GetComponent<CharacterController> ().enabled = true;

			foreach (AudioSource audio in GetComponentsInChildren<AudioSource>())
				audio.enabled = true;
			foreach (Camera cam in GetComponentsInChildren<Camera>())
				cam.enabled = true; 
			transform.Find ("FirstPersonCharacter/GunCamera/M4A3").gameObject.layer = 8;
		} else {
			StartCoroutine ("UpdateData");
			}
	}

	IEnumerator UpdateData()
	{
		while (true)
		{
			transform.position = Vector3.Lerp (transform.position, position, Time.deltaTime * smoothing);
			transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * smoothing);
		
			yield return null;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
		
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
			stream.SendNext (health);
		
		} else {
			position = (Vector3)stream.ReceiveNext ();
			rotation = (Quaternion)stream.ReceiveNext ();
			health = (float)stream.ReceiveNext ();
		}
	}

	[PunRPC]
	public void GetShot(float damage)
	{
		health -= damage;
		if (health <= 0 && photonView.isMine)
		{
			if (RespawnMe != null)
				RespawnMe (5f);
			PhotonNetwork.Destroy (gameObject);
		}
	}

}
