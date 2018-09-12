		this.Load(out this._coins, out this._collectedCoins, out this._comicStrips, out this._puzzlePieces, out this._timeTrialRecords, out this._hats, out this._customs, out this._championShipsRecords, out this._characters, out this._karts, out this._championsShips, out this._timeTrialInfos, out this._advantagesQuantity, out this._advantages, out this._timeTrialMedals, out this._timeTrialBestTimes, out this._challenge, out this._pseudo, out this._gameSave, out this._playerConfig, out this._showTuto, out this._firstTime, out this._askRating, out this._askSharing, out this._matchmakingServer);
		return Network.isClient || Network.isServer || this._puzzlePieces["pp_" + pPiece];
		if (Network.isClient || Network.isServer)
		{
			return E_UnlockableItemSate.Unlocked;
		}
		if (Network.isClient || Network.isServer)
		{
			return E_UnlockableItemSate.Unlocked;
		}
		if (Network.isClient || Network.isServer)
		{
			return E_UnlockableItemSate.Unlocked;
		}
		if (Network.isClient || Network.isServer)
		{
			return E_UnlockableItemSate.Unlocked;
		}
				return;
			if (pDifficulty == EDifficulty.NORMAL && !Singleton<GameConfigurator>.Instance.PlayerConfig.HasNormalChampionShipStar)
				return;
			if (pDifficulty == EDifficulty.HARD && !Singleton<GameConfigurator>.Instance.PlayerConfig.HasHardChampionShipStar)
		if (Network.isClient || Network.isServer)
		{
			return E_UnlockableItemSate.Unlocked;
		}
		Dictionary<string, int> advantagesQuantity = this._advantagesQuantity;
		string key;
		int num = advantagesQuantity[key = text];
		advantagesQuantity[key] = num + pQuantity;
	public void Load(out int opCoins, out int opCollectedCoins, out Dictionary<string, E_UnlockableItemSate> opComicStrips, out Dictionary<string, bool> opPuzzlePieces, out Dictionary<string, int> opTimeTrialRecords, out Dictionary<string, E_UnlockableItemSate> opHats, out Dictionary<string, E_UnlockableItemSate> opCustoms, out Dictionary<string, int> opChampionShipsRecords, out Dictionary<string, E_UnlockableItemSate> opCharacters, out Dictionary<string, E_UnlockableItemSate> opKarts, out Dictionary<string, E_UnlockableItemSate> opChampionShips, out Dictionary<string, string> opTimeTrialInfos, out Dictionary<string, int> opAdvantagesQuantity, out Dictionary<string, E_UnlockableItemSate> opAdvantages, out Dictionary<string, E_TimeTrialMedal> opMedals, out Dictionary<string, int> opBestTimes, out string opChallenge, out string opPseudo, out GameSave opGameSave, out string opPlayerConfig, out bool opShowTuto, out bool opFirstTime, out int opAskRating, out bool opAskSharing, out string opMatchmakingServer)
	{
		opCoins = 0;
		opCollectedCoins = 0;
		opComicStrips = new Dictionary<string, E_UnlockableItemSate>();
		opPuzzlePieces = new Dictionary<string, bool>();
		opTimeTrialRecords = new Dictionary<string, int>();
		opHats = new Dictionary<string, E_UnlockableItemSate>();
		opCustoms = new Dictionary<string, E_UnlockableItemSate>();
		opChampionShipsRecords = new Dictionary<string, int>();
		opCharacters = new Dictionary<string, E_UnlockableItemSate>();
		opKarts = new Dictionary<string, E_UnlockableItemSate>();
		opChampionShips = new Dictionary<string, E_UnlockableItemSate>();
		opTimeTrialInfos = new Dictionary<string, string>();
		opAdvantagesQuantity = new Dictionary<string, int>();
		opAdvantages = new Dictionary<string, E_UnlockableItemSate>();
		opMedals = new Dictionary<string, E_TimeTrialMedal>();
		opBestTimes = new Dictionary<string, int>();
		opChallenge = string.Empty;
		opGameSave = GameSave.Load("progession");
		opPseudo = string.Empty;
		opPseudo = opGameSave.GetString("PSEUDO", string.Empty);
		string text = string.Format("{0};{1};{2};{3}", new object[]
		{
			ECharacter.GARFIELD,
			ECharacter.HARRY,
			"None",
			"None"
		});
		opMatchmakingServer = opGameSave.GetString("MATCHMAKINGSERVER", MenuOptionInput.defaultMatchmakingServer);
		opPlayerConfig = text;
		opPlayerConfig = opGameSave.GetString("CONFIG", text);
		opShowTuto = opGameSave.GetBool("SHOWTUTO", true);
		opFirstTime = opGameSave.GetBool("FIRSTTIME", true);
		opAskRating = opGameSave.GetInt("ASKRATING", 0);
		opAskSharing = opGameSave.GetBool("ASKSHARING", true);
		opCoins = opGameSave.GetInt("coins", 400);
		opCollectedCoins = opGameSave.GetInt("col_coins", 0);
		foreach (string str in ((TrackList)Resources.Load("Tracks", typeof(TrackList))).Tracks)
		{
			string text2 = "ct_" + str;
			int @int = opGameSave.GetInt(text2, 0);
			if (!opComicStrips.ContainsKey(text2))
			{
				opComicStrips.Add(text2, (E_UnlockableItemSate)@int);
			}
			for (int j = 0; j < 3; j++)
			{
				string text3 = "pp_" + str + "_" + j.ToString();
				bool @bool = opGameSave.GetBool(text3, false);
				if (!opPuzzlePieces.ContainsKey(text3))
				{
					opPuzzlePieces.Add(text3, @bool);
				}
			}
			string text4 = "tt_" + str;
			int int2 = opGameSave.GetInt(text4, -1);
			if (!opTimeTrialRecords.ContainsKey(text4))
			{
				opTimeTrialRecords.Add(text4, int2);
			}
			string text5 = text4.Replace("tt_", "tf_");
			string pDefaultValue = string.Format("{0};{1};{2};{3}", new object[]
			{
				ECharacter.NONE,
				ECharacter.NONE,
				"None",
				"None"
			});
			string @string = opGameSave.GetString(text5, pDefaultValue);
			if (!opTimeTrialInfos.ContainsKey(text5))
			{
				opTimeTrialInfos.Add(text5, @string);
			}
			string text6 = text4.Replace("tt_", "tb_");
			int int3 = opGameSave.GetInt(text6, -1);
			if (!opBestTimes.ContainsKey(text6))
			{
				opBestTimes.Add(text6, int3);
			}
			string text7 = text4.Replace("tt_", "tm_");
			int int4 = opGameSave.GetInt(text7, 0);
			opMedals.Add(text7, (E_TimeTrialMedal)int4);
		}
		foreach (UnityEngine.Object @object in Resources.LoadAll("Hat", typeof(BonusCustom)))
		{
			string text8 = "ht_" + @object.name;
			int int5 = opGameSave.GetInt(text8, (int)((BonusCustom)@object).State);
			if (!opHats.ContainsKey(text8))
			{
				opHats.Add(text8, (E_UnlockableItemSate)int5);
			}
		}
		foreach (UnityEngine.Object object2 in Resources.LoadAll("Kart", typeof(KartCustom)))
		{
			string text9 = "cm_" + object2.name;
			int int6 = opGameSave.GetInt(text9, (int)((KartCustom)object2).State);
			if (!opCustoms.ContainsKey(text9))
			{
				opCustoms.Add(text9, (E_UnlockableItemSate)int6);
			}
		}
		UnityEngine.Object[] array2 = Resources.LoadAll("ChampionShip", typeof(ChampionShipData));
		string name = array2[0].name;
		foreach (UnityEngine.Object object3 in array2)
		{
			string str2 = "cr_" + object3.name;
			string text10 = str2 + "_Easy";
			int int7 = opGameSave.GetInt(text10, -1);
			if (!opChampionShipsRecords.ContainsKey(text10))
			{
				opChampionShipsRecords.Add(text10, int7);
			}
			string text11 = str2 + "_Normal";
			int int8 = opGameSave.GetInt(text11, -1);
			if (!opChampionShipsRecords.ContainsKey(text11))
			{
				opChampionShipsRecords.Add(text11, int8);
			}
			string text12 = str2 + "_Hard";
			int int9 = opGameSave.GetInt(text12, -1);
			if (!opChampionShipsRecords.ContainsKey(text12))
			{
				opChampionShipsRecords.Add(text12, int9);
			}
			string str3 = "cs_" + object3.name;
			string text13 = str3 + "_Easy";
			int int10 = opGameSave.GetInt(text13, (int)((ChampionShipData)object3).EasyState);
			if (!opChampionShips.ContainsKey(text13))
			{
				opChampionShips.Add(text13, (E_UnlockableItemSate)int10);
			}
			string text14 = str3 + "_Normal";
			int int11 = opGameSave.GetInt(text14, (int)((ChampionShipData)object3).NormalState);
			if (!opChampionShips.ContainsKey(text14))
			{
				opChampionShips.Add(text14, (E_UnlockableItemSate)int11);
			}
			string text15 = str3 + "_Hard";
			int int12 = opGameSave.GetInt(text15, (int)((ChampionShipData)object3).HardState);
			if (!opChampionShips.ContainsKey(text15))
			{
				opChampionShips.Add(text15, (E_UnlockableItemSate)int12);
			}
		}
		UnityEngine.Object[] array3 = Resources.LoadAll("Character", typeof(CharacterCarac));
		for (int k = 0; k < array3.Length; k++)
		{
			if (array3[k] is CharacterCarac)
			{
				CharacterCarac characterCarac = (CharacterCarac)array3[k];
				string str4 = characterCarac.Owner.ToString();
				string text16 = "ch_" + str4;
				int int13 = opGameSave.GetInt(text16, (int)characterCarac.State);
				if (!opCharacters.ContainsKey(text16))
				{
					opCharacters.Add(text16, (E_UnlockableItemSate)int13);
				}
			}
		}
		UnityEngine.Object[] array4 = Resources.LoadAll("Kart", typeof(KartCarac));
		for (int l = 0; l < array4.Length; l++)
		{
			if (array4[l] is KartCarac)
			{
				KartCarac kartCarac = (KartCarac)array4[l];
				string str5 = kartCarac.Owner.ToString();
				string text17 = "kt_" + str5;
				int int14 = opGameSave.GetInt(text17, (int)kartCarac.State);
				if (!opKarts.ContainsKey(text17))
				{
					opKarts.Add(text17, (E_UnlockableItemSate)int14);
				}
			}
		}
		UnityEngine.Object[] array5 = Resources.LoadAll("Advantages", typeof(AdvantageData));
		for (int m = 0; m < array5.Length; m++)
		{
			if (array5[m] is AdvantageData)
			{
				AdvantageData advantageData = (AdvantageData)array5[m];
				string str6 = advantageData.AdvantageType.ToString();
				string text18 = "av_" + str6;
				int int15 = opGameSave.GetInt(text18, (int)advantageData.State);
				if (!opAdvantages.ContainsKey(text18))
				{
					opAdvantages.Add(text18, (E_UnlockableItemSate)int15);
				}
				string text19 = "aq_" + str6;
				int pDefaultValue2 = 0;
				if (advantageData.State != E_UnlockableItemSate.Hidden)
				{
					pDefaultValue2 = 2;
				}
				int int16 = opGameSave.GetInt(text19, pDefaultValue2);
				if (!opAdvantagesQuantity.ContainsKey(text19))
				{
					opAdvantagesQuantity.Add(text19, int16);
				}
			}
		}
		string format = "{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16}";
		object[] array6 = new object[17];
		int num = 0;
		DateTime dateTime = new DateTime(2000, 1, 1);
		array6[num] = dateTime.ToString("ddMMyyyy");
		array6[1] = E_GameModeType.SINGLE.ToString();
		array6[2] = EChallengeFirstObjective.FinishAtPosX.ToString();
		array6[3] = ECharacter.NONE.ToString();
		array6[4] = EChallengeSingleRaceObjective.EarnXCoins.ToString();
		array6[5] = EChallengeChampionshipObjective.EarnXCoins.ToString();
		array6[6] = E_TimeTrialMedal.None.ToString();
		array6[7] = ECharacter.NONE.ToString();
		array6[8] = ECharacter.NONE.ToString();
		array6[9] = name;
		array6[10] = 0.ToString();
		array6[11] = EChallengeState.NotPlayed;
		array6[12] = true.ToString();
		array6[13] = EDifficulty.NORMAL;
		array6[14] = string.Empty;
		array6[15] = E_RewardType.Custom;
		array6[16] = ERarity.Base;
		string pDefaultValue3 = string.Format(format, array6);
		opChallenge = opGameSave.GetString("chal", pDefaultValue3);
	}

	public void SetMatchmakingServer(string server, bool save)
	{
		this._matchmakingServer = server;
		this._gameSave.SetString("MATCHMAKINGSERVER", server);
		if (save)
		{
			this.Save();
		}
	}

	public string GetMatchmakingServer()
	{
		return this._matchmakingServer;
	}


	private string _matchmakingServer;
