	public void Awake()
		string path;
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			path = "Camera/followCamMobile";
		}
		else
		{
			path = "Camera/followCam";
		}
		UnityEngine.Object.Instantiate(Resources.Load(path) as GameObject);
	public IEnumerator Start()
	public void OnDestroy()
			UnityEngine.Object[] array = UnityEngine.Object.FindSceneObjectsOfType(typeof(RcPortalTrigger));
			for (int j = 0; j < array.Length; j++)
				((RcPortalTrigger)array[j]).enabled = false;
				return;
		else if (this.m_eState == GameEntryPoint.ECreationState.PlayersCreated)
			int iStep = this.m_iStep;
			this.m_iStep = iStep + 1;
			if (iStep > 5 && (Network.peerType == NetworkPeerType.Disconnected || !this.networkMgr.WaitingSynchronization))
				if (GameEntryPoint.OnVehicleCreated != null)
				{
					GameEntryPoint.OnVehicleCreated();
				}
				Singleton<GameManager>.Instance.GameMode.StartScene();
				Singleton<BonusMgr>.Instance.NullSafe(delegate(BonusMgr i)
				{
					i.StartScene();
				});
				this.m_eState = GameEntryPoint.ECreationState.SceneStarted;
