using UnityEngine;

public class Shop : MonoBehaviour
{

    //private BuildManager buildManager;

    private void Start()
    {
        //buildManager = BuildManager.instance;
    }

    public void PurchaseStandardTurret()
    {
        Debug.Log("Tourelle standard selectionn�e");
        //buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }


}
