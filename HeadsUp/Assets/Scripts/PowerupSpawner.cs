using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{

	public GameObject HeartContainer;
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
				SpawnNew();
			}
		}
	}
	
	private void SpawnNew()
	{
		var spawnPos = new Vector3(Random.Range(-5,5), Random.Range(-5,5), 0);
		Instantiate(HeartContainer, spawnPos, HeartContainer.GetComponent<Transform>().rotation);
	}
}
