			return;
		if (component.AskForRating)
			return;
		if (component.ShowInterstitial)
		if (ASE_Tools.Available)
		{
			ASE_Flurry.LogEvent("MP_DEMARRER_MAGASIN");
		}
		if (ASE_Tools.Available)
		{
			ASE_Flurry.LogEvent("MP_DEMARRER_MODE_PRINCIPAL");
		}
				return;
			this.ActSwapMenu(EMenus.MENU_SOLO);
		if (ASE_Tools.Available)
		{
			ASE_Flurry.LogEvent("MP_DEMARRER_CHALLENGE_DU_JOUR");
		}
				return;
			this.ActSwapMenu(EMenus.MENU_CHALLENGE);
				this.ActSwapMenu((EMenus)((int)param));
			return;
		this.m_eOnHighlightTutorialExit = EMenus.MENU_WELCOME;
		GameObject.Find("EntryPoint").GetComponent<EntryPoint>().AskForRating = false;
		if (ASE_Tools.Available)
		{
			ASE_Flurry.LogEvent("MP_PARTAGE_FACEBOOK");
		}
		GameObject.Find("EntryPoint").GetComponent<EntryPoint>().AskForSharing = false;
		GameObject.Find("EntryPoint").GetComponent<EntryPoint>().DoSharing();
			ASE_Flurry.LogEvent("MP_DEMARRER_MORE_APPS");
	public override void ActSwapMenu(EMenus NextMenu)
	{
		if (ASE_Tools.Available)
		{
			if (NextMenu == EMenus.MENU_CREDITS)
			{
				ASE_Flurry.LogEvent("MP_DEMARRER_CREDITS");
			}
			else if (NextMenu == EMenus.MENU_OPTIONS)
			{
				ASE_Flurry.LogEvent("MP_DEMARRER_REGLAGES");
			}
			else if (NextMenu == EMenus.MENU_BEST_OF_GARFIELD)
			{
				ASE_Flurry.LogEvent("MP_DEMARRER_PUZZLE");
			}
			else if (NextMenu == EMenus.MENU_TUTO_HUB)
			{
				ASE_Flurry.LogEvent("MP_DEMARRER_TUTO");
			}
		}
		base.ActSwapMenu(NextMenu);
	}

