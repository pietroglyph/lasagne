using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
	private void Awake()
	{
		if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
		{
			List<FileInfo> list = new List<FileInfo>();
			list.AddRange(new DirectoryInfo(Application.dataPath + "/../External/").GetFiles("*.png"));
			list.AddRange(new DirectoryInfo(Application.dataPath + "/../PatchTest/").GetFiles("*.png"));
			foreach (FileInfo fileInfo in list)
			{
				WWW www = new WWW("file://" + fileInfo.FullName);
				while (!www.isDone)
				{
				}
				Texture2D texture2D = new Texture2D(1024, 768, TextureFormat.ARGB32, false);
				www.LoadImageIntoTexture(texture2D);
				this.m_vTextures.Add(texture2D);
			}
			this.m_oTweenColor = this.Texture.GetComponent<TweenColor>();
			this.m_oTexture = this.Texture.GetComponent<UITexture>();
		}
	}

	private void OnDestroy()
	{
		if (this.m_oTexture)
		{
			this.m_oTexture.mainTexture = null;
		}
		foreach (Texture2D obj in this.m_vTextures)
		{
			UnityEngine.Object.Destroy(obj);
		}
	}

	private void Update()
	{
		if (this.m_vTextures == null || this.m_vTextures.Count == 0)
		{
			Application.LoadLevel("RootScene");
			return;
		}
		if (this.m_iIndex == -1)
		{
			this.m_bFadeIn = true;
			this.m_iIndex = 0;
			this.Texture.SetActive(true);
			this.m_oTexture.mainTexture = this.m_vTextures[this.m_iIndex];
			this.m_oTexture.MakePixelPerfect();
			this.m_oTweenColor.enabled = true;
			this.m_oTweenColor.duration = this.FadeTimer;
			this.m_oTweenColor.Play(true);
		}
		else
		{
			base.GetComponentInChildren<Camera>().backgroundColor = this.m_vTextures[this.m_iIndex].GetPixel(0, 0);
		}
		if (this.m_fTimer >= 0f)
		{
			this.m_fTimer += Time.deltaTime;
			if (this.m_fTimer >= this.SplashTimer)
			{
				this.m_bFadeIn = false;
				this.m_oTweenColor.duration = this.FadeTimer - (this.m_fTimer - this.SplashTimer);
				this.m_fTimer = -1f;
				this.m_oTweenColor.Play(false);
			}
		}
	}

	public void DoSplashFinished()
	{
		if (this.m_iIndex == -1)
		{
			return;
		}
		if (this.m_bFadeIn)
		{
			this.m_fTimer = 0f;
			return;
		}
		this.Texture.SetActive(false);
		if (this.m_iIndex < this.m_vTextures.Count - 1)
		{
			this.m_iIndex++;
			this.Texture.SetActive(true);
			this.m_oTexture.mainTexture = this.m_vTextures[this.m_iIndex];
			this.m_oTexture.MakePixelPerfect();
			this.m_oTweenColor.enabled = true;
			this.m_oTweenColor.duration = this.FadeTimer;
			this.m_oTweenColor.Play(true);
			this.m_bFadeIn = true;
			return;
		}
		Application.LoadLevel("RootScene");
	}

	private void OnGUI()
	{
		if (!Debug.isDebugBuild)
		{
			return;
		}
		string str = File.GetLastWriteTime(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)).ToString("hh:mm tt on MM/dd/yyyy");
		GUI.Label(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), "Patched at " + str);
	}

	public float FadeTimer = 0.5f;

	public float SplashTimer = 1f;

	public GameObject Texture;

	private float m_fTimer = -1f;

	private bool m_bFadeIn;

	private int m_iIndex = -1;

	private TweenColor m_oTweenColor;

	private UITexture m_oTexture;

	private List<Texture2D> m_vTextures = new List<Texture2D>();
}
