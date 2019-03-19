using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
	public GameObject UIScreen;
	public GameObject Player;

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
	}

	// Update is called once per frame
	void Update()
	{

	}
}
