using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu]
public class EnemyFactory : GameObjectFactory
{

	[SerializeField]
	Enemy velociraptor = default;

	[SerializeField]
	Enemy caveTroll = default;

	[SerializeField]
	Enemy evilDragon = default;

	public Enemy GetPrefab(GameEnemyType type)
	{
		switch (type)
		{
			case GameEnemyType.Velociraptor: return velociraptor;
			case GameEnemyType.CaveTroll: return caveTroll;
			case GameEnemyType.EvilDragon: return evilDragon;
		}
		Debug.Assert(false, "Unsupported type: " + type);
		return null;
	}

	public Enemy Get(GameEnemyType type)
	{
		Enemy instance = CreateGameObjectInstance(GetPrefab(type));
		instance.OriginFactory = this;
		return instance;
	}

	public void Reclaim(Enemy enemy)
	{
		Debug.Assert(enemy.OriginFactory == this, "Wrong factory reclaimed!");
		Destroy(enemy.gameObject);
	}

}
