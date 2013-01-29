using UnityEngine;
using System.Collections;
using System.Linq;

public class Runner : MonoBehaviour 
{
	private float heartRateMonitor;
	private bool mouseDown;
	
	public GameObject spawnPoint;
	public static float vomitPowerUp;
	public float MAXIMUM_DISTANCE;
	public Vector3 jumpVelocity; 
	public bool powerVomitCharged;
	
	//Debug Vars
	public static Vector3 finalThrust;
	public static Vector3 transformedForward;
	
	public HeartBeatSound heartBeatSound;
	public float maxHeartRate;
	
	private const float maxTimeToLive = 2.0f;
	private const float hold = 0.5f;

	public bool dying = false;
	public bool explode = false;

	private float timeToLive = maxTimeToLive;
	private Bloom bloom;
	private NoiseEffect noise;
    private ColorCorrectionCurves colour;

	private float fadeLevel = 0.0f;
	
	public ParticleSystem powerVomitParticle;
	
	public float particleStartSize;
	
	private float chargeForLastBoost;
	
	public GoreController explodingGore;
	public VisibilityScript visibilityScript;
	
	public Animator animator;
	private Quaternion initialRotation;
	
	private float finishTime = 0.0f;
	
	// Use this for initialization
	void Start () 
	{
		heartRateMonitor = 1f;
		transformedForward = new Vector3();
		finalThrust = new Vector3();
		powerVomitCharged = false;
		
		initialRotation = transform.rotation;
		
		bloom = Camera.mainCamera.GetComponent("Bloom") as Bloom;
		noise = Camera.mainCamera.GetComponent("NoiseEffect") as NoiseEffect;
		colour = Camera.mainCamera.GetComponent("ColorCorrectionCurves") as ColorCorrectionCurves;
		particleStartSize = particleStartSize;
		Reset();
	}
	
	void Reset()
	{
		bloom.enabled = false;
		noise.enabled = false;
		colour.enabled = false;
		dying = false;
		explode = false;
		timeToLive = maxTimeToLive;
		fadeLevel = 0.0f;
		transform.position = spawnPoint.transform.position;
		visibilityScript.SetVisibility(true);
		rigidbody.isKinematic = false;
		GameObject.Find("StartGate").GetComponent<StartEndGate>().Reset();
	}

	// Update is called once per frame
	void Update () 
	{
		if (finishTime > 0 && Time.time > finishTime + 1.0)
			Application.LoadLevel(Application.loadedLevel + 1);
		
		if(!dying  && !explode)
		{
		
			RotateBunny();
			ChargePowerVomit();
			PerformVelocityStuff();
			ReleasePowerVomit();
			
		}
		else if (dying)
		{
			rigidbody.isKinematic = true;
			timeToLive -= Time.deltaTime;
			if (timeToLive < 0.0)
			{
				if(timeToLive < (0.0 - hold))
				{
					Reset();
				}
			}
			else
			{
				bloom.enabled = true;
				noise.enabled = true;
				colour.enabled = true;
				fadeLevel += Time.deltaTime / maxTimeToLive;
			}
			
			bloom.bloomThreshhold = 0.5f - fadeLevel;
			noise.grainSize = (2f * maxTimeToLive) - timeToLive;
			colour.saturation = timeToLive / (4f * maxTimeToLive);
		}
		else if (explode)
		{
			rigidbody.isKinematic = true;
			ForceReleasePowerVomit();
			timeToLive -= Time.deltaTime;
			if (timeToLive < 0.0)
			{
				if(timeToLive < (0.0 - hold))
				{
					Reset();
				}
			}
		}

	}
	
