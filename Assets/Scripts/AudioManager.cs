using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public AudioSource[] soundEffect;
	public AudioSource[] backgroundMusic;

	public static AudioManager instance;
	// Start is called before the first frame update
	void Start()
	{
		instance = this;

		DontDestroyOnLoad(this.gameObject);
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void PlaySoundEffects(int soundToPlay)
	{
		if (soundToPlay < soundEffect.Length)
			soundEffect[soundToPlay].Play();
	}

	public void PlayBackgroundMusic(int musicToPlay)
	{
		if (!backgroundMusic[musicToPlay].isPlaying)
		{
			StopMusic();

			if (musicToPlay < backgroundMusic.Length)
				backgroundMusic[musicToPlay].Play();
		}
	}

	public void StopMusic()
	{
		for (int i = 0; i < backgroundMusic.Length; i++)
		{
			backgroundMusic[i].Stop();
		}
	}
}
