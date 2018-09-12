using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
		string text = string.Empty;
		if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
		{
			string text2 = Application.dataPath + "/../External/Language.csv";
			if (!File.Exists(text2))
			{
				Debug.LogWarning(text2 + " not exist");
			}
			string[] array = File.ReadAllText(text2).Split(new char[]
			{
				';'
			});
			if ((array.Length == 1 && array[0] != string.Empty) || (array.Length == 2 && array[1] == string.Empty && array[0] != string.Empty))
			{
				string[] array2 = GameOptionManager.SplitCsvLine(array[0]);
				text = array2[0];
			}
		}
		if (text == string.Empty)
		{
			this.m_oLanguageList = Array.ConvertAll<UnityEngine.Object, TextAsset>(Resources.LoadAll("Localization", typeof(TextAsset)), (UnityEngine.Object o) => (TextAsset)o);
		}
		else
		{
			this.m_oLanguageList = new TextAsset[1];
			TextAsset textAsset = Resources.Load("Localization/" + text, typeof(TextAsset)) as TextAsset;
			if (textAsset != null)
			{
				this.m_oLanguageList[0] = textAsset;
			}
			else
			{
				this.m_oLanguageList[0] = (Resources.Load("Localization/Lang_DB_UK", typeof(TextAsset)) as TextAsset);
			}
		}
		Localization.instance.languages = this.m_oLanguageList;
		string gameLanguage = this.GetGameLanguage(eLangId);
		string @string = opGameSave.GetString("lang_", gameLanguage);
		string gameLanguage = this.GetGameLanguage(eLanguage);
		this._language = this.ConvertLangStringToId(gameLanguage);
		Localization.instance.currentLanguage = gameLanguage;
		this._gameSave.SetString("lang_", gameLanguage);
		if ((ulong)num >= (ulong)((long)GameOptionManager.m_oLanguageCodeList.Length))
		return GameOptionManager.m_oLanguageCodeList[(int)((UIntPtr)num)];
		foreach (string a in GameOptionManager.m_oLanguageCodeList)
	public int GetLanguagesNumber()
	{
		return this.m_oLanguageList.Length;
	}

	public string GetGameLanguage(GameOptionManager.ELangID eLangId)
	{
		string text = this.ConvertLangIdToString(eLangId);
		for (int i = 0; i < this.m_oLanguageList.Length; i++)
		{
			if (text == this.m_oLanguageList[i].name)
			{
				return text;
			}
		}
		if (this.m_oLanguageList.Length > 0)
		{
			return this.m_oLanguageList[0].name;
		}
		return string.Empty;
	}

	public static string[] SplitCsvLine(string line)
	{
		return (from Match m in Regex.Matches(line, "(((?<x>(?=[,\\r\\n]+))|\"(?<x>([^\"]|\"\")+)\"|(?<x>[^,\\r\\n]+)),?)", RegexOptions.ExplicitCapture)
		select m.Groups[1].Value).ToArray<string>();
	}

	private TextAsset[] m_oLanguageList;

	private static string[] m_oLanguageCodeList = new string[]
