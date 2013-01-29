#pragma strict

var preDelay = 0.0;
var fadeInTime = 1.0;
//var lifeTime = 0.0;
var fadeOutTime = 1.0;
var maxAlpha = 1.0;
var fadeInAlpha = 1.0;

var colorName = "";

var destroyOnFadeOut = true;

private var fadeColor = Color(1.0,1.0,1.0,0.0);
private var animTime = 0.0;
private var fadingIn = true;

function Start()
{
	if (preDelay > 0)
		fadeColor.a = 0.0;
		
	SetColor(fadeColor);
}

function SetColor(color : Color)
{
	if (colorName != "")
		renderer.material.SetColor(colorName, fadeColor);
	else
		renderer.material.color = fadeColor;
}

function Init()
{
	fadingIn = true;
	if (renderer.enabled == false)
	{
		animTime = 0.0;
		fadeColor = Color(1.0,1.0,1.0,0.0);
		SetColor(fadeColor);
		renderer.enabled = true;
	}
	else if (animTime > fadeInTime + preDelay)
	{
		animTime = fadeInTime + preDelay;
		//animTime = ((fadeInTime / fadeOutTime) * (animTime - preDelay - fadeInTime));
	}
}

function FadeOut()
{
	//if (animTime < fadeInTime + preDelay)
	//{
		animTime = fadeInTime + preDelay + fadeOutTime;// - ((fadeOutTime / fadeInTime) * (animTime - preDelay));
		fadingIn = false;
	//}
}

function Update()
{
	animTime += Time.deltaTime;
	
	if (animTime < preDelay)
		return;
	
	if (animTime < fadeInTime + preDelay)
	{
		fadeColor.a = ((animTime - preDelay) / fadeInTime) * fadeInAlpha;
		//renderer.material.SetColor("color", fadeColor);
		SetColor(fadeColor);
	}
	else if (fadeOutTime > 0)
	{
		if (fadingIn)
		{
			fadeColor.a = Mathf.Lerp(fadeColor.a, maxAlpha, Time.deltaTime * 10.0);
			fadingIn = false;
		}
		else
			fadeColor.a = (maxAlpha - (animTime - preDelay - fadeInTime) / fadeOutTime);
		
		//if (fadeColor.a > maxAlpha)
		//	fadeColor.a = maxAlpha;
		
		//renderer.material.SetColor("color", fadeColor);
		SetColor(fadeColor);
		
		//print (fadeColor.a);
		
		if (fadeColor.a < 0.01)
		{
			if (destroyOnFadeOut)
				Destroy(gameObject, 0.05);
			else
				renderer.enabled = false;
		}
	}
	//print (renderer.material.color);
}