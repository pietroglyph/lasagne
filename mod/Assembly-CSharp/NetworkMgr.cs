using System.Net;
using System.Security.Cryptography;
using System.Text;
		this.m_sPlayerName = "Player";
		this.m_sGameName = string.Empty;
		this.waitForNamesSynchro = true;
		this.waitForColorsSynchro = true;
		this.m_fTestingTimeout = 10f;
		this.m_bTestAsked = true;
		this.m_eConnectionTestResult = ConnectionTesterStatus.Undetermined;
			switch (this.m_eConnectionTestResult + 2)
				goto IL_133;
				goto IL_133;
				goto IL_133;
					goto IL_133;
				if (Time.time > this.m_fTimerNAT)
					goto IL_133;
				goto IL_133;
				goto IL_133;
				goto IL_133;
				goto IL_133;
				goto IL_133;
			IL_133:
		while (this.DirectConnectID == "")
		{
			RandomNumberGenerator randomNumberGenerator = new RNGCryptoServiceProvider();
			byte[] array = new byte[2];
			randomNumberGenerator.GetBytes(array);
			this.DirectConnectID = BitConverter.ToUInt16(array, 0).ToString();
			using (SHA512 sha = new SHA512Managed())
			{
				HostData[] array2 = MasterServer.PollHostList();
				for (int i = 0; i < array2.Length; i++)
				{
					string[] array3 = array2[i].comment.Split(new char[]
					{
						','
					});
					if (array3.Length >= 7 && array3[6] == this.ComputeConnectionChallenge(sha, this.DirectConnectID, array3[3]))
					{
						this.DirectConnectID = "";
						break;
					}
				}
			}
		}
			if (Network.connections.Length != 0)
			this.m_sPlayerName + ((this.m_privateServerToConnectTo != "") ? ("\a" + this.m_privateServerToConnectTo) : ""),
		this.m_privateServerToConnectTo = "";
		if (Network.isServer && this.Private)
		{
			string[] array = name.Split(new char[]
			{
				'\a'
			});
			if (array.Length < 2 || (array.Length >= 2 && array[1] != this.DirectConnectID))
			{
				base.networkView.RPC("QuitToMenu", player, new object[]
				{
					17
				});
				Network.CloseConnection(player, true);
				return;
			}
			name = array[0];
		}
		if (Network.isServer && Singleton<GameManager>.Instance.Modifiers.KickCheaters && name == GameManager.CHEATER_NAME)
		{
			Network.CloseConnection(player, true);
			return;
		}
			return;
		this.readyToGo.Add(player, ready);
			return;
		Singleton<GameConfigurator>.Instance.PlayerConfig.PlayerColor = Color.yellow;
			return;
		if (Network.isClient)
		if (Network.isServer && synchronizeIndex == this.m_SynchronizeIndex)
			this.m_SynchronizeCounter++;
			return;
		this.m_SynchronizeIndex = synchronizeIndex;
		this.m_WaitingSynchronization = false;
				return;
			if (Network.isClient)
	public string MatchmakingServer
	{
		get
		{
			return MasterServer.ipAddress;
		}
		set
		{
			IPAddress ipaddress;
			try
			{
				ipaddress = Dns.GetHostAddresses(value)[0];
				Debug.Log("Matchmaking server set to " + value);
			}
			catch
			{
				ipaddress = null;
			}
			Network.natFacilitatorIP = (MasterServer.ipAddress = ((ipaddress == null) ? value : ipaddress.ToString()));
		}
	}

	public void Start()
	{
		this.MatchmakingServer = Singleton<GameSaveManager>.Instance.GetMatchmakingServer();
		MasterServer.port = 23466;
		Network.natFacilitatorPort = 50005;
	}

	public bool Private
	{
		get
		{
			return this.m_isPrivate;
		}
		set
		{
			this.m_isPrivate = value;
		}
	}

	public string GameStage
	{
		get
		{
			return this.m_gameStage;
		}
		set
		{
			this.m_gameStage = value;
		}
	}

	public string GameType
	{
		get
		{
			return this.m_gameType;
		}
		set
		{
			this.m_gameType = value;
		}
	}

	public void RegisterHost()
	{
		using (SHA512 sha = new SHA512Managed())
		{
			MasterServer.RegisterHost("GK12", "GK12", string.Concat(new string[]
			{
				this.GameType,
				",",
				(!this.BLanOnly) ? "WAN" : "LAN",
				",",
				this.SGameName + (this.Private ? " [Private]" : ""),
				",",
				this.ExternalIP,
				",",
				this.GameStage,
				",",
				((int)this.ConnectionStatus).ToString(),
				",",
				this.Private ? this.ComputeConnectionChallenge(sha, this.DirectConnectID, this.ExternalIP) : ""
			}));
		}
	}

	public string ComputeConnectionChallenge(SHA512 shaM, string directConnectID, string salt)
	{
		return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(directConnectID + salt)));
	}

	public string DirectConnectID
	{
		get
		{
			return this.m_directConnectID;
		}
		set
		{
			this.m_directConnectID = value;
		}
	}

	public string PrivateServerToConnectTo
	{
		set
		{
			this.m_privateServerToConnectTo = value;
		}
	}

	private string m_sPlayerName;
	private string m_sGameName;
	private bool waitForNamesSynchro;
	private bool waitForColorsSynchro;
	public float m_fTestingTimeout;
	private bool m_bTestAsked;
	private ConnectionTesterStatus m_eConnectionTestResult;

	private string m_directConnectID = "";

	private bool m_isPrivate;

	private string m_gameType;

	private string m_gameStage;

	private string m_privateServerToConnectTo;
