using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class PlayerExplodeScript : MonoBehaviour
{

	private Vector2 _randomDirection;
	
	private Player _player;
	// Use this for initialization
	void Start ()
	{
		_player = transform.GetComponentInParent<Player>();
		_randomDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
	}
	
	// Update is called once per frame
	void Update () {
		if (_player.health <= 0)
		{
			transform.Translate(_randomDirection * Time.deltaTime * 20.0f);
		}
	}
}
