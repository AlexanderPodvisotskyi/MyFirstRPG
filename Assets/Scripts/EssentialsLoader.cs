using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
	public GameObject UIScreen;
	public GameObject Player;
	public GameObject gameManager;
	public GameObject audioManager;

	// Start is called before the first frame update
	void Start()
	{
		if (UIFade.instance == null)
		{
			UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
		}

		if (PlayerController.instense == null)
		{
			PlayerController clone = Instantiate(Player).GetComponent<PlayerController>();
			PlayerController.instense = clone;
		}

		if (GameManager.instense == null)
		{
			Instantiate(gameManager);
		}

		if (AudioManager.instance == null)
		{
			Instantiate(audioManager);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
