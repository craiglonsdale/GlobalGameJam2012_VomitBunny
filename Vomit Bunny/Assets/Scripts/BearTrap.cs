using UnityEngine;
using System.Collections;

public class BearTrap : MonoBehaviour 
{
	private Animator anim;
	
	void Start ()
	{
		anim = GetComponent("Animator") as Animator;
		anim.speed = 0.0f;
	}
	
	void Update ()
	{
	}
	
	void OnCollisionEnter(Collision other)
	{
		//Debug.Log("COLLISION!!!!!" + other.collider.name);
		if(other.collider.tag == "Player")
		{
			//Debug.Log("PLAYER COLLISION!!!!!");
			var player = other.collider.GetComponent("Runner") as Runner;
			if (player != null)
			{
				//Debug.Log("YOU DEAD!!!!");
				anim.speed = 1.0f;
				player.Die();
			}
		}
	}
}