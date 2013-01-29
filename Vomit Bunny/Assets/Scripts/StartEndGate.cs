using UnityEngine;
using System.Collections;

public class StartEndGate : MonoBehaviour
{
	public static float startTime;
	public static float endTime;
	public GUIText writeToText;
	public float currentTime;
	
	// Use this for initialization
	void Start ()
	{
		Reset();
	}
	
	public void Reset()
	{
		startTime = float.MinValue + 1.0f;
		endTime = float.MinValue;
		currentTime = 0;
	}
	
	
	// Update is called once per frame
	void Update ()
	{
		if(endTime > 0.0f)
		{
			currentTime = endTime - startTime;
		}
		else if(startTime > 0.0f)
		{
			currentTime = Mathf.Max(Time.timeSinceLevelLoad - startTime, 0.0f);
		}
		
		//TextMesh text = Camera.mainCamera.GetComponentInChildren(typeof(TextMesh)) as TextMesh;
		writeToText.text = currentTime.ToString("0.00") + " s";
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			var player = other.GetComponent("Runner") as Runner;
			
			if(player != null && (player.dying || player.explode))
			{
				return;
			}
			
			if(name == "StartGate" && (startTime < 0.0f))
			{
				startTime = Time.timeSinceLevelLoad;
			}
			else if (name == "EndGate" && (endTime < 0.0f))
			{
				endTime = Time.timeSinceLevelLoad;
				if (player != null)
				{
					Update ();
					player.FinishLevel();
				}
			}
		}
	}
}
