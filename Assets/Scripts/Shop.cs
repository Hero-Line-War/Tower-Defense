using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField, Range(1f, 100f)]
    int costSandBags = 10;

    [SerializeField, Range(1f, 100f)]
    int costStandardTurret = 100;

    [SerializeField, Range(1f, 200f)]
    int costMissileLauncher = 150;

    public Button SandBags;
    public Button StandardTurret;
    public Button MissileLauncher;

    private Button [] buttons;
    private Dictionary<Button, int> costTurret;

    private void Start()
    {
        costTurret = new Dictionary<Button, int>();
        costTurret.Add(SandBags, costSandBags);
        costTurret.Add(StandardTurret, costStandardTurret);
        costTurret.Add(MissileLauncher, costMissileLauncher);
    }

    private void Update()
    {
        foreach (KeyValuePair<Button, int> kvp in costTurret)
        {
            if (Player.Money < kvp.Value)
            {
                kvp.Key.enabled = false;
                //kvp.Key.image.color = new Color(219, 26, 26);
            }
            else
            {
                kvp.Key.enabled = true;
                //kvp.Key.image.color = new Color(255, 255, 255);
            }
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


}
