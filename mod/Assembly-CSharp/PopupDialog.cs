	public override void OnQuit()
	{
		base.OnQuit();
		if (this.OnQuitCallback != null)
		{
			this.OnQuitCallback();
		}
	}

	public PopupDialog.Callback OnQuitCallback;

	public delegate void Callback();
