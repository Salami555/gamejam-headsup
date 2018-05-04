using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class LifeScript : MonoBehaviour
{

	public int minHealth;
	public Player player;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<SpriteRenderer>().enabled = player.health >= minHealth;
	}
}
