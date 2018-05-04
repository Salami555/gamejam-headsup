using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScreenShake : MonoBehaviour
{
	private Vector2 cameraPosition; //current position, without shake applied
	private float timeShaking = 99999;
	private float shakeIntensity = 0;
	private float shakeDuration = 1;
	private Vector2 shakeDirection = new Vector2(0, 0); //null if unidirectional
	
	// Use this for initialization
	void Start ()
	{
		cameraPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			MakeUndirectedShake(0.2f, 1);
		}

		timeShaking += Time.deltaTime;
		float relativeTimeShaking = timeShaking / shakeDuration;
		Vector2 random = new Vector2(0, 0);
		if (relativeTimeShaking < 1)
		{
			float currentIntensity = (1 - relativeTimeShaking) * shakeIntensity;
			float timeFactor = (float) Math.Sqrt(Time.timeScale);
			if (shakeDirection.sqrMagnitude > 0)
			{
				//directional
				random = shakeDirection * (Random.value * 2 - 1) * currentIntensity * timeFactor;
			}
			else
			{
				//un-directional
				random = new Vector2((Random.value * 2 - 1) * currentIntensity * timeFactor, (Random.value * 2 - 1) * currentIntensity * timeFactor);
			}
		}


		Vector2 actualPosition = cameraPosition + random;
		transform.position = new Vector3(actualPosition.x, actualPosition.y, -10);
	}
	
	public void MakeUndirectedShake(float intensity, float duration)
	{
		MakeDirectedShake(intensity, duration, new Vector2(0, 0));
	}

	public void MakeDirectedShake(float intensity, float duration, Vector2 direction)
	{
		shakeIntensity = intensity;
		shakeDuration = duration;
		shakeDirection = direction;
		timeShaking = 0;
	}
}