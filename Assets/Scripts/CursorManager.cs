using UnityEngine;

public class CursorManager : MonoBehaviour
{

	#region Singleton
	public static CursorManager Instance { get; private set; }

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

	[SerializeField]
	Texture2D SandBagsCursor = default;

	[SerializeField]
	Texture2D StandardTurretCursor = default;

	[SerializeField]
	Texture2D MissileLauncherCursor = default;


    public void SetCursor()
    {
		switch (Game.Instance.board.GetTurretToBuild())
		{
			case 3:
				Cursor.SetCursor(SandBagsCursor, Vector2.zero, CursorMode.Auto);
				break;
			case 4:
				Cursor.SetCursor(StandardTurretCursor, Vector2.zero, CursorMode.Auto);
				break;
			case 5:
				Cursor.SetCursor(MissileLauncherCursor, Vector2.zero, CursorMode.Auto);
				break;
			default:
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
				break;
		}
		
	}
}
