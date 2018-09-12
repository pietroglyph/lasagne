	public void OnDestroy()
			return;
		if (this.netView.isMine)
		bool waitForAnim = (Network.peerType == NetworkPeerType.Disconnected) ? (this.m_pHudBonus != null) : (this.m_pHudBonus != null || this.m_pParent.GetControlType() == RcVehicle.ControlType.Net);
		this.SetItem(_item, iQuantity, waitForAnim, true);
			int i = 0;
			while (i < 2)
						return;
					if (!needToWaitAnim)
						return;
				else
					if (this.m_ItemTab[i].m_bAnimated)
					{
						flag = true;
					}
					i++;
			return;
		if (Network.peerType == NetworkPeerType.Disconnected)
			return;
		if (this.netView.isMine)
			if (Network.isServer && Singleton<GameManager>.Instance.Modifiers.BottomlessPowerups)
			{
				Array values = Enum.GetValues(typeof(EITEM));
				this.SetItem((EITEM)values.GetValue(UnityEngine.Random.Range(0, values.Length - 1)), UnityEngine.Random.Range(1, 3), false, true);
			}
	public void SetItem(EITEM _item, int iQuantity, bool waitForAnim, bool checkIfServer)
	{
		if (Singleton<GameManager>.Instance.Modifiers.SleepsOnly)
		{
			_item = EITEM.ITEM_NAP;
		}
		if (Network.isServer || !checkIfServer)
		{
			this.netView.RPC("DoSetItem", RPCMode.All, new object[]
			{
				(int)_item,
				waitForAnim,
				iQuantity
			});
			return;
		}
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			this.DoSetItem((int)_item, waitForAnim, iQuantity);
		}
	}

