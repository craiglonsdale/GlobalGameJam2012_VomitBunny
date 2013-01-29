#pragma strict

var maxTimeToLive = 2.0;
var hold = 0.5;

var dying = false;
var explode = false;

var timeToLive = maxTimeToLive;
var bloom : Bloom;
var noise : NoiseEffect;
var colour : ColorCorrectionCurves;

var fadeLevel = 0.0;

function Start ()
{
	bloom = Camera.mainCamera.GetComponent("Bloom") as Bloom;
	noise = Camera.mainCamera.GetComponent("NoiseEffect") as NoiseEffect;
	colour = Camera.mainCamera.GetComponent("ColorCorrectionCurves") as ColorCorrectionCurves;
	
	Reset();
}

function Reset()
{
	bloom.enabled = false;
	noise.enabled = false;
	colour.enabled = false;
	dying = false;
	explode = false;
	timeToLive = maxTimeToLive;
	fadeLevel = 0.0;
	
	
}

function Update ()
{
	if(dying)
	{
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
		
		bloom.bloomThreshhold = 0.5 - fadeLevel;
		noise.grainSize = (2 * maxTimeToLive) - timeToLive;
		colour.saturation = timeToLive / (4 * maxTimeToLive);
	}
}

function Die()
{
	dying = true;
}

function Explode()
{
	explode = true;
}