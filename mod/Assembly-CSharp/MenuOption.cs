		if (this.Languages)
			if (Localization.instance.languages.Length > 1)
			{
				this.Languages.SetActive(true);
			}
			else
			{
				this.Languages.SetActive(false);
			}
		}
		if (this.Controls)
		{
			this.Controls.GetComponentInChildren<UILocalize>().key = "Multiplayer";

	public GameObject Languages;
