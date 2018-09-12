		this.m_bAccelerometer = false;
		else if (SystemInfo.supportsAccelerometer)
		{
			this.m_bAccelerometer = true;
		}
		else if (Singleton<GameOptionManager>.Instance.GetInputType() == E_InputType.Gyroscopic && this.m_bAccelerometer)
		{
			float num5 = 2f * Mathf.Max(0.3f, Singleton<GameOptionManager>.Instance.GetGyroSensibility());
			num4 = Input.acceleration.x * num5;
		}
	protected bool m_bAccelerometer;

