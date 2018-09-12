		if (Network.InitializeServer(this.networkMgr.maxPlayers, this.networkMgr.port, !Network.HavePublicAddress()) != NetworkConnectionError.NoError)
		this.ActSwapMenu(EMenus.MENU_SOLO);
			this.ActSwapMenu(EMenus.MENU_MULTI);
			this.networkMgr.GameStage = "waitPlayers";
				this.networkMgr.GameType = "Single race";
				this.networkMgr.RegisterHost();
				this.ActSwapMenu(EMenus.MENU_MULTI_PLAYERS_LIST);
				(this.m_pMenuEntryPoint.MenuRefList[7] as MenuMultiWaitingRoom).Init(EMenus.MENU_MULTI_CREATE, 0, this.networkMgr.SGameName, 0);
				return;
			if (this.m_bNeedToCreateChampionship)
				this.networkMgr.GameType = "Championship";
				this.networkMgr.RegisterHost();
				this.ActSwapMenu(EMenus.MENU_MULTI_PLAYERS_LIST);
				(this.m_pMenuEntryPoint.MenuRefList[7] as MenuMultiWaitingRoom).Init(EMenus.MENU_MULTI_CREATE, 0, this.networkMgr.SGameName, 1);
