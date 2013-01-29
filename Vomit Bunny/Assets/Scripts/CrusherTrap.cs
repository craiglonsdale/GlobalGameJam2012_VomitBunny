using UnityEngine;
using System.Collections;

public class CrusherTrap : MonoBehaviour
{
	private Animator anim;
	
	public float offset = 0.0f;
	public float timeToLive = -1.0f;
	
	void Start()
	{
		timeToLive = offset;
		
		anim = GetComponent<Animator>();
		anim.speed = 0.0f;
	}
	
	// Use this for initialization
	void Update()
	{
		if(timeToLive <= 0.0f)
		{
			anim.speed = 1.0f;
		}
		else
		{
			timeToLive -= Time.deltaTime;
		}
	}
}
