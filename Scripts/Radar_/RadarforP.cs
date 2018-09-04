

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarforP : MonoBehaviour {

	List<GameObject> radarObjects;
	public GameObject[] trackedObjects;
	public GameObject radarTargetPrefab;
	List<GameObject> borderObjects; 
	public float switchDistance;
	public Transform radarTransform;


	// Use this for initialization
	void Start () {
			createRadarObj ();
		}

	// Update is called once per frame
	void Update () {

			for (int i = 0; i < radarObjects.Count; i++) {
				if (Vector3.Distance (radarObjects [i].transform.position, transform.position) > switchDistance) {

					radarTransform.LookAt (radarObjects [i].transform);
					borderObjects [i].transform.position = transform.position + switchDistance * radarTransform.forward;
					borderObjects [i].layer = LayerMask.NameToLayer ("Radar");
					radarObjects [i].layer = LayerMask.NameToLayer ("nvisible");

				} else {

					borderObjects [i].layer = LayerMask.NameToLayer ("nvisible");
					radarObjects [i].layer = LayerMask.NameToLayer ("Radar");
				}
			}
	}

	void createRadarObj()
	{
		radarObjects = new List<GameObject> ();
		borderObjects = new List<GameObject> ();

		foreach ( GameObject o in trackedObjects) {


				GameObject obj = Instantiate (radarTargetPrefab, o.transform.position, Quaternion.identity) as GameObject;
				radarObjects.Add (obj);
				GameObject ob = Instantiate (radarTargetPrefab, o.transform.position, Quaternion.identity) as GameObject;
				borderObjects.Add (ob);
				


		}
	}
}
