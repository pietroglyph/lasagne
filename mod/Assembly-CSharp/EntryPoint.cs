	public void Awake()
	public void Start()
			ASE_Flurry.Init(this.FlurryApiID);
	public void Update()
	public void LateUpdate()
	public void OnApplicationQuit()
	{
		if (ASE_Tools.Available)
		{
			ASE_Flurry.Stop();
		}
	}

	public void OnApplicationPause(bool goingPause)
	{
		if (ASE_Tools.Available)
		{
			if (goingPause)
			{
				ASE_Flurry.Stop();
			}
			else
			{
				ASE_Flurry.Init(this.FlurryApiID);
			}
		}
	}

	private void SetupScreen()
	{
		if (SystemInfo.deviceModel == "Amazon KFAPWA" || SystemInfo.deviceModel == "Amazon KFAPWI" || SystemInfo.deviceModel == "Amazon KFTHWA" || SystemInfo.deviceModel == "Amazon KFTHWI" || SystemInfo.deviceModel == "Amazon KFSOWI")
		{
			if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
			{
				Screen.orientation = ScreenOrientation.LandscapeRight;
			}
			else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
			{
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}
		}
		else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
		}
		else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
		{
			Screen.orientation = ScreenOrientation.LandscapeRight;
		}
	}

	public string FlurryApiID = "YOUR_FLURRY_API_KEY";

