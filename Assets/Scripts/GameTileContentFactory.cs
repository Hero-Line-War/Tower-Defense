using UnityEngine;

[CreateAssetMenu]
public class GameTileContentFactory : GameObjectFactory {

	[SerializeField]
	GameTileContent startPrefab = default;

	[SerializeField]
	GameTileContent destinationPrefab = default;

	[SerializeField]
	GameTileContent emptyPrefab = default;

	[SerializeField]
	GameTileContent SandBagsPrefab = default;

    [SerializeField]
    GameTileContent StandardTurretPrefab = default;

	public GameTileContent Get(GameTileContentType type)
	{
		switch (type)
		{
			case GameTileContentType.Destination: return Get(destinationPrefab);
			case GameTileContentType.Empty: return Get(emptyPrefab);
			case GameTileContentType.SandBags: return Get(SandBagsPrefab);
			case GameTileContentType.Start: return Get(startPrefab);
            case GameTileContentType.StandardTurret: return Get(StandardTurretPrefab);
		}
		Debug.Assert(false, "Unsupported type: " + type);
		return null;
	}

	public GameTileContent Get(GameTileContent prefab)
	{
		GameTileContent instance = CreateGameObjectInstance(prefab);
		instance.OriginFactory = this;
		return instance;
	}

	public void Reclaim (GameTileContent content) {
		Debug.Assert(content.OriginFactory == this, "Wrong factory reclaimed!");
		Destroy(content.gameObject);
	}

}