			this.Input.maxChars = 14;
		if (ASE_Tools.Available)
		{
			ASE_Flurry.LogEvent("MJ_SELECTION_LOCAL");
		}
		this.ActSwapMenu(EMenus.MENU_MULTI_JOIN);
		if (ASE_Tools.Available)
		{
			ASE_Flurry.LogEvent("MJ_SELECTION_EN_LIGNE");
		}
		this.ActSwapMenu(EMenus.MENU_MULTI_JOIN);
			if (ASE_Tools.Available)
			{
				ASE_Flurry.LogEvent("MJ_TON_NOM");
			}
			string text = NGUITools.StripSymbols(this.Input.text).Trim();
			if (!string.IsNullOrEmpty(text) && text != Localization.instance.Get("MENU_PLAYER"))
				Singleton<GameSaveManager>.Instance.SetPseudo(text, true);
			this.ActSwapMenu(EMenus.MENU_WELCOME);
		}
	}

	public override void ActSwapMenu(EMenus NextMenu)
	{
		if (ASE_Tools.Available && NextMenu == EMenus.MENU_WELCOME)
		{
			ASE_Flurry.LogEvent("MJ_RETOUR_MENU_PRINCIPAL");
		base.ActSwapMenu(NextMenu);
