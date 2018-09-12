	public void LateUpdate()
			this.ActSwapMenu(EMenus.MENU_CHAMPIONSHIP);
	public override void ActSwapMenu(EMenus NextMenu)
	{
		if (ASE_Tools.Available && NextMenu == EMenus.MENU_SELECT_KART)
		{
			ASE_Flurry.LogEvent("1J_SELECTION", new string[]
			{
				"DIFFICULTY",
				"CUP"
			}, new string[]
			{
				Singleton<GameConfigurator>.Instance.Difficulty.ToString(),
				Singleton<GameConfigurator>.Instance.ChampionShipData.ChampionShipName
			});
		}
		base.ActSwapMenu(NextMenu);
	}

