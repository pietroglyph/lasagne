		foreach (UnityEngine.Object @object in Resources.LoadAll("Reward", typeof(RewardBase)))
			return;
		if (pRewardType == E_RewardType.Hat)
			return;
		if (num2 >= availableHats.Count)
		if (Singleton<RandomManager>.Instance.Next(0, 99) < 65)
