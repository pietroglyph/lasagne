	public MenuMultiWaitingRoom()
	{
		this.columnOneX = (float)Screen.width / 3f;
		this.rowIncrement = (float)Screen.height / 5f;
		this.buttonSize = new Vector2(this.columnOneX - 30f, this.rowIncrement - 20f);
		this.columnTwoX = this.columnOneX * 2f;
	}

			return;
		this.IconType.ChangeTexture(0);
		int num = 0;
		int num2 = 0;
			this.m_oPlayerColor.Add(num2, keyValuePair.Value);
			num2++;
		num2 = 0;
			this.m_oPlayerList.Add(num2, keyValuePair2.Value);
			num2++;
		if (this.m_iLastNbPeers != num2)
			this.m_iLastNbPeers = num2;
		using (Dictionary<int, string>.Enumerator enumerator3 = this.m_oPlayerList.GetEnumerator())
			while (enumerator3.MoveNext())
				KeyValuePair<int, string> keyValuePair3 = enumerator3.Current;
				if (this.m_oPlayerLabel[num])
				{
					this.m_oPlayerLabel[num].text = keyValuePair3.Value;
				}
				if (this.PlayerBackground[num])
				{
					this.PlayerBackground[num].color = this.m_oPlayerColor[num];
					this.PlayerBackground[num].gameObject.SetActive(true);
				}
				num++;
			goto IL_1FC;
		IL_1AD:
		if (this.m_oPlayerLabel[num])
			this.m_oPlayerLabel[num].text = string.Empty;
		if (this.PlayerBackground[num])
			this.PlayerBackground[num].gameObject.SetActive(false);
		num++;
		IL_1FC:
		if (num >= 6)
		{
			if (this.m_oNbPlayersLabel)
			{
				this.m_oNbPlayersLabel.text = string.Format("{0} / 6 ", this.networkMgr.NbPeers) + Localization.instance.Get("MENU_PLAYERS");
			}
			return;
		}
		goto IL_1AD;
		foreach (UISlicedSprite uislicedSprite in base.GetComponentsInChildren<UISlicedSprite>())
		{
			if (uislicedSprite.spriteName == "gui_back_inactive")
			{
				Vector3 vector = this.m_oMenuCamera.camera.WorldToScreenPoint(uislicedSprite.transform.position);
				Vector3 vector2 = this.m_oMenuCamera.camera.WorldToScreenPoint(uislicedSprite.cachedTransform.lossyScale);
				Rect rect = new Rect(vector.x - vector2.x / 2f, vector.y - vector2.y / 2f, vector2.x, vector2.y);
				int num = 200;
				this.m_serverVisibilityPositioningRect = new Rect((float)Screen.width / 2f - (float)num / 2f, 0f, (float)num, 0f)
				{
					yMin = rect.yMax - 0.25f * rect.height + 15f,
					yMax = rect.yMax - 10f
				};
			}
		}
		this.ActSwapMenu(EMenus.MENU_MULTI);
		this.ActSwapMenu(EMenus.MENU_CHAMPIONSHIP);
		this.networkMgr.GameStage = "startGame";
		this.networkMgr.RegisterHost();
		this.ActSwapMenu(EMenus.MENU_SOLO);
	public void OnGUI()
	{
		if (!Network.isServer)
		{
			return;
		}
		if (GUI.Button(new Rect(100f, (float)Screen.height - 200f, 200f, 200f), new GUIContent(), new GUIStyle()))
		{
			this.OnBackButton();
		}
		Rect serverVisibilityPositioningRect = this.m_serverVisibilityPositioningRect;
		float num = (float)Screen.width * 0.08f;
		serverVisibilityPositioningRect.x -= num;
		if (ModGUIHelper.RectButton(serverVisibilityPositioningRect, "Game Settings", ""))
		{
			this.m_isSettingsMenuOpen = true;
		}
		Rect serverVisibilityPositioningRect2 = this.m_serverVisibilityPositioningRect;
		serverVisibilityPositioningRect2.xMin = serverVisibilityPositioningRect.xMax;
		serverVisibilityPositioningRect2.xMax += num;
		GUI.Box(serverVisibilityPositioningRect2, new GUIContent
		{
			text = string.Format("Your game is currently {0}." + (this.networkMgr.Private ? " Other users can direct connect using the ID: <b>{1}</b>" : ""), this.networkMgr.Private ? "private" : "public", this.networkMgr.DirectConnectID)
		}, new GUIStyle(GUI.skin.box)
		{
			wordWrap = true,
			richText = true,
			alignment = TextAnchor.MiddleCenter
		});
		if (this.m_isSettingsMenuOpen)
		{
			this.DrawSettingsWindow();
		}
	}

	public new void Start()
	{
		foreach (UIButtonScale uibuttonScale in base.GetComponentsInChildren<UIButtonScale>())
		{
			if (uibuttonScale.name == "ButtonPrev")
			{
				Transform componentInChildren = uibuttonScale.GetComponentInChildren<Transform>();
				componentInChildren.localScale = new Vector3(-50f, 50f);
				uibuttonScale.tweenTarget = componentInChildren;
			}
		}
	}

	private void DrawSettingsWindow()
	{
		GUI.Box(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), "");
		ModGUIHelper.CenteredLabel(new Vector3((float)Screen.width / 2f, this.rowIncrement * 1.5f), this.buttonSize, "Settings", 32);
		if (ModGUIHelper.CenteredButton(new Vector3(80f, 80f), new Vector2(60f, 60f), "X", ""))
		{
			this.m_isSettingsMenuOpen = false;
		}
		if (ModGUIHelper.CenteredButton(new Vector3(this.columnOneX, this.rowIncrement * 2f), this.buttonSize, "Make " + ((!this.networkMgr.Private) ? "Private" : "Public"), ""))
		{
			this.networkMgr.Private = !this.networkMgr.Private;
			this.networkMgr.RegisterHost();
		}
		this.ModifierToggleButton(ref Singleton<GameManager>.Instance.Modifiers.KickCheaters, this.columnTwoX, 2f, "Kick Baddies", "");
		this.ModifierToggleButton(ref Singleton<GameManager>.Instance.Modifiers.JumpyBots, this.columnOneX, 3f, "Jumpy Bots", "Make NPCs drive... differently.");
		this.ModifierToggleButton(ref Singleton<GameManager>.Instance.Modifiers.BottomlessPowerups, this.columnTwoX, 3f, "Unlimited Powerups", "Never run out of powerups.");
		this.ModifierToggleButton(ref Singleton<GameManager>.Instance.Modifiers.SleepsOnly, this.columnOneX, 4f, "Sleeps Only", "Only give players sleep powerups.");
		if (GUI.tooltip != "")
		{
			GUI.Box(new Rect(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y, 150f, 38f), GUI.tooltip, new GUIStyle(GUI.skin.box)
			{
				wordWrap = true,
				alignment = TextAnchor.MiddleCenter
			});
		}
	}

	private void ModifierToggleButton(ref bool modifier, float xPosition, float row, string text, string tooltip)
	{
		if (ModGUIHelper.CenteredButton(new Vector3(xPosition, this.rowIncrement * row), this.buttonSize, text, tooltip, modifier))
		{
			modifier = !modifier;
		}
	}


	private List<Tuple<string, Rect>> m_componentPositions = new List<Tuple<string, Rect>>();

	private Rect m_serverVisibilityPositioningRect;

	private bool m_isPublic = true;

	private bool m_isSettingsMenuOpen;

	private float columnOneX;

	private float rowIncrement;

	private Vector2 buttonSize;

	private float columnTwoX;
