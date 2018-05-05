using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class PlayerExplodeScript : MonoBehaviour
{

	private Vector2 _randomDirection;
	private float _randomRotation;

	private Player _player;
	// Use this for initialization
	void Start()
	{
		_player = transform.GetComponentInParent<Player>();
		_randomDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
		_randomRotation = Random.Range(-50.0f, 50.0f);
	}

	// Update is called once per frame
	void Update()
	{
		if (!_player.alive)
		{
			transform.SetParent(null, true);
			transform.Rotate(0, 0, Time.deltaTime * _randomRotation);
			transform.Translate(_randomDirection * Time.deltaTime * 5.0f, Space.World);
		}
	}
}