	void RotateBunny()
	{
		Quaternion targetRotation = initialRotation;
		float lerpSpeed = 5.0f;
		if (Input.GetMouseButton(0))
		{
			animator.SetBool("Spew", true );
			float x = Input.mousePosition.x - Camera.main.WorldToScreenPoint(transform.position).x;
	    	float y = Input.mousePosition.y - Camera.main.WorldToScreenPoint(transform.position).y;
	
	    	float zRotation = Mathf.Rad2Deg * Mathf.Atan2(y, x);
			
			targetRotation = Quaternion.AngleAxis(zRotation, new Vector3(0f, 0f, 1f));
			lerpSpeed = 20.0f;
			//transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 20.0f);
		}
		else
		{
			//if (rigidbody.velocity.magnitude < 0.5)
				animator.SetBool("Spew", false );
			//else
			//	animator.SetBool("Spew", true );
		}
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * lerpSpeed);
		
	}
	
	void ForceReleasePowerVomit()
	{
		powerVomitParticle.startSize = particleStartSize * chargeForLastBoost * 0.5f;
		powerVomitParticle.Emit(200);

		powerVomitCharged = false;
		chargeForLastBoost = 0f;
	}
	
	void ReleasePowerVomit()
	{
		if(!Input.GetMouseButton(1) && powerVomitCharged)
		{
			transformedForward = transform.right;//transform.TransformDirection(Vector3.forward);

			var transformVelocity = transformedForward;
			finalThrust = transformVelocity;
			finalThrust.y = (-finalThrust.y * jumpVelocity.y) * (heartRateMonitor * 7.5f);
			finalThrust.x = (-finalThrust.x * jumpVelocity.x) * (heartRateMonitor * 7.5f);
			Debug.Log ("Vomit Boost: " + finalThrust.ToString());
			rigidbody.AddForce(finalThrust, ForceMode.Impulse);
			powerVomitParticle.startSize = particleStartSize * chargeForLastBoost * 0.75f;
			powerVomitParticle.Emit(100);

			powerVomitCharged = false;
			chargeForLastBoost = 0f;
		}
	}
	
	void ChargePowerVomit()
	{
		if(Input.GetMouseButton(1) && heartRateMonitor < maxHeartRate)
		{
			heartRateMonitor += 0.015f;
			powerVomitCharged = true;
			chargeForLastBoost += 0.015f;
			Debug.Log(heartRateMonitor.ToString());
		}
		else if(heartRateMonitor > 1f)
		{
			heartRateMonitor -= 0.005f;
		}
		
		if(heartRateMonitor > maxHeartRate && Input.GetMouseButton(1))
		{
			//EXPLODE
			Debug.Log("YOU DEAD!!!!");		
			Explode();
			heartRateMonitor = 0f;
		}
		
		heartBeatSound.IncreaseHeartRate(heartRateMonitor);
	}
	
	void PerformVelocityStuff()
	{
		//Find the distance from player to floor
		if (Input.GetMouseButton(0))
		{
			RaycastHit rayCastHitInfo = new RaycastHit();
			
			transformedForward = transform.right;//transform.TransformDirection(Vector3.forward);
			if(Physics.Raycast(transform.position, transform.right, out rayCastHitInfo, 100, 1))
			{
				if(rayCastHitInfo.distance < MAXIMUM_DISTANCE)
				{
					//Calculate the percentage...linear this time
					float steps = 100f/MAXIMUM_DISTANCE;
					
					float raycastDist = rayCastHitInfo.distance;
					if (raycastDist < 1)
						raycastDist = 1;
					
					float percentageStrength = (MAXIMUM_DISTANCE / raycastDist) * steps;
					
					var transformVelocity = transformedForward;
					finalThrust = transformVelocity;
					finalThrust.y = -finalThrust.y * jumpVelocity.y;
					finalThrust.x = -finalThrust.x * jumpVelocity.x;
					Debug.Log ("Regular Thrust:" + finalThrust.ToString());
					rigidbody.AddForce((finalThrust) * (percentageStrength/100f), ForceMode.VelocityChange);
				}
			}
		}
	}
	
	public void Die()
	{
		dying = true;
	}

	public void Explode()
	{
		explodingGore.TriggerGore();
		visibilityScript.SetVisibility(false);
		explode = true;
	}

	public void FinishLevel()
	{
		finishTime = Time.time;
	}
	
}
