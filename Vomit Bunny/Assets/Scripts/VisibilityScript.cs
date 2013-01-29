using UnityEngine;
using System.Collections;

public class VisibilityScript : MonoBehaviour {
	
	private bool IsVisible;
	
	public void SetVisibility( bool vis )
	{
		gameObject.SetActive(vis);
	}
	// Use this for initialization
	void Start () {
	IsVisible = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}