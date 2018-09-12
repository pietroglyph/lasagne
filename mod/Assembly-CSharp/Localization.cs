			if (string.IsNullOrEmpty(this.mLanguage) && this.languages != null && this.languages.Length != 0)
				return;
		string result;
		if (this.mDictionary.TryGetValue(key, out result))
		{
			return result;
		}
		return key;
		if (Localization.instance != null)
		{
			return Localization.instance.Get(key);
		}
		return key;
