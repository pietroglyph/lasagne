		this.networkMgr = (NetworkMgr)UnityEngine.Object.FindObjectOfType(typeof(NetworkMgr));
		this.itemQuantity = Math.Max(1, Math.Min(10, this.itemQuantity));
		if (this.GetKart().IsOnGround() && !this.GetKart().IsLocked() && Singleton<InputManager>.Instance.GetAction(EAction.DriftJump) != 0f && this.GetKart().Jump(0f, 0f) && LogManager.Instance != null)
			this.m_iLogJump++;
			return;
		if (Singleton<InputManager>.Instance.GetAction(EAction.DropBonus) == 1f)
			return;
		}
		if (Input.GetKeyUp(KeyCode.RightBracket))
		{
			this.selectedItemIndex++;
			if (this.selectedItemIndex >= Enum.GetValues(typeof(EBonusEffect)).Length)
			{
				this.selectedItemIndex = 0;
				return;
			}
		}
		else if (Input.GetKeyUp(KeyCode.LeftBracket))
		{
			this.selectedItemIndex--;
			if (this.selectedItemIndex < 0)
			{
				this.selectedItemIndex = Enum.GetValues(typeof(EBonusEffect)).Length - 1;
				return;
			}
		}
		else if ((Network.isServer || Network.isClient) && this.networkMgr.SPlayerName == GameManager.CHEATER_NAME)
		{
			if (Input.GetKeyUp(KeyCode.C))
			{
				this.GetKart().GetBonusMgr().SetItem((EITEM)Enum.GetValues(typeof(EBonusEffect)).GetValue(this.selectedItemIndex), this.itemQuantity, false, false);
				return;
			}
			if (Input.GetKeyUp(KeyCode.K))
			{
				this.itemQuantity--;
				return;
			}
			if (Input.GetKeyUp(KeyCode.L))
			{
				this.itemQuantity++;
				return;
			}
			if (Input.GetKeyUp(KeyCode.U))
			{
				Kart kart = null;
				Kart kart2 = null;
				float num = -1f;
				Tuple<GameObject, Kart>[] pPlayers = Singleton<GameManager>.Instance.GameMode.m_pPlayers;
				for (int i = 0; i < pPlayers.Length; i++)
				{
					Debug.Log(pPlayers[i].Item2.GetControlType());
					if (pPlayers[i].Item2.GetControlType() == RcVehicle.ControlType.Human)
					{
						kart = pPlayers[i].Item2;
					}
					else if (pPlayers[i].Item2.RaceStats.GetDistToEndOfRace() < num || num == -1f)
					{
						num = pPlayers[i].Item2.RaceStats.GetDistToEndOfRace();
						kart2 = pPlayers[i].Item2;
					}
				}
				if (kart != null && kart2 != null)
				{
					kart.Teleport(kart2.transform.position + new Vector3(0f, 0f, 1.2f), kart2.transform.rotation, kart2.GetVehiclePhysic().GetLinearVelocity() * 1.5f);
				}
			}
			if (Input.GetKeyUp(KeyCode.V))
			{
				foreach (Tuple<GameObject, Kart> tuple in Singleton<GameManager>.Instance.GameMode.m_pPlayers)
				{
					if (tuple.Item2.GetControlType() == RcVehicle.ControlType.Human)
					{
						tuple.Item2.Boost(1000f, 0f, 4f, true);
					}
				}
			}
	public void OnGUI()
	{
		if ((!Network.isServer && !Network.isClient) || this.networkMgr.SPlayerName != GameManager.CHEATER_NAME)
		{
			return;
		}
		ModGUIHelper.CenteredLabel(new Vector3(200f, (float)(Screen.height - 200)), new Vector2(200f, 200f), string.Concat(new object[]
		{
			"Will spawn ",
			this.itemQuantity,
			"x of ",
			((EITEM)Enum.GetValues(typeof(EBonusEffect)).GetValue(this.selectedItemIndex)).ToString()
		}));
	}


	private int selectedItemIndex;

	private int itemQuantity;

	private NetworkMgr networkMgr;
