using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerScript : MonoBehaviour
{

	public Sprite[] sprites;
	
	public float timePerImage;

	private int currentSpriteIndex;
	private float timeSinceLastChange;
	
	// Use this for initialization
	void Start ()
	{
		GetComponent<SpriteRenderer>().sprite = sprites[0];
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeSinceLastChange += Time.deltaTime;

		if (timeSinceLastChange >= timePerImage)
		{
			timeSinceLastChange = 0;
			currentSpriteIndex++;
			if (currentSpriteIndex >= sprites.Length)
			{
				currentSpriteIndex = 0;
			}
			GetComponent<SpriteRenderer>().sprite = sprites[currentSpriteIndex];
		}
	}
}
