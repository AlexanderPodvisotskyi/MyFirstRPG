using System.Collections;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{

	public BattleType[] potentialBattles;

	public bool activateOnEnter;
	public bool activateOnStay;
	public bool activateOnExit;

	public float timeBetweenBattles = 10;


	private float betweenBattleCounter;
	private bool inArea;
	public bool deactivateAfterStarting;

	public bool shouldCompleteQuest;
	public string QuestToComplete;

	// Start is called before the first frame update
	void Start()
	{
		betweenBattleCounter = Random.Range(timeBetweenBattles * .5f, timeBetweenBattles * 1.5f);
	}

	// Update is called once per frame
	void Update()
	{
		if (inArea && PlayerController.instense.canMove)
		{
			if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
			{
				betweenBattleCounter -= Time.deltaTime;
			}

			if (betweenBattleCounter <= 0)
			{
				betweenBattleCounter = Random.Range(timeBetweenBattles * .5f, timeBetweenBattles * 1.5f);

				StartCoroutine(StartBattleCo());
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			if (activateOnEnter)
			{
				StartCoroutine(StartBattleCo());
			}
			else
			{
				inArea = true;
			}

		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			if (activateOnExit)
			{

			}
			else
			{
				inArea = false;
			}
		}
	}

	public IEnumerator StartBattleCo()
	{
		UIFade.instance.FadeToBlack();
		GameManager.instense.battleActive = true;

		int selectedBattle = Random.Range(0, potentialBattles.Length);

		BattleManager.instance.rewardItems = potentialBattles[selectedBattle].rewardItems;
		BattleManager.instance.rewardXP = potentialBattles[selectedBattle].rewardXP;

		yield return new WaitForSeconds(1.5f);

		BattleManager.instance.BattleStart(potentialBattles[selectedBattle].enemies);

		UIFade.instance.FadeFromBlack();

		if (deactivateAfterStarting)
		{
			gameObject.SetActive(false);
		}

		BattleReward.instance.markQuestComplete = shouldCompleteQuest;
		BattleReward.instance.questToMark = QuestToComplete;
	}
}
