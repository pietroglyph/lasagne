	public void Awake()
		string path;
		string path2;
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			path = "Camera/CameraCustomMobile";
			path2 = "Camera/CameraShopMobile";
		}
		else
		{
			path = "Camera/CameraCustom";
			path2 = "Camera/CameraShop";
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(path) as GameObject) as GameObject;
		GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load(path2) as GameObject) as GameObject;
		this.m_oCurrentCamera = gameObject2.GetComponent<Camera>();
		this.m_oDefaultCamera = gameObject2.GetComponent<Camera>();
		this.MenuRefList[4].m_oCamera = gameObject.GetComponent<Camera>();
		foreach (GameObject gameObject3 in this.menuBackGroundChars)
			gameObject3.SetActive(false);
	public void Start()
	public void Update()
