using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class FlameScript : MonoBehaviour
{
	public Sprite FullFlameSprite;
	public Sprite HalfFlameSprite;

	public Player Player;
	
	// Update is called once per frame
	void Update () {
		switch (Player.Thrust)
		{
				case Player.ThrustState.FULL:
					GetComponent<SpriteRenderer>().sprite = FullFlameSprite;
					GetComponent<SpriteRenderer>().color = Color.white;
					break;
				case Player.ThrustState.HALF:
					GetComponent<SpriteRenderer>().sprite = HalfFlameSprite;
					GetComponent<SpriteRenderer>().color = Color.white;
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
