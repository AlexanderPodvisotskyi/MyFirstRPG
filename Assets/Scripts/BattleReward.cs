using UnityEngine;
using UnityEngine.UI;

public class BattleReward : MonoBehaviour
{
	public Text ExpText;
	public Text itemText;

	public GameObject rewardScreen;

	public string[] rewardItems;
	public int expEarned;

	public bool markQuestComplete;
	public string questToMark;

	public static BattleReward instance;
	// Start is called before the first frame update
	void Start()
	{
		instance = this;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Y))
		{
		}

	}

	public void OpenRewardScreen(int exp, string[] rewards)
	{
		expEarned = exp;
		rewardItems = rewards;

		ExpText.text = "Team earned " + expEarned + " xp!";
		itemText.text = "";

		for (int i = 0; i < rewardItems.Length; i++)
		{
			itemText.text += rewards[i] + "\n";
		}

		rewardScreen.SetActive(true);
	}

	public void CloseRewardScreen()
	{
		for (int i = 0; i < GameManager.instense.playerStats.Length; i++)
		{
			if (GameManager.instense.playerStats[i].gameObject.activeInHierarchy)
			{
				GameManager.instense.playerStats[i].AddExp(expEarned);
			}
		}

		for (int i = 0; i < rewardItems.Length; i++)
		{
			GameManager.instense.AddItem(rewardItems[i]);
		}

		rewardScreen.SetActive(false);

		GameManager.instense.battleActive = false;

		if (markQuestComplete)
		{
			QuestManager.instance.MarkQuestComplete(questToMark);
		}
	}
}
