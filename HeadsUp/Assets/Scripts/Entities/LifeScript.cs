using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class LifeScript : MonoBehaviour
{

	public int minHealth;
	public Player player;

	public Sprite deadSprite;

	public bool showsHealth
	{
		get
		{
			return player.health >= minHealth;
		}
	}

	// Update is called once per frame
	void Update()
	{
		// GetComponent<SpriteRenderer>().enabled = showsHealth;
		if (!showsHealth)
			GetComponent<SpriteRenderer>().sprite = deadSprite;
	}
}
