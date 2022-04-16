using UnityEngine;

public class Shop : MonoBehaviour
{
    public void PurchaseSandBags()
    {
        Game.Instance.board.SetTurretToBuild(GameTileContentType.SandBags);
    }

    public void PurchaseStandardTurret()
    {
        Game.Instance.board.SetTurretToBuild(GameTileContentType.StandardTurret);
    }

    public void PurchaseMissileLauncher()
    {
        Game.Instance.board.SetTurretToBuild(GameTileContentType.MissileLauncher);
    }


}
