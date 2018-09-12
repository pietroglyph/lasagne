using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class ASE_Flurry
{
	[DllImport("__Internal")]
	private static extern void ASE_FlurryStartSession(string sApiKey);

	public static void Init(string sApiKey)
	{
		if (ASE_Tools.Available)
		{
		}
	}

	public static void Stop()
	{
		if (ASE_Tools.Available)
		{
		}
	}

	[DllImport("__Internal")]
	private static extern void ASE_FlurryLogEvent(string sEventName);

	public static void LogEvent(string sEventName)
	{
		if (ASE_Tools.Available)
		{
		}
	}

	[DllImport("__Internal")]
	private static extern void ASE_FlurryLogEventWithParameters(string sEventName, string[] sKeys, string[] sValues);

	public static void LogEvent(string sEventName, Dictionary<string, string> dict)
	{
		if (ASE_Tools.Available)
		{
			string[] array;
			string[] array2;
			ASE_Tools.DictionaryToArrays(dict, out array, out array2);
		}
	}

	public static void LogEvent(string sEventName, string[] sKeys, string[] sValues)
	{
		if (ASE_Tools.Available)
		{
		}
	}

	[DllImport("__Internal")]
	private static extern void ASE_FlurryLogEventTimed(string sEventName, bool bTimed);

	public static void LogEvent(string sEventName, bool bTimed)
	{
		if (ASE_Tools.Available)
		{
		}
	}

	[DllImport("__Internal")]
	private static extern void ASE_FlurryLogEventTimedWithParameters(string sEventName, bool bTimed, string[] sKeys, string[] sValues);

	public static void LogEvent(string sEventName, bool bTimed, Dictionary<string, string> dict)
	{
		if (ASE_Tools.Available)
		{
			string[] array;
			string[] array2;
			ASE_Tools.DictionaryToArrays(dict, out array, out array2);
		}
	}

	public static void LogEvent(string sEventName, bool bTimed, string[] sKeys, string[] sValues)
	{
		if (ASE_Tools.Available)
		{
		}
	}

	[DllImport("__Internal")]
	private static extern void ASE_FlurryEndTimedEvent(string sEventName);

	public static void EndTimedEvent(string sEventName)
	{
		if (ASE_Tools.Available)
		{
		}
	}

	[DllImport("__Internal")]
	private static extern void ASE_FlurryEndTimedEventWithParameters(string sEventName, string[] sKeys, string[] sValues);

	public static void EndTimedEvent(string sEventName, Dictionary<string, string> dict)
	{
		if (ASE_Tools.Available)
		{
			string[] array;
			string[] array2;
			ASE_Tools.DictionaryToArrays(dict, out array, out array2);
		}
	}

	public static void EndTimedEvent(string sEventName, string[] sKeys, string[] sValues)
	{
		if (ASE_Tools.Available)
		{
		}
	}

	[DllImport("__Internal")]
	private static extern void ASE_FlurrySetEventLoggingEnabled(bool bValue);

	public static void SetEventLoggingEnabled(bool bValue)
	{
		if (ASE_Tools.Available)
		{
		}
	}

	[DllImport("__Internal")]
	private static extern void ASE_FlurrySetLogLevel(int nLevel);

	public static void SetLogLevel(int nLevel)
	{
		if (ASE_Tools.Available)
		{
		}
	}

	[DllImport("__Internal")]
	private static extern void ASE_FlurrySetGender(int nGender);

	public static void SetGender(ASE_Flurry.Gender gender)
	{
		if (ASE_Tools.Available)
		{
		}
	}

	[DllImport("__Internal")]
	private static extern void ASE_FlurrySetAge(int nAge);

	public static void SetAge(int nAge)
	{
		if (ASE_Tools.Available)
		{
		}
	}

	[DllImport("__Internal")]
	private static extern void ASE_FlurrySetUserID(string sUserID);

	public static void SetUserID(string sUserID)
	{
		if (ASE_Tools.Available)
		{
		}
	}

	public enum Gender
	{
		UNKNOWN = -1,
		FEMALE,
		MALE
	}
}
