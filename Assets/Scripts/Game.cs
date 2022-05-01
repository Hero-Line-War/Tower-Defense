using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

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
		//board.ShowGrid = true;
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
	private float timeBetweenWaves = 10f;

	[SerializeField]
	private Text waveCountdownTimer;

	[SerializeField]
	private TextMeshProUGUI waveNumber;

	[SerializeField]
	private TextMeshProUGUI waveNumberGameOver;

	[SerializeField]
	private GameObject panelGameOver;

	[SerializeField]
	private GameObject panelPause;

	[SerializeField]
	private GameObject panelPlayerInfo;

	private float countdown = 10f;

	private int waveIndex = 0;


	EnemyCollection enemies = new EnemyCollection();


	Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);

    private void Start()
    {
		waveNumber.text = "0";
	}

    void OnValidate () {
		if (boardSize.x < 2) {
			boardSize.x = 2;
		}
		if (boardSize.y < 2) {
			boardSize.y = 2;
		}
	}

	void Update () {

		if(Player.Lives <= 0)
        {
			Pause();
			PanelOpenerGameOver();

		}

		if (Input.GetMouseButtonDown(0))
		{
            if (EventSystem.current.IsPointerOverGameObject())
            {
				return;
            }
			MousePrimaryClick();
		}

		if (Input.GetMouseButtonDown(1))
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}
			MouseSecondaryClick();
		}

		if (Input.GetKeyDown(KeyCode.P)) {
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

	void MousePrimaryClick()
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

	void MouseSecondaryClick()
	{
		GameTile tile = board.GetTile(TouchRay);
		if (tile != null && tile.Content.Type != GameTileContentType.Empty)
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				board.SellTurret(tile);
			}
			

		}
	}

	IEnumerator SpawnWave()
	{
		
		waveIndex++;
		waveNumber.text = waveIndex.ToString();

		for (int i = 0; i < waveIndex; i++)
		{
			SpawnEnemy(ChooseRandomEnemy());
			yield return new WaitForSeconds(0.5f);
		}

	}

	void SpawnEnemy(GameEnemyType type)
	{
		GameTile spawnPoint = board.GetSpawnPoint(Random.Range(0, board.SpawnPointCount));
		Enemy enemy = enemyFactory.Get(type);
		enemy.SpawnOn(spawnPoint);
		enemies.Add(enemy);
	}

	GameEnemyType ChooseRandomEnemy()
    {
		return (GameEnemyType) Random.Range(0, System.Enum.GetValues(typeof(GameEnemyType)).Length);
	}

	public void PanelOpenerGameOver()
	{
		if (panelGameOver != null)
		{
			panelGameOver.SetActive(true);
			waveNumberGameOver.text = waveNumber.text + " wave";
		}
	}

	public void PanelOpenerPause()
	{
		if (panelPause != null)
		{
			panelPause.SetActive(true);
			panelPlayerInfo.SetActive(false);
			Pause();
		}
	}

	public void Speed()
	{
		Time.timeScale = 2f;
	}

	public void Resume()
	{
		Time.timeScale = 1f;
	}

	public void ResumePauseMenu()
	{
		panelPause.SetActive(false);
		panelPlayerInfo.SetActive(true);
		Time.timeScale = 1f;
	}

	public void Pause()
    {
		Time.timeScale = 0f;
    }

	public void Restart()
	{
		SceneManager.LoadScene("Game");
	}

	public void Quit()
	{
		Application.Quit();
	}
}