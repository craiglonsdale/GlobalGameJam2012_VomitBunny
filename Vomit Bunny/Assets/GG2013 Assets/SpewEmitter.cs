using UnityEngine;
using System.Collections;

public class SpewEmitter : MonoBehaviour {
	
	public ParticleSystem m_mainJetSpew;
	public ParticleSystem m_mainChuckSpews;
	// Use this for initialization
	void Start () {
		m_mainJetSpew.particleSystem.enableEmission = false;
		m_mainChuckSpews.particleSystem.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetMouseButtonDown(0) )
		{
			m_mainJetSpew.particleSystem.enableEmission = true;
			m_mainChuckSpews.particleSystem.enableEmission = true;
			
		}

		if( Input.GetMouseButtonUp(0) )
		{
			m_mainJetSpew.particleSystem.enableEmission = false;
			m_mainChuckSpews.particleSystem.enableEmission = false;
		}
	}
	
}
