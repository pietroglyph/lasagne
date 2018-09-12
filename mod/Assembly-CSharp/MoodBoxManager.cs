	public MoodBox CurrentMoodBox
		get
			return this.currentMoodBox;
		}
		set
		{
			this.currentMoodBox = value;
			this.UpdateFromMoodBox();
	public void Start()
		this.CurrentMoodBox = this.startMoodBox;
		if (this.CurrentCamera)
		{
			this.CurrentBloom = this.CurrentCamera.GetComponent<Bloom>();
			if (this.CurrentBloom)
			{
				this.CurrentBloom.bloomIntensity = this.IntensityBloom;
				this.CurrentBloom.bloomThreshhold = this.IntensityThreshlod;
			}
		}
		if (this.CurrentMoodBox)
				this.currentData.FogStart = this.CurrentMoodBox.data.FogStart;
				this.currentData.FogEnd = this.CurrentMoodBox.data.FogEnd;
				this.currentData.FogColor = this.CurrentMoodBox.data.FogColor;
				this.currentData.outside = this.CurrentMoodBox.data.outside;
				this.currentData.FogStart = Mathf.Lerp(this.currentData.FogStart, this.CurrentMoodBox.data.FogStart, deltaTime);
				this.currentData.FogEnd = Mathf.Lerp(this.currentData.FogEnd, this.CurrentMoodBox.data.FogEnd, deltaTime);
				this.currentData.FogColor = Color.Lerp(this.currentData.FogColor, this.CurrentMoodBox.data.FogColor, deltaTime);
				this.currentData.outside = this.CurrentMoodBox.data.outside;
