using System.Security.Cryptography;
			this.ActSwapMenu(EMenus.MENU_MULTI);
						GameObject gameObject;
						if (this.m_oButtonServerList.TryGetValue(keyValuePair.Key, out gameObject))
						{
							BtnServer component2 = gameObject.GetComponent<BtnServer>();
							keyValuePair.Value.connectedPlayers = hostData.connectedPlayers;
							component2.SetPlayerCount(hostData.connectedPlayers);
							if (array4.Length >= 7 && array4[6] != "")
							{
								this.RemoveServerFromGUI(keyValuePair.Key);
							}
						}
						else if (array4.Length >= 7 && array4[6] == "")
						{
							this.AddServerToGUI(keyValuePair.Key, keyValuePair.Value, this.GameTypeStringToInt(array4[0]), array4[2]);
						}
			foreach (int num in list)
				this.RemoveServerFromGUI(num);
				this.m_oHostDataDic.Remove(num);
				string[] array5 = hostData2.comment.Split(new char[]
				if (array5.Length <= 4 || !(array5[4] == "startGame"))
						string[] array6 = keyValuePair2.Value.comment.Split(new char[]
						{
							','
						});
						if (hostData2.guid == keyValuePair2.Value.guid && (array5.Length < 7 || array6.Length < 7 || (array5.Length >= 7 && array6.Length >= 7 && array6[6] == array5[6])))
					int iNextId = this.m_iNextId;
					this.m_iNextId = iNextId + 1;
					this.m_oHostDataDic.Add(iNextId, hostData3);
					this.AddServerToGUI(iNextId, hostData3, type, sGameName);
	private void AddServerToGUI(int iId, HostData host, int type, string sGameName)
		string[] array = host.comment.Split(new char[]
			','
		});
		if (array.Length < 7 || (array.Length >= 7 && array[6] == ""))
			this.AddServerToGUI(iId, sGameName, host.connectedPlayers, type);
	public void RemoveServerFromGUI(int iId)
		this.m_oScrollPanel.transform.GetChild(0).gameObject.SendMessage("Reposition");
		HostData hostData;
		if (this.m_oHostDataDic.TryGetValue(iId, out hostData))
			string[] array = hostData.comment.Split(new char[]
				','
			});
			if (array.Length >= 5 && this.serverId == -1)
			{
				if (hostData.connectedPlayers >= 6)
						return;
					this.sServerName = array[2];
					this.iGameType = this.GameTypeStringToInt(array[0]);
		(this.m_pMenuEntryPoint.MenuRefList[7] as MenuMultiWaitingRoom).Init(EMenus.MENU_MULTI_JOIN, this.serverId, this.sServerName, this.iGameType);
		this.ActSwapMenu(EMenus.MENU_MULTI_PLAYERS_LIST);
		this.ActSwapMenu(EMenus.MENU_SOLO);
	private void AddServerToGUI(int iId, string sServerName, int iNbPlayers, int iGameType)
	{
		if (!this.m_oScrollPanel)
		{
			return;
		}
		GameObject gameObject = this.m_oScrollPanel.transform.GetChild(0).gameObject;
		GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(this.m_oButtonServerTemplate);
		if (!gameObject2)
		{
			return;
		}
		gameObject2.transform.parent = gameObject.transform;
		this.m_oButtonServerList.Add(iId, gameObject2);
		BtnServer component = gameObject2.GetComponent<BtnServer>();
		if (!component)
		{
			return;
		}
		component.Init(iId, sServerName, iNbPlayers, iGameType, base.gameObject, this.m_oScrollPanel);
		gameObject.SendMessage("Reposition");
		if (this.NoGame && this.NoGame.activeSelf)
		{
			this.NoGame.SetActive(false);
		}
	}

	public override void Start()
	{
		foreach (BoxCollider boxCollider in base.GetComponentsInChildren<BoxCollider>())
		{
			if (boxCollider.name == "ButtonRefresh")
			{
				this.m_directConnectButtonPosition = this.m_oMenuCamera.camera.WorldToScreenPoint(boxCollider.bounds.center);
				this.m_directConnectButtonPosition.y = (float)Screen.height - this.m_directConnectButtonPosition.y;
			}
		}
		foreach (Component component in base.GetComponentsInChildren<Component>())
		{
			if (component.name == "ButtonRefresh")
			{
				component.active = false;
			}
		}
	}

	public void OnGUI()
	{
		if (this.m_guiState == MenuMultiJoin.GUIState.SHOW_POPUP_FIELDS)
		{
			this.m_gameIDToConnectTo = ModGUIHelper.CenteredTextField(new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f + 15f), new Vector2((float)Screen.width * 0.25f, 30f), this.m_gameIDToConnectTo, FontStyle.Normal, 32);
			this.m_gameIDToConnectTo = new string(Array.FindAll<char>(this.m_gameIDToConnectTo.ToCharArray(), (char c) => char.IsDigit(c)));
			return;
		}
		if (this.m_guiState == MenuMultiJoin.GUIState.SHOW_DIRECT_CONNECT && ModGUIHelper.CenteredButton(this.m_directConnectButtonPosition, new Vector2((float)Screen.width * 0.25f, (float)Screen.height * 0.1f), "Direct Connect", ""))
		{
			Popup2Choices popup2Choices = (Popup2Choices)this.m_pMenuEntryPoint.ShowPopup(EPopUps.POPUP_DIALOG_2CHOICES, false);
			popup2Choices.ShowText("Enter Game ID Below:", delegate(object unused)
			{
				this.m_guiState = MenuMultiJoin.GUIState.SHOW_DIRECT_CONNECT;
			}, new Popup2Choices.Callback(this.StartDirectConnection), new object(), "Cancel", "MENU_POPUP_OK");
			this.m_popupTextPosition = this.m_oMenuCamera.camera.WorldToScreenPoint(popup2Choices.Text.GetComponent<UILabel>().transform.position);
			this.m_guiState = MenuMultiJoin.GUIState.SHOW_POPUP_FIELDS;
		}
	}

	private void StartDirectConnection(object unused)
	{
		this.networkMgr.PrivateServerToConnectTo = this.m_gameIDToConnectTo;
		bool flag = false;
		using (SHA512 sha = new SHA512Managed())
		{
			foreach (KeyValuePair<int, HostData> keyValuePair in this.m_oHostDataDic)
			{
				string[] array = keyValuePair.Value.comment.Split(new char[]
				{
					','
				});
				if (array.Length >= 7 && array[6] == this.networkMgr.ComputeConnectionChallenge(sha, this.m_gameIDToConnectTo, array[3]))
				{
					this.OnServer(keyValuePair.Key);
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			PopupDialog popupDialog = (PopupDialog)this.m_pMenuEntryPoint.ShowPopup(EPopUps.POPUP_DIALOG, false);
			popupDialog.OnQuitCallback = delegate()
			{
				this.m_guiState = MenuMultiJoin.GUIState.SHOW_DIRECT_CONNECT;
			};
			popupDialog.ShowText("Couldn't find a server with an ID of \"" + this.m_gameIDToConnectTo + "\".");
			this.m_guiState = MenuMultiJoin.GUIState.SHOW_NONE;
			this.m_gameIDToConnectTo = "";
			return;
		}
		this.m_guiState = MenuMultiJoin.GUIState.SHOW_DIRECT_CONNECT;
	}

	private int GameTypeStringToInt(string gameTypeString)
	{
		if (gameTypeString.Equals("Single race"))
		{
			return 0;
		}
		return 1;
	}


	private Vector3 m_directConnectButtonPosition;

	private string m_gameIDToConnectTo = "";

	private Vector3 m_popupTextPosition;

	private MenuMultiJoin.GUIState m_guiState;

	private enum GUIState
	{
		SHOW_DIRECT_CONNECT,
		SHOW_POPUP_FIELDS,
		SHOW_NONE
	}
