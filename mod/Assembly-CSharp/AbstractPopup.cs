	public virtual void OnQuit()
	public void ShowText(string text)
	{
		base.gameObject.SetActive(true);
		UILabel component = this.Text.gameObject.GetComponent<UILabel>();
		if (component)
		{
			component.text = text;
		}
	}

