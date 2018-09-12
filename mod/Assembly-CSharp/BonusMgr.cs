				foreach (Kart kart in (Kart[])UnityEngine.Object.FindObjectsOfType(typeof(Kart)))
			BonusEntity[] array = this.m_pPieEntities;
			BonusEntity[] pEntities = array;
			this.DestroyEntities(pEntities);
			array = this.m_pAutolockPieEntities;
			pEntities = array;
			this.DestroyEntities(pEntities);
			array = this.m_pSpringEntities;
			pEntities = array;
			this.DestroyEntities(pEntities);
			array = this.m_pMagicEntities;
			pEntities = array;
			this.DestroyEntities(pEntities);
			array = this.m_pDiamondEntities;
			pEntities = array;
			this.DestroyEntities(pEntities);
			break;
			break;
			break;
		case EITEM.ITEM_LASAGNA:
		case EITEM.ITEM_NAP:
		case EITEM.ITEM_PARFUME:
			return;
			break;
			break;
			break;
		default:
			return;
			return;
			return;
		{
			SpringBonusEntity[] pSpringEntities = this.m_pSpringEntities;
			int num = this.nbSpring;
			this.nbSpring = num + 1;
			pSpringEntities[num] = _Bonus.GetComponentInChildren<SpringBonusEntity>();
			return;
		}
		case EITEM.ITEM_LASAGNA:
		case EITEM.ITEM_NAP:
		case EITEM.ITEM_PARFUME:
			return;
		{
			DiamondBonusEntity[] pDiamondEntities = this.m_pDiamondEntities;
			int num2 = this.nbDiamond;
			this.nbDiamond = num2 + 1;
			pDiamondEntities[num2] = _Bonus.GetComponentInChildren<DiamondBonusEntity>();
			return;
		}
			return;
			int num3 = 0;
			while (num3 < _Bonus.transform.childCount && bonusEntity == null)
				bonusEntity = _Bonus.transform.GetChild(num3).GetComponent<MagicBonusEntity>();
				num3++;
			MagicBonusEntity[] pMagicEntities = this.m_pMagicEntities;
			int num4 = this.nbMagic;
			this.nbMagic = num4 + 1;
			pMagicEntities[num4] = (MagicBonusEntity)bonusEntity;
			return;
		default:
			return;
				(this.ActiveLastUsed(_Item, _Kart) as PieBonusEntity).Launch(_Behind);
				return;
			if (Network.isServer)
				(this.ActiveLastUsed(_Item, _Kart) as PieBonusEntity).NetLaunch(_Kart.networkViewID, _Behind);
				return;
				(this.ActiveLastUsed(_Item, _Kart) as AutolockPieBonusEntity).Launch(_Kart, _Behind);
				return;
			if (Network.isServer)
				(this.ActiveLastUsed(_Item, _Kart) as AutolockPieBonusEntity).NetLaunch(_Kart.networkViewID, _Behind);
				return;
			if (!_Behind)
				_Kart.GetBonusMgr().GetBonusEffectMgr().ActivateBonusEffect(EBonusEffect.BONUSEFFECT_JUMP);
				return;
			if (Network.peerType == NetworkPeerType.Disconnected)
				(this.ActiveLastUsed(_Item, _Kart) as SpringBonusEntity).Launch();
				return;
			}
			if (Network.isServer)
			{
				(this.ActiveLastUsed(_Item, _Kart) as SpringBonusEntity).NetLaunch(_Kart.networkViewID);
				return;
			return;
				(this.ActiveLastUsed(_Item, _Kart) as DiamondBonusEntity).Launch(_Behind);
				return;
			if (Network.isServer)
				(this.ActiveLastUsed(_Item, _Kart) as DiamondBonusEntity).NetLaunch(_Kart.networkViewID, _Behind);
				return;
			return;
				if (kart != null && kart.Index != _Kart.Index && kart.RaceStats.GetRank() < rank)
					ParfumeBonusEffect parfumeBonusEffect = (ParfumeBonusEffect)kart.GetBonusMgr().GetBonusEffectMgr().GetBonusEffect(EBonusEffect.BONUSEFFECT_ATTRACTED);
					if (!parfumeBonusEffect.Activated || parfumeBonusEffect.StinkParfume)
						((NapBonusEffect)kart.GetBonusMgr().GetBonusEffectMgr().GetBonusEffect(EBonusEffect.BONUSEFFECT_SLEPT)).Launcher = _Kart;
						kart.GetBonusMgr().GetBonusEffectMgr().ActivateBonusEffect(EBonusEffect.BONUSEFFECT_SLEPT);
			return;
			return;
				(this.ActiveLastUsed(_Item, _Kart) as MagicBonusEntity).Launch();
				return;
			if (Network.isServer)
				(this.ActiveLastUsed(_Item, _Kart) as MagicBonusEntity).NetLaunch(_Kart.networkViewID);
				return;
		default:
			return;
		{
			BonusEntity[] array = this.m_pPieEntities;
			BonusEntity[] array2 = array;
			_Tab = array2;
		}
		{
			BonusEntity[] array = this.m_pAutolockPieEntities;
			BonusEntity[] array3 = array;
			_Tab = array3;
		}
		{
			BonusEntity[] array = this.m_pSpringEntities;
			BonusEntity[] array4 = array;
			_Tab = array4;
		}
		{
			BonusEntity[] array = this.m_pDiamondEntities;
			BonusEntity[] array5 = array;
			_Tab = array5;
		}
		{
			BonusEntity[] array = this.m_pMagicEntities;
			BonusEntity[] array6 = array;
			_Tab = array6;
		}
	private UnityEngine.Object GenerateItem(EITEM _Item)
	{
		string path = string.Empty;
		switch (_Item)
		{
		case EITEM.ITEM_PIE:
			path = "Bonus/PieBonusEntity";
			break;
		case EITEM.ITEM_AUTOLOCK_PIE:
			path = "Bonus/AutolockPieBonusEntity";
			break;
		case EITEM.ITEM_SPRING:
			path = "Bonus/SpringBonusEntity";
			break;
		case EITEM.ITEM_DIAMOND:
			path = "Bonus/DiamondBonusEntity";
			break;
		case EITEM.ITEM_UFO:
			path = "Bonus/UfoBonusEntity";
			break;
		case EITEM.ITEM_MAGIC:
			path = "Bonus/MagicBonusEntity";
			break;
		}
		UnityEngine.Object @object = Resources.Load(path);
		UnityEngine.Object result = null;
		if (Network.isServer)
		{
			result = Network.Instantiate(@object, Vector3.zero, Quaternion.identity, 0);
		}
		else if (Network.peerType == NetworkPeerType.Disconnected)
		{
			result = UnityEngine.Object.Instantiate(@object);
		}
		return result;
	}

