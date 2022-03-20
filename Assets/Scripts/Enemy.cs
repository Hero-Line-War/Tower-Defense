using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	EnemyFactory originFactory;
	GameTile tileFrom, tileTo;
	Vector3 positionFrom, positionTo;
	float progress;
	float speed;

	public void Initialize(float speed)
	{
		this.speed = speed;
	}

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
	}

	public bool GameUpdate()
	{
		progress += Time.deltaTime * speed;
		while (progress >= 1f)
		{
			tileFrom = tileTo;
			tileTo = tileTo.NextTileOnPath;
			if (tileTo == null)
			{
				OriginFactory.Reclaim(this);
				return false;
			}
			positionFrom = positionTo;
			positionTo = tileTo.transform.localPosition;
			progress -= 1f;
		}
		transform.localPosition =
			Vector3.LerpUnclamped(positionFrom, positionTo, progress);
		return true;
	}
}
