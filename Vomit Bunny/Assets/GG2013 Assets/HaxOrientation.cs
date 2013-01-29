using UnityEngine;
using System.Collections;

public class HaxOrientation : MonoBehaviour {
	public Transform mainCharacterHead;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		//transform.rigidbody.MovePosition(mainCharacterHead.transform.position);
		transform.position = mainCharacterHead.transform.position;
		transform.rotation = mainCharacterHead.transform.rotation * Quaternion.AngleAxis(-90.0f, new Vector3(0,1,0));
	}
}
