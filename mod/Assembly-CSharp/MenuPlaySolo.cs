		this.MultiButton.SetActive(true);
		this.MultiBackgroundButton.SetActive(true);
		this.MultiTextTitle.SetActive(true);
		if (ASE_Tools.Available)
		{
			ASE_Flurry.LogEvent("1J_DEMARRER_COURSE_UNIQUE");
		}
		this.ActSwapMenu(EMenus.MENU_CHAMPIONSHIP);
		if (ASE_Tools.Available)
		{
			ASE_Flurry.LogEvent("1J_DEMARRER_GRAND_PRIX");
		}
		this.ActSwapMenu(EMenus.MENU_CHAMPIONSHIP);
		if (ASE_Tools.Available)
		{
			ASE_Flurry.LogEvent("1J_DEMARRER_CONTRE_LA_MONTRE");
		}
		this.ActSwapMenu(EMenus.MENU_CHAMPIONSHIP);
			if (ASE_Tools.Available)
				ASE_Flurry.LogEvent("1J_TON_NOM");
			}
			string text = NGUITools.StripSymbols(this.Input.text).Trim();
			if (!string.IsNullOrEmpty(text) && text != Localization.instance.Get("MENU_PLAYER"))
			{
				Singleton<GameSaveManager>.Instance.SetPseudo(text, true);
	public override void ActSwapMenu(EMenus NextMenu)
	{
		if (ASE_Tools.Available)
		{
			if (NextMenu == EMenus.MENU_WELCOME)
			{
				ASE_Flurry.LogEvent("1J_RETOUR_MENU_PRINCIPAL");
			}
			else if (NextMenu == EMenus.MENU_MULTI)
			{
				ASE_Flurry.LogEvent("MJ_SELECTION_ONGLET_MULTI");
			}
		}
		base.ActSwapMenu(NextMenu);
	}


	public GameObject MultiButton;

	public GameObject MultiBackgroundButton;

	public GameObject MultiTextTitle;
