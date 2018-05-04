using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class LifeScript : MonoBehaviour
{

	public int minHealth;
	public Player player;
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<SpriteRenderer>().enabled = player.health >= minHealth;
	}
}
