using UnityEngine;
using System.Collections;

public class BloodSpray : MonoBehaviour
{
	public GameObject drip;
	public GameObject bloodParticle;
	public float splatterFrequency = 0.2f;
	
	private float lastSplatterTime = 0.0f;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//float x = -(float)Input.GetAxisRaw("Vertical") * 10.0f;
		//float z = (float)Input.GetAxisRaw("Horizontal") * 10.0f;
		//transform.Translate(x * Time.deltaTime,0,z * Time.deltaTime);
		//transform.rotation = Quaternion.Euler(-z*1.5f, -x*1.5f, 0);

		if (Input.GetMouseButton(0))
		{
			lastSplatterTime += Time.deltaTime;
			
			if (lastSplatterTime > splatterFrequency)
			{
		        Vector3 ray = transform.forward;
				RaycastHit hit;
			
				lastSplatterTime -= splatterFrequency;
			
				Vector3 randomOffset = new Vector3(Random.Range (-2, 2),Random.Range (-2, 2),Random.Range (-2, 2));
				
				if (Physics.Raycast(this.transform.position + randomOffset, ray, out hit, 100, 1)) 
				{
		            GameObject splatter = (GameObject)Instantiate(drip,
						hit.point + (hit.normal * 0.1f), 
						Quaternion.FromToRotation (Vector3.up, hit.normal));

		            float scale = Random.value * 0.5f + 0.5f;
					splatter.transform.localScale = new Vector3(splatter.transform.localScale.x * scale,
																splatter.transform.localScale.y,
																splatter.transform.localScale.z * scale);
					
					GameObject splashBack = (GameObject)Instantiate( bloodParticle,
																	hit.point,Quaternion.FromToRotation (Vector3.up, hit.normal) );
																	
		            //splashBack.GetComponentInChildren(EllipsoidParticleEmitter).particleEmitter.enabled
		            float r = Random.Range (0, 359);
		            splatter.transform.RotateAround (hit.point, hit.normal, r);
					splashBack.transform.RotateAround(hit.point, hit.normal, r);
				}
			}
		}
	}
}
