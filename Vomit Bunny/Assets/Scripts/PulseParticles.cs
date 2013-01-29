using UnityEngine;
using System.Collections;

public class PulseParticles : MonoBehaviour {
	
	public float pulseRate = 5.0f;
	public int defaultAmount = 10;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameObject.particleEmitter.maxEmission = (int)(Mathf.Max(0, Mathf.Sin(Time.time * pulseRate) * 40.0f)) + defaultAmount;
	}
}
