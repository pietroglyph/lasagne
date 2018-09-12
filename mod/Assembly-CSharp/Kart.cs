		if (Singleton<GameManager>.Instance.Modifiers.JumpyBots && UnityEngine.Random.Range(0f, 1f) > 0.995f && Network.isServer && base.GetControlType() == RcVehicle.ControlType.AI && base.IsOnGround())
		{
			float num2 = this.tmpJumpForward;
			float num3 = this.tmpJumpHeight;
			this.Jump(UnityEngine.Random.Range(1f, 3.5f), -3f);
			this.tmpJumpForward = num2;
			this.tmpJumpHeight = num3;
		}
