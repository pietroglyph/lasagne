				bool flag = this.m_pPlayers[i].Item2.GetControlType() > RcVehicle.ControlType.Human;
				bool flag = this.m_pPlayers[i].Item2.GetControlType() > RcVehicle.ControlType.Human;
				return;
			return;
		base.networkView.RPC("DoSelectAdvantage", RPCMode.All, new object[]
			iIndex,
			(int)eAdvantage
		});
	public Tuple<GameObject, Kart>[] m_pPlayers;
