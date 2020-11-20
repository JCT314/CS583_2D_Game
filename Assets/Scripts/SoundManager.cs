using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager _instance { get; private set; }
	public AudioClip jumpSound, buttonClickSound, hitSound, backgroundMusic, gameOverMusic, gameWinMusic, itemCollect;
	public AudioSource[] audioSources;

	// Use this for initialization
	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(this);
			jumpSound = Resources.Load<AudioClip>("jump");
			buttonClickSound = Resources.Load<AudioClip>("buttonClick");
			hitSound = Resources.Load<AudioClip>("hit");
			backgroundMusic = Resources.Load<AudioClip>("backgroundMusic");
			gameOverMusic = Resources.Load<AudioClip>("gameOverMusic");
			gameWinMusic = Resources.Load<AudioClip>("gameWinMusic");
			itemCollect = Resources.Load<AudioClip>("itemCollect");
			audioSources = GetComponents<AudioSource>();
			audioSources[0].clip = backgroundMusic;
			audioSources[0].loop = true;
			audioSources[0].Play();
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void PlaySound(string clip)
	{
		switch (clip)
		{
            case "jump":
                audioSources[1].PlayOneShot(jumpSound);
                break;
            case "buttonClick":
				audioSources[1].PlayOneShot(buttonClickSound, 5f);
                break;
            case "hit":
				audioSources[1].PlayOneShot(hitSound);
                break;
			case "itemCollect":
				audioSources[1].PlayOneShot(itemCollect,10f);
				break;
			case "gameOverMusic":
				audioSources[0].loop = false;
				audioSources[0].clip = gameOverMusic;
				audioSources[0].Play();
				break;
			case "backGroundMusic":
				audioSources[0].loop = true;
				audioSources[0].clip = backgroundMusic;
				audioSources[0].Play();
				break;
			case "gameWinMusic":
				audioSources[0].loop = false;
				audioSources[0].clip = gameWinMusic;
				audioSources[0].Play();
				break;
		}
	}
}
