using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class LifeScript : MonoBehaviour
{

	public int minHealth;
	public Player player;

	public Sprite aliveSprite, deadSprite;

	public bool showsHealth
	{
		get
		{
			return player.health >= minHealth;
		}
	}

	void Start()
	{
		Debug.Log("started");
		aliveSprite = GetComponent<SpriteRenderer>().sprite;
	}

	// Update is called once per frame
	void Update()
	{
		GetComponent<SpriteRenderer>().sprite = showsHealth
													? aliveSprite
													: deadSprite;
	}
}
