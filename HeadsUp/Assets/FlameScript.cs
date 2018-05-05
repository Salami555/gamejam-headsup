using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class FlameScript : MonoBehaviour
{

	public Sprite FullFlameSprite;
	public Sprite HalfFlameSprite;

	public Player player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player._grounded)
		{
			GetComponent<SpriteRenderer>().sprite = HalfFlameSprite;
		}
		else
		{
			GetComponent<SpriteRenderer>().sprite = FullFlameSprite;
		}
	}
}
