using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerupSpawner : MonoBehaviour
{

	public GameObject[] Containers;
	public float spawnTimeout;
	private float _spawnTimeout = 0;

	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		_spawnTimeout -= Time.deltaTime;
		if (_spawnTimeout <= 0)
		{
			_spawnTimeout = spawnTimeout;
			var powerupCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
			if (powerupCount < 3)
			{
				int random = Random.Range(0, Containers.Length);
				var spawnee = Containers[random];
				SpawnNew(spawnee);
			}
		}
	}
	
	private void SpawnNew(GameObject spawnee)
	{	
		var spawnPos = new Vector3(Random.Range(-5,5), Random.Range(-5,5), 0);
		Instantiate(spawnee, spawnPos, spawnee.GetComponent<Transform>().rotation);
	}
}
