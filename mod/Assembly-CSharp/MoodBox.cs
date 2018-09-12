	public void Start()
	public void OnDrawGizmos()
	public void OnDrawGizmosSelected()
	public void OnTriggerEnter(Collider other)
		if (this.manager)
		{
			this.manager.CurrentMoodBox = this;
		}
