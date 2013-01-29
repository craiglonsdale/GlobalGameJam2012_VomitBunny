using UnityEngine;
using System.Collections;

public class BuzzSawTrap : MonoBehaviour 
{
	private Animator anim;
	private ParticleSystem bloodParticleSystem;
	private float particleDuration;
	private float currentDurationLife;
	private bool playerKilled;
	public AudioClip sound;
	
	void Start ()
	{
		Transform bloodParticles = transform.Find("BuzzSawParticle");
		bloodParticleSystem = bloodParticles.particleSystem;
		bloodParticleSystem.enableEmission = false;

		particleDuration = 5f;
		anim = GetComponent("Animator") as Animator;
		
		//bloodParticleSystem.enableEmission = false;
		playerKilled = false;
	}
	
	void Update ()
	{
		if( playerKilled )
		{
			currentDurationLife -= Time.deltaTime;
			bloodParticleSystem.enableEmission = true;
			if( currentDurationLife <= 0f )
			{
				playerKilled = false;
				currentDurationLife = 0f;
				bloodParticleSystem.enableEmission = false;
			}
		}
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag == "Player")
		{
			var player = other.collider.GetComponent("Runner") as Runner;
			if (player != null)
			{
				player.Die();
				playerKilled = true;
				currentDurationLife = particleDuration;
				
				if( audio)
				{
					audio.clip = sound;
					audio.Play();
					audio.pitch = 1f;				
				}
			}
		}
	}
}