using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
	public GameObject UIScreen;
	public GameObject Player;
	public GameObject gameManager;
	public GameObject audioManager;
	public GameObject battleManager;

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
			PlayerController.instense.joystick = UIScreen.GetComponent<JoystickHelper>().joystick;
		}

		if (GameManager.instense == null)
		{
			GameManager.instense = Instantiate(gameManager).GetComponent<GameManager>();
		}

		//if (AudioManager.instance == null)
		//{
		//	AudioManager.instance = Instantiate(audioManager).GetComponent<AudioManager>();
		//}

		//if (BattleManager.instance == null)
		//{
		//	BattleManager.instance = Instantiate(battleManager).GetComponent<BattleManager>();
		//}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
