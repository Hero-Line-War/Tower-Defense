using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Singleton
    public static Shop Instance { get; private set; }

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
    }
    #endregion

    [SerializeField, Range(1f, 100f)]
    int costSandBags = 10;

    [SerializeField, Range(1f, 100f)]
    int costStandardTurret = 100;

    [SerializeField, Range(1f, 200f)]
    int costMissileLauncher = 150;

    public Button SandBags;
    public Button StandardTurret;
    public Button MissileLauncher;

    public Text textCostSandBags;
    public Text textCostStandardTurret;
    public Text textCostMissileLauncher;

    public Image PadlockSandBags;
    public Image PadlockStandardTurret;
    public Image PadlockMissileLauncher;

    private Button [] buttons;
    private Image[] images;
    private Dictionary<Button, int> costTurret;

    private int count;

    private void Start()
    {
        costTurret = new Dictionary<Button, int>();
        costTurret.Add(SandBags, costSandBags);
        costTurret.Add(StandardTurret, costStandardTurret);
        costTurret.Add(MissileLauncher, costMissileLauncher);
        textCostSandBags.text = costSandBags.ToString();
        textCostStandardTurret.text = costStandardTurret.ToString();
        textCostMissileLauncher.text = costMissileLauncher.ToString();
        images = new Image[] { PadlockSandBags, PadlockStandardTurret, PadlockMissileLauncher };
    }

    private void Update()
    {
        count = 0;
        foreach (KeyValuePair<Button, int> kvp in costTurret)
        {
            if (Player.Money < kvp.Value)
            {
                kvp.Key.enabled = false;
                images[count].enabled = true;
            }
            else
            {
                kvp.Key.enabled = true;
                images[count].enabled = false;
            }
            count++;
        }

    }


    public void SelectSandBags()
    {
        Game.Instance.board.SetTurretToBuild(GameTileContentType.SandBags, costSandBags);
        CursorManager.Instance.SetCursor();
    }

    public void SelectStandardTurret()
    {
        Game.Instance.board.SetTurretToBuild(GameTileContentType.StandardTurret, costStandardTurret);
        CursorManager.Instance.SetCursor();
    }

    public void SelectMissileLauncher()
    {
        Game.Instance.board.SetTurretToBuild(GameTileContentType.MissileLauncher, costMissileLauncher);
        CursorManager.Instance.SetCursor();
    }

    public int GetCostTurret(GameTileContentType type)
    {
        switch ((int) type)
        {
            case 3:
                return costSandBags;
            case 4:
                return costStandardTurret;
            case 5:
                return costMissileLauncher;
            default:
                return -1;
        }
    }

}
