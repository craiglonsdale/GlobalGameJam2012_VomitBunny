using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	
	public Transform following;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = following.position;
		transform.Translate(new Vector3(0, 5, -40));
		//transform.LookAt(following.position);
	}
}
