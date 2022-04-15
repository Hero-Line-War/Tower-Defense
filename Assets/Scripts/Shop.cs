using UnityEngine;

public class Shop : MonoBehaviour
{

    //private BuildManager buildManager;

    private void Start()
    {
        Game game;
        game = Game.instance;
        //buildManager = BuildManager.instance;
    }

    
    
    public void PurchaseStandardTurret()
    {
        Debug.Log("Tourelle standard selectionnée");
        //buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }


}
