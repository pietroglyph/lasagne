				UnityEngine.Debug.LogWarning("Resources.Load('DebugSettings.asset') returns null");
			UnityEngine.Debug.Log(DebugMgr.GetHeader(frame, cat) + message + "\n\n--------------------\n");
			UnityEngine.Debug.LogWarning(DebugMgr.GetHeader(frame, cat) + message + "\n\n--------------------\n");
		UnityEngine.Debug.LogError(DebugMgr.GetHeader(2, EDbgCategory.ERROR) + message + "\n\n--------------------\n");
