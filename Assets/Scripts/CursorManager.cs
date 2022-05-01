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

	private Vector2 cursorHotspot;

	public void SetCursor()
    {
		switch (Game.Instance.board.GetTurretToBuild())
		{
			case 3:
				cursorHotspot = new Vector2(SandBagsCursor.width / 2, SandBagsCursor.height / 2);
				Cursor.SetCursor(SandBagsCursor, Vector2.zero, CursorMode.Auto);
				break;
			case 4:
				cursorHotspot = new Vector2(StandardTurretCursor.width / 2, StandardTurretCursor.height / 2);
				Cursor.SetCursor(StandardTurretCursor, Vector2.zero, CursorMode.Auto);
				break;
			case 5:
				cursorHotspot = new Vector2(MissileLauncherCursor.width / 2, MissileLauncherCursor.height / 2);
				Cursor.SetCursor(MissileLauncherCursor, Vector2.zero, CursorMode.Auto);
				break;
			default:
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
				break;
		}
		
	}
}
