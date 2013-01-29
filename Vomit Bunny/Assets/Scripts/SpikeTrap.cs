using UnityEngine;
using System.Collections;


public class SpikeTrap : MonoBehaviour
{
	public AudioClip spikeDeath;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "Player")
		{
			var player = other.collider.GetComponent("Runner") as Runner;
			if (player != null)
			{
				player.Die();
				Debug.Log("SQUISHED");
				if(audio)
				{
					audio.clip = spikeDeath;
					audio.Play();
					audio.pitch = 1f;
				}
			}
		}
	}
}
