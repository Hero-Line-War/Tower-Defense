using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField, Range(1f, 5f)]
	float speed = 1f;

	[SerializeField, Range(1f, 300f)]
	float health = 100f;

	[SerializeField, Range(1, 50)]
	int GoldEarned = 10;

	[SerializeField, Range(1, 4)]
	int damage = 1;

	EnemyFactory originFactory;
	GameTile tileFrom, tileTo;
	
	Vector3 positionFrom, positionTo;
	float progress;
	public float Speed { get; set; }
	public float Health { get; set; }
	

	public EnemyFactory OriginFactory
	{
		get => originFactory;
		set
		{
			Debug.Assert(originFactory == null, "Redefined origin factory!");
			originFactory = value;
		}
	}

	public void SpawnOn(GameTile tile)
	{
		Debug.Assert(tile.NextTileOnPath != null, "Nowhere to go!", this);
		tileFrom = tile;
		tileTo = tile.NextTileOnPath;
		positionFrom = tileFrom.transform.localPosition;
		positionTo = tileTo.transform.localPosition;
		progress = 0f;
		Speed = speed;
		Health = health;
	}

	public bool GameUpdate()
	{
		
		if (Health <= 0f)
		{
			OriginFactory.Reclaim(this);
			Player.Money += GoldEarned;
			return false;
		}
		
		progress += Time.deltaTime * speed;
		while (progress >= 1f)
		{
			tileFrom = tileTo;
			tileTo = tileTo.NextTileOnPath;
			if (tileTo == null)
			{
				OriginFactory.Reclaim(this);
				Player.Lives -= damage;
				return false;
			}
			positionFrom = positionTo;
			positionTo = tileTo.transform.localPosition;
			progress -= 1f;
		}
		transform.localPosition = Vector3.LerpUnclamped(positionFrom, positionTo, progress);
		transform.LookAt(positionTo);
		return true;
	}


	public void ApplyDamage(float damage)
	{
		Debug.Assert(damage >= 0f, "Negative damage applied.");
		Health -= damage;
	}
}
