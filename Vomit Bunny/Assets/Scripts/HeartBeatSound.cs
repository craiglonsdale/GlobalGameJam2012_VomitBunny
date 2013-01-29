using UnityEngine;
using System.Collections;

public class HeartBeatSound : MonoBehaviour {
	
	private ScreenOverlay overlay;
	int qSamples = 1024;  // array size
	public float rmsValue;   // sound level - RMS
	private float[] samples; // audio samples
	
	
	public AudioClip sound;
	
	// Use this for initialization
	void Start () 
	{
		audio.clip = sound;
		audio.Play();
		audio.pitch = 1f;
		
		
		samples = new float[qSamples];
		overlay = Camera.mainCamera.GetComponent<ScreenOverlay>();
	}
	
	public void IncreaseHeartRate(float pitch)
	{
		audio.pitch = pitch;
	}
	
	
	void Update ()
	{
		audio.GetOutputData(samples, 0); // fill array with samples
		int i;
		float sum = 0f;
		for (i=0; i < qSamples; i++)
		{
			sum += samples[i]*samples[i]; // sum squared samples
		}
		rmsValue = Mathf.Sqrt(sum/qSamples); // rms = square root of average
		
		//overlay.enabled = true;
		//overlay.intensity = rmsValue * (audio.pitch - 1.0f);
		overlay.intensity = 1.0f;
		overlay.enabled = (rmsValue * (audio.pitch - 1.0f)) > 0.1f;
	}
}
