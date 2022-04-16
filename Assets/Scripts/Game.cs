using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	#region Singleton
	public static Game Instance { get; private set; }

	private void Awake()
    {
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
		board.Initialize(boardSize, tileContentFactory);
		board.ShowGrid = true;
    }
    #endregion

    [SerializeField]
	Vector2Int boardSize = new Vector2Int(11, 11);

	[SerializeField]
	public GameBoard board = default;

	[SerializeField]
	GameTileContentFactory tileContentFactory = default;

	[SerializeField]
	EnemyFactory enemyFactory = default;

	[SerializeField]
	private float timeBetweenWaves = 5f;

	[SerializeField]
	private Text waveCountdownTimer;

	private float countdown = 2f;

	private int waveIndex = 0;

	EnemyCollection enemies = new EnemyCollection();

	Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);

	void OnValidate () {
		if (boardSize.x < 2) {
			boardSize.x = 2;
		}
		if (boardSize.y < 2) {
			boardSize.y = 2;
		}
	}

	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			HandleTouch();
		}

		if (Input.GetKeyDown(KeyCode.V)) {
			board.ShowPaths = !board.ShowPaths;
		}
		if (Input.GetKeyDown(KeyCode.G)) {
			board.ShowGrid = !board.ShowGrid;
		}

		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}

		countdown -= Time.deltaTime;
		waveCountdownTimer.text = Mathf.Round(countdown).ToString();

		enemies.GameUpdate();
		Physics.SyncTransforms();
		board.GameUpdate();
	}

	void HandleTouch()
	{
        GameTile tile = board.GetTile(TouchRay);
        if (tile != null)
        {
			if(board.GetTurretToBuild() == -1)
            {
				return;
            }
            else
            {
				board.BuildTurret(tile);

			}


            if (Input.GetKey(KeyCode.LeftShift))
            {
                board.UpgradeTurret(tile);
            }
        }
	}

	IEnumerator SpawnWave()
	{
		waveIndex++;

		for (int i = 0; i < waveIndex; i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(0.5f);
		}

	}

	void SpawnEnemy()
	{
		GameTile spawnPoint =
			board.GetSpawnPoint(Random.Range(0, board.SpawnPointCount));
		Enemy enemy = enemyFactory.Get();
		enemy.SpawnOn(spawnPoint);
		enemies.Add(enemy);
	}

}