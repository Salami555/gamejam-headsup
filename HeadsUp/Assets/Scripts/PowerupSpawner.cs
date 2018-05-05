using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class PowerupSpawner : MonoBehaviour
{

	public GameObject[] Containers;
	public float spawnTimeout;
	private float _spawnTimeout = 0;
	public int maxPowerups;
	
	public Tilemap wall;
	public List<Vector2> possibleSpawnPositions;
	
	// Use this for initialization
	void Start ()
	{
		wall = GameObject.FindGameObjectWithTag("Wall").GetComponent<Tilemap>();
		BoundsInt bounds = wall.cellBounds;
		TileBase[] allTiles = wall.GetTilesBlock(bounds);
		possibleSpawnPositions = new List<Vector2>();

		for (int x = 2; x < bounds.size.x-4; x++)
		{
			for (int y = 0; y < bounds.size.y; y++)
			{
				if (IsSpawnTile(allTiles, x, y))
				{
					possibleSpawnPositions.Add(new Vector2(x + bounds.min.x + 0.5f, y + bounds.min.y + 0.5f));
				}
			}
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		_spawnTimeout -= Time.deltaTime;
		if (_spawnTimeout <= 0)
		{
			_spawnTimeout = spawnTimeout;
			var powerupCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
			if (powerupCount < maxPowerups)
			{
				int random = Random.Range(0, Containers.Length);
				var spawnee = Containers[random];
				SpawnNew(spawnee);
			}
		}
	}
	
	private void SpawnNew(GameObject spawnee)
	{
		var spawnPos = FindSpawnPoint();
		Instantiate(spawnee, spawnPos, spawnee.GetComponent<Transform>().rotation);
	}

	private Vector2 FindSpawnPoint()
	{
		return possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Count)];
	}
	
	
	private bool IsSpawnTile(TileBase[] allTiles, int x, int y)
	{
		for (int xx = Math.Max(0, x - 1); xx < Math.Min(x + 1, wall.cellBounds.size.x - 1); xx++)
		{
			for (int yy = Math.Max(0, y - 1); yy < Math.Min(y + 1, wall.cellBounds.size.y - 1); yy++)
			{
				if (allTiles[xx + yy * wall.cellBounds.size.x] != null) return false;
			}
		}

		return true;
	}
	
}
