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
	Enemy prefab = default;

	[SerializeField]
	float speed = default;

	public Enemy Get()
	{
		Enemy instance = CreateGameObjectInstance(prefab);
		instance.OriginFactory = this;
		instance.Initialize(speed);
		return instance;
	}

	public void Reclaim(Enemy enemy)
	{
		Debug.Assert(enemy.OriginFactory == this, "Wrong factory reclaimed!");
		Destroy(enemy.gameObject);
	}

}
