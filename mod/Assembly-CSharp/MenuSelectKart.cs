		foreach (UnityEngine.Object @object in Resources.LoadAll("Character", typeof(CharacterCarac)))
		foreach (UnityEngine.Object object2 in Resources.LoadAll("Hat", typeof(BonusCustom)))
		foreach (UnityEngine.Object object3 in Resources.LoadAll("Kart", typeof(KartCarac)))
		foreach (UnityEngine.Object object4 in Resources.LoadAll("Kart", typeof(KartCustom)))
		foreach (UnityEngine.Object object5 in Resources.LoadAll("InApp", typeof(InAppCarac)))
		foreach (UnityEngine.Object object6 in Resources.LoadAll("Advantages", typeof(AdvantageData)))
			E_UnlockableItemSate characterState = Singleton<GameSaveManager>.Instance.GetCharacterState(Singleton<GameConfigurator>.Instance.PlayerConfig.m_eCharacter);
			if (characterState == E_UnlockableItemSate.NewUnlocked || characterState == E_UnlockableItemSate.Unlocked)
			E_UnlockableItemSate kartState = Singleton<GameSaveManager>.Instance.GetKartState(Singleton<GameConfigurator>.Instance.PlayerConfig.m_eKart);
			if (kartState == E_UnlockableItemSate.NewUnlocked || kartState == E_UnlockableItemSate.Unlocked)
			text2 = Singleton<GameConfigurator>.Instance.PlayerConfig.m_eCharacter.ToString()[0].ToString() + "_DefaultHat";
			E_UnlockableItemSate hatState = Singleton<GameSaveManager>.Instance.GetHatState(text2);
			if (hatState != E_UnlockableItemSate.NewUnlocked && hatState != E_UnlockableItemSate.Unlocked)
				text2 = Singleton<GameConfigurator>.Instance.PlayerConfig.m_eCharacter.ToString()[0].ToString() + "_DefaultHat";
			text = "K" + Singleton<GameConfigurator>.Instance.PlayerConfig.m_eKart.ToString()[0].ToString() + "C_Default";
			E_UnlockableItemSate customState = Singleton<GameSaveManager>.Instance.GetCustomState(text);
			if (customState != E_UnlockableItemSate.NewUnlocked && customState != E_UnlockableItemSate.Unlocked)
				text = "K" + Singleton<GameConfigurator>.Instance.PlayerConfig.m_eKart.ToString()[0].ToString() + "C_Default";
	public void OnDestroy()
			this.m_oPanelAdvantages.GetComponent<PanelAdvantages>().Initialize();
	public void LateUpdate()
			using (Dictionary<NetworkPlayer, bool>.Enumerator enumerator = readyToGo.GetEnumerator())
				while (enumerator.MoveNext())
					KeyValuePair<NetworkPlayer, bool> keyValuePair = enumerator.Current;
					if (keyValuePair.Value)
					{
						this.ClientState[num].ChangeTexture(this.m_oNetworkMgr.SelectedColors.IndexOf(this.m_oNetworkMgr.PlayersColor[keyValuePair.Key]));
					}
					num++;
				goto IL_101;
		if (this.goNet && this.wasServer)
		IL_101:
			if (Singleton<GameConfigurator>.Instance.PlayerConfig.GetAdvantages().Count != 0 || Network.isClient || Network.isServer || Singleton<GameConfigurator>.Instance.GameModeType == E_GameModeType.TIME_TRIAL || Singleton<ChallengeManager>.Instance.IsActive)
				this.Go(null);
				return;
			this.OnSelectTab(4);
			this.m_oButtonAdvantages.GetComponent<UICheckbox>().isChecked = true;
			Popup2Choices popup2Choices = (Popup2Choices)this.m_pMenuEntryPoint.ShowPopup(EPopUps.POPUP_DIALOG_2CHOICES, false);
			if (popup2Choices)
				Popup2Choices popup2Choices2 = popup2Choices;
				Popup2Choices.Callback oCbRight = new Popup2Choices.Callback(this.Go);
				popup2Choices2.Show("MENU_POPUP_NO_ADVANTAGE", null, oCbRight, null, "MENU_POPUP_NO", "MENU_POPUP_YES");
				return;
		LogManager instance = LogManager.Instance;
			this.ActSwapMenu(EMenus.MENU_MULTI_JOIN);
			return;
		this.ActSwapMenu(EMenus.MENU_SELECT_TRACK);
			foreach (EAdvantage eadvantage in Singleton<GameConfigurator>.Instance.PlayerConfig.GetAdvantages())
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
				Singleton<GameOptionManager>.Instance.GetInputType();
			return;
		if (!this.goNet)
		if (iTab == 4 && (Network.isClient || Network.isServer))
		{
			this.OnSelectTab(0);
			this.m_pMenuEntryPoint.ShowPopup(EPopUps.POPUP_DIALOG, false).ShowText("Boosts are disabled in multiplayer.");
			return;
		}
		if (Network.isClient || Network.isServer)
		{
			return false;
		}
		if (Network.isClient || Network.isServer)
		{
			return false;
		}
		if (!this.m_oPanelAdvantages || Network.isClient || Network.isServer)
			return;
		this.UpdateTextPanel((IconCarac)oData, e_UnlockableItemSate);
		this.m_oKartPreview.GetComponent<KartPreviewBuilder>().Build(Singleton<GameConfigurator>.Instance.PlayerConfig.m_eCharacter, Singleton<GameConfigurator>.Instance.PlayerConfig.m_eKart, Singleton<GameConfigurator>.Instance.PlayerConfig.m_oKartCustom.name, Singleton<GameConfigurator>.Instance.PlayerConfig.m_oHat.name);
				return;
			this.ActSwapMenu(EMenus.MENU_MULTI_JOIN);
