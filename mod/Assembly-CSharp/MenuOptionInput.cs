using System.Diagnostics;
using System.IO;
using System.Reflection;
	public MenuOptionInput()
	{
		this.m_matchmakingServers = new Tuple<string, string>[3];
		this.m_matchmakingServers[0] = new Tuple<string, string>("Pietroglyph's Server", MenuOptionInput.defaultMatchmakingServer);
		this.m_matchmakingServers[1] = new Tuple<string, string>("Vanilla Game Server", "94.23.51.63");
		this.m_matchmakingServers[2] = new Tuple<string, string>("Custom", "");
		string text = Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path);
		this.m_lastPatchedTimestamp = File.GetLastWriteTime(text).ToString("hh:mm tt on MM/dd/yyyy");
		FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(text);
		this.m_version = versionInfo.FileMajorPart + "." + versionInfo.FileMinorPart;
	}

		this.m_pBtnGyro.active = false;
		this.m_pBtnTouched.active = false;
		this.m_pGyroSlider.active = false;
		if (this.m_relabelled)
		{
			this.UpdateMatchmakingServerFromGameSave();
			return;
		}
		foreach (UILocalize uilocalize in base.GetComponentsInChildren<UILocalize>())
			if (uilocalize.key == "MENU_CONTROLE")
			{
				uilocalize.key = "Multiplayer Settings";
			}
			else if (uilocalize.key == "MENU_BT_GYRO")
			{
				uilocalize.key = "Matchmaking Server";
			}
			else if (uilocalize.key == "MENU_BT_SENSIBILITY")
			{
				this.m_serverNameLabel = uilocalize;
				this.m_serverNameLabel.key = this.m_matchmakingServers[this.m_currentMatchmakingServerIndex].Item1;
				this.m_serverNameLabel.Localize();
			}
			else if (uilocalize.key == "MENU_BT_TOUCH")
				uilocalize.key = "About";
				this.m_secondPanelTitlePosition = this.m_oMenuCamera.camera.WorldToScreenPoint(uilocalize.transform.position);
				UILabel componentInChildren = uilocalize.GetComponentInChildren<UILabel>();
				if (componentInChildren != null)
				{
					componentInChildren.active = false;
				}
		}
		this.m_relabelled = true;
	}

	public override void OnExit()
	{
		this.UpdateMatchmakingServerInGameSave();
		base.OnExit();
	}

	public void OnGUI()
	{
		this.DrawFirstPanel();
		this.DrawSecondPanel();
	}

	public override void Awake()
	{
		base.Awake();
		this.UpdateMatchmakingServerFromGameSave();
		this.m_networkManager = (NetworkMgr)UnityEngine.Object.FindObjectOfType(typeof(NetworkMgr));
	}

	private void UpdateMatchmakingServerFromGameSave()
	{
		string matchmakingServer = Singleton<GameSaveManager>.Instance.GetMatchmakingServer();
		int num = -1;
		Tuple<string, string>[] matchmakingServers = this.m_matchmakingServers;
		for (int i = 0; i < matchmakingServers.Length; i++)
		{
			if (matchmakingServers[i].Item2 == matchmakingServer)
				num = i;
				break;
		if (num == -1)
		{
			num = 2;
			this.m_matchmakingServers[num].Item2 = matchmakingServer;
		}
		this.m_currentMatchmakingServerIndex = num;
	private void UpdateMatchmakingServerInGameSave()
		Singleton<GameSaveManager>.Instance.SetMatchmakingServer(this.m_matchmakingServers[this.m_currentMatchmakingServerIndex].Item2, true);
		this.m_networkManager.MatchmakingServer = this.m_matchmakingServers[this.m_currentMatchmakingServerIndex].Item2;
	private void DrawFirstPanel()
		Vector3 vector = this.m_oMenuCamera.camera.WorldToScreenPoint(this.m_serverNameLabel.transform.position);
		vector.y += 200f;
		float x = 270f;
		int num = this.m_currentMatchmakingServerIndex;
		if (ModGUIHelper.CenteredTickerButton(new Vector2(vector.x - 50f, vector.y), ModGUIHelper.TickerDirection.LEFT))
		{
			num--;
		}
		else if (ModGUIHelper.CenteredTickerButton(new Vector2(vector.x + 50f, vector.y), ModGUIHelper.TickerDirection.RIGHT))
		{
			num++;
		}
		if (num != this.m_currentMatchmakingServerIndex)
		{
			if (num >= this.m_matchmakingServers.Length)
			{
				num = 0;
			}
			else if (num < 0)
			{
				num = this.m_matchmakingServers.Length - 1;
			}
			this.m_currentMatchmakingServerIndex = num;
			this.m_serverNameLabel.key = this.m_matchmakingServers[this.m_currentMatchmakingServerIndex].Item1;
			this.m_serverNameLabel.Localize();
			this.UpdateMatchmakingServerInGameSave();
		}
		vector.y -= 100f;
		string text = ModGUIHelper.CenteredTextField(vector, new Vector2(x, 42f), this.m_matchmakingServers[this.m_currentMatchmakingServerIndex].Item2, (this.m_matchmakingServers[this.m_currentMatchmakingServerIndex].Item1 == "Custom") ? FontStyle.Normal : FontStyle.Italic, 32);
		if (this.m_matchmakingServers[this.m_currentMatchmakingServerIndex].Item1 == "Custom")
		{
			if (this.m_matchmakingServers[this.m_currentMatchmakingServerIndex].Item2 != text)
			{
				this.m_matchmakingServers[this.m_currentMatchmakingServerIndex].Item2 = text;
				this.m_customServerUnsaved = true;
			}
			if (this.m_customServerUnsaved)
			{
				vector.y += 250f;
				if (ModGUIHelper.CenteredButton(vector, new Vector2(80f, 30f), "Save", ""))
				{
					this.m_customServerUnsaved = false;
					this.UpdateMatchmakingServerInGameSave();
				}
			}
		}
	private void DrawSecondPanel()
		Vector3 vector = new Vector3(this.m_secondPanelTitlePosition.x, this.m_secondPanelTitlePosition.y + 258f);
		string text = string.Concat(new string[]
		{
			"Version ",
			this.m_version,
			".\r\n\r\nGame patched on ",
			this.m_lastPatchedTimestamp,
			".\r\n\r\nThis mod was written in 2018 by Declan Freeman-Gleason. I originally reversed this game and saw that multiplayer was already in the code, but disabled, so I enabled it and added some features. Then, literally the day before I was going to release the patch they enabled mutliplayer in the beta channel of the game (lucky me), so I added hacks. You can view more of my work at https://github.com/pietroglyph."
		});
		ModGUIHelper.CenteredLabel(vector, new Vector2(120f, 500f), text);
		if (ModGUIHelper.CenteredButton(new Vector3(vector.x, this.m_secondPanelTitlePosition.y), new Vector2(200f, 70f), "Mod Website", ""))
		{
			Process.Start("https://github.com/pietroglyph/lasagne");
		}

	private bool m_relabelled;

	private Tuple<string, string>[] m_matchmakingServers;

	private int m_currentMatchmakingServerIndex;

	private const string k_customServerKey = "Custom";

	private NetworkMgr m_networkManager;

	public static string defaultMatchmakingServer = "matchmaking.lasagne.margo.ml";

	private UILocalize m_serverNameLabel;

	private bool m_customServerUnsaved;

	private Vector3 m_secondPanelTitlePosition;

	private string m_lastPatchedTimestamp;

	private string m_version;
