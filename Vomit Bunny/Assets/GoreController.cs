using UnityEngine;
using System.Collections;

public class GoreController : MonoBehaviour {
	
	public ParticleSystem m_partsExplosion;
	public ParticleSystem m_innerGore;
	private bool mTriggerGore;
	
	public void TriggerGore()
	{
		mTriggerGore = true;
	}
	// Use this for initialization
	void Start () {
		mTriggerGore = false;
	}
	
	// Update is called once per frame
	void Update () {
		if( mTriggerGore )
		{
			m_partsExplosion.Emit( 20 );
			m_innerGore.Emit( 100 );
			mTriggerGore = false;
		}
	}
}
