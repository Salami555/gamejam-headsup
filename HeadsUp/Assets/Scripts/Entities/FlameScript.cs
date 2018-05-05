using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class FlameScript : MonoBehaviour
{
	public Sprite[] FullFlameSprites;
	public Sprite[] HalfFlameSprites;

	private Player Player;

	private float timeSinceLastFlameChange;
	private int currentFrame;

	public ParticleSystem chemtrails;
	
	private void Start()
	{
		Player = transform.parent.gameObject.GetComponent<Player>();
	}

	// Update is called once per frame
	void Update ()
	{
		timeSinceLastFlameChange += Time.deltaTime;
		if (timeSinceLastFlameChange >= 0.05)
		{
			currentFrame++;
			timeSinceLastFlameChange = 0;
		}
		switch (Player.Thrust)
		{
				case Player.ThrustState.FULL:
					GetComponent<SpriteRenderer>().sprite = FullFlameSprites[currentFrame % FullFlameSprites.Length];
					GetComponent<SpriteRenderer>().color = Color.white;
					Instantiate(chemtrails, transform.position, transform.rotation);
					break;
				case Player.ThrustState.HALF:
					GetComponent<SpriteRenderer>().sprite = HalfFlameSprites[currentFrame % HalfFlameSprites.Length];
					GetComponent<SpriteRenderer>().color = Color.white;
					if (Random.value > 0.8)
					{
						Instantiate(chemtrails, transform.position, transform.rotation);
					}

					break;
				case Player.ThrustState.ZERO:
					GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
					break;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		
	}
}
