		this.m_fCurrentSafeCounter = 0f;
					this.m_fCurrentSafeCounter += deltaTime;
					if (this.m_fCurrentSafeCounter >= this.m_fSafeCounter)
					{
						flag2 = true;
					}
	private float m_fSafeCounter = 6f;

	private float m_fCurrentSafeCounter;

