using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{

	public static BattleManager instance;

	private bool battleActive;

	public GameObject BattleScene;

	public Transform[] playerPositions;
	public Transform[] enemyPositions;

	public BattleCharacter[] playerPrefabs;
	public BattleCharacter[] enemyPrefabs;

	public List<BattleCharacter> activeBattlers = new List<BattleCharacter>();

	public int currentTurn;
	public bool turnWaiting;

	public GameObject uiButtonsHolder;

	public BattleMove[] movesList;

	public GameObject enemyAttackEffect;

	public DamageNumber theDamageNumber;

	public Text[] playerName;
	public Text[] playerHP;
	public Text[] playerMP;

	public GameObject targetMenu;

	public BattleTargetsButton[] targetButtons;

	public GameObject magicMenu;
	public BattleMagicSelect[] magicButtons;

	public BattleNotification battleNotice;

	public int chanceToFlee = 50;
	private bool fleeing;

	public string gameOverScene;

	public int rewardXP;
	public string[] rewardItems;

	// Start is called before the first frame update
	void Start()
	{
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			BattleStart(new string[] { "Goblin", "Spider" });
		}

		if (battleActive)
		{
			if (turnWaiting)
			{
				if (activeBattlers[currentTurn].isPlayer)
				{
					uiButtonsHolder.SetActive(true);
				}
				else
				{
					uiButtonsHolder.SetActive(false);

					StartCoroutine(EnemyMoveCo());
				}
			}

			if (Input.GetKeyDown(KeyCode.N))
			{
				NextTurn();
			}
		}
	}

	public void BattleStart(string[] enemiesToSpawn)
	{
		if (!battleActive)
		{
			battleActive = true;

			GameManager.instense.battleActive = true;

			transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
			BattleScene.SetActive(true);

			AudioManager.instance.PlayBackgroundMusic(0);

			for (int i = 0; i < playerPositions.Length; i++)
			{
				if (GameManager.instense.playerStats[i].gameObject.activeInHierarchy)
				{
					for (int j = 0; j < playerPrefabs.Length; j++)
					{
						if (playerPrefabs[j].characterName == GameManager.instense.playerStats[i].nameHero)
						{
							BattleCharacter newPlayer = Instantiate(playerPrefabs[j], playerPositions[i].position, playerPositions[i].rotation);
							newPlayer.transform.parent = playerPositions[i];

							activeBattlers.Add(newPlayer);

							CharStats thePlayer = GameManager.instense.playerStats[i];
							activeBattlers[i].currentHP = thePlayer.currentHP;
							activeBattlers[i].maxHP = thePlayer.maxHP;
							activeBattlers[i].currentMP = thePlayer.currentMP;
							activeBattlers[i].maxMP = thePlayer.maxMP;
							activeBattlers[i].strength = thePlayer.strength;
							activeBattlers[i].defence = thePlayer.defence;
							activeBattlers[i].weaponPower = thePlayer.weaponPower;
							activeBattlers[i].armorPower = thePlayer.armorPower;
						}
					}
				}
			}

			for (int i = 0; i < enemiesToSpawn.Length; i++)
			{
				if (enemiesToSpawn[i] != "")
				{
					for (int j = 0; j < enemyPrefabs.Length; j++)
					{
						if (enemyPrefabs[j].characterName == enemiesToSpawn[i])
						{
							BattleCharacter newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[i].position, enemyPositions[i].rotation);
							newEnemy.transform.parent = enemyPositions[i];
							activeBattlers.Add(newEnemy);
						}
					}
				}
			}
			turnWaiting = true;
			currentTurn = Random.Range(0, activeBattlers.Count);

			UpdateUIStats();
		}
	}

	public void NextTurn()
	{
		currentTurn++;
		if (currentTurn >= activeBattlers.Count)
		{
			currentTurn = 0;
		}

		turnWaiting = true;
		UpdateBattle();
		UpdateUIStats();
	}

	public void UpdateBattle()
	{
		bool allEnemiesDead = true;
		bool allPlayersDead = true;

		for (int i = 0; i < activeBattlers.Count; i++)
		{
			if (activeBattlers[i].currentHP < 0)
			{
				activeBattlers[i].currentHP = 0;
			}

			if (activeBattlers[i].currentHP == 0)
			{
				if (activeBattlers[i].isPlayer)
				{
					activeBattlers[i].theSprite.sprite = activeBattlers[i].deadSprite;
				}
				else
				{
					activeBattlers[i].EnemyFade();
				}
			}
			else
			{
				if (activeBattlers[i].isPlayer)
				{
					allPlayersDead = false;
					activeBattlers[i].theSprite.sprite = activeBattlers[i].aliveSprite;
				}
				else
				{
					allEnemiesDead = false;
				}
			}
		}

		if (allEnemiesDead || allPlayersDead)
		{
			if (allEnemiesDead)
			{
				// Victory battle
				StartCoroutine(EndBattleCo());
			}
			else
			{
				// Fail battle
				StartCoroutine(GameOverCo());
			}
		}
		else
		{
			while (activeBattlers[currentTurn].currentHP == 0)
			{
				currentTurn++;
				if (currentTurn >= activeBattlers.Count)
				{
					currentTurn = 0;
				}
			}
		}
	}

	public IEnumerator EnemyMoveCo()
	{
		turnWaiting = false;

		yield return new WaitForSeconds(1f);

		EnemyAttack();

		yield return new WaitForSeconds(1f);

		NextTurn();
	}

	public void EnemyAttack()
	{
		List<int> players = new List<int>();

		for (int i = 0; i < activeBattlers.Count; i++)
		{
			if (activeBattlers[i].isPlayer && activeBattlers[i].currentHP > 0)
			{
				players.Add(i);
			}
		}

		int selectedTarget = players[Random.Range(0, players.Count)];

		int selectAttack = Random.Range(0, activeBattlers[currentTurn].movesAvailable.Length);
		int movePower = 0;

		for (int i = 0; i < movesList.Length; i++)
		{
			if (movesList[i].moveName == activeBattlers[currentTurn].movesAvailable[selectAttack])
			{
				Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
				movePower = movesList[i].movePower;
			}
		}

		Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);

		DealDamage(selectedTarget, movePower);
	}

	public void DealDamage(int target, int movePower)
	{
		float attackPower = activeBattlers[currentTurn].strength + activeBattlers[currentTurn].weaponPower;
		float defencePower = activeBattlers[target].defence + activeBattlers[target].armorPower;

		float damageCalc = (attackPower / defencePower) * movePower * Random.Range(.9f, 1.1f);

		int damageToGive = Mathf.RoundToInt(damageCalc);

		Debug.Log(activeBattlers[currentTurn].characterName + " Is dealing " + damageCalc + "(" + damageToGive + ") damage to " + activeBattlers[target].characterName);

		activeBattlers[target].currentHP -= damageToGive;

		Instantiate(theDamageNumber, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damageToGive);

		UpdateUIStats();
	}

	public void UpdateUIStats()
	{
		for (int i = 0; i < playerName.Length; i++)
		{
			if (activeBattlers.Count > i)
			{
				if (activeBattlers[i].isPlayer)
				{
					BattleCharacter playerData = activeBattlers[i];

					playerName[i].gameObject.SetActive(true);

					playerName[i].text = playerData.characterName;
					playerHP[i].text = Mathf.Clamp(playerData.currentHP, 0, int.MaxValue) + "/" + playerData.maxHP;
					playerMP[i].text = Mathf.Clamp(playerData.currentMP, 0, int.MaxValue) + "/" + playerData.maxMP;
				}
				else
				{
					playerName[i].gameObject.SetActive(false);
				}
			}
			else
			{
				playerName[i].gameObject.SetActive(false);
			}
		}
	}

	public void PlayerAttack(string moveName, int selectedTarget)
	{

		int movePower = 0;

		for (int i = 0; i < movesList.Length; i++)
		{
			if (movesList[i].moveName == moveName)
			{
				Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
				movePower = movesList[i].movePower;
			}
		}

		Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);

		DealDamage(selectedTarget, movePower);

		uiButtonsHolder.SetActive(false);
		targetMenu.SetActive(false);

		NextTurn();
	}

	public void OpenTargetMenu(string moveName)
	{
		targetMenu.SetActive(true);

		List<int> Enemies = new List<int>();

		for (int i = 0; i < activeBattlers.Count; i++)
		{
			if (!activeBattlers[i].isPlayer)
			{
				Enemies.Add(i);
			}
		}

		for (int i = 0; i < targetButtons.Length; i++)
		{
			if (Enemies.Count > i && activeBattlers[Enemies[i]].currentHP > 0)
			{
				targetButtons[i].gameObject.SetActive(true);

				targetButtons[i].moveName = moveName;
				targetButtons[i].activeBattlerTarget = Enemies[i];
				targetButtons[i].targetName.text = activeBattlers[Enemies[i]].characterName;
			}
			else
			{
				targetButtons[i].gameObject.SetActive(false);
			}
		}
	}

	public void OpenMagicMenu()
	{
		magicMenu.SetActive(true);

		for (int i = 0; i < magicButtons.Length; i++)
		{
			if (activeBattlers[currentTurn].movesAvailable.Length > i)
			{
				magicButtons[i].gameObject.SetActive(true);

				magicButtons[i].spellName = activeBattlers[currentTurn].movesAvailable[i];
				magicButtons[i].nameText.text = magicButtons[i].spellName;

				for (int j = 0; j < movesList.Length; j++)
				{
					if (movesList[j].moveName == magicButtons[i].spellName)
					{
						magicButtons[i].spellCost = movesList[j].moveCost;
						magicButtons[i].costText.text = magicButtons[i].spellCost.ToString();
					}
				}
			}
			else
			{
				magicButtons[i].gameObject.SetActive(false);
			}
		}
	}

	public void Flee()
	{
		int fleeSuccess = Random.Range(0, 100);

		Debug.Log(fleeSuccess);

		if (fleeSuccess < chanceToFlee)
		{
			fleeing = true;
			StartCoroutine(EndBattleCo());
		}
		else
		{
			NextTurn();

			battleNotice.theText.text = "Not today, maybe next turn";
			battleNotice.Activate();
		}
	}

	public IEnumerator EndBattleCo()
	{
		battleActive = false;
		uiButtonsHolder.SetActive(false);
		targetMenu.SetActive(false);
		magicMenu.SetActive(false);

		yield return new WaitForSeconds(.5f);

		UIFade.instance.FadeToBlack();

		yield return new WaitForSeconds(1.5f);

		for (int i = 0; i < activeBattlers.Count; i++)
		{
			if (activeBattlers[i].isPlayer)
			{
				for (int j = 0; j < GameManager.instense.playerStats.Length; j++)
				{
					if (activeBattlers[i].characterName == GameManager.instense.playerStats[j].nameHero)
					{
						GameManager.instense.playerStats[j].currentHP = activeBattlers[i].currentHP;
						GameManager.instense.playerStats[j].currentMP = activeBattlers[i].currentMP;
					}
				}
			}
			Destroy(activeBattlers[i].gameObject);
		}

		UIFade.instance.FadeFromBlack();
		BattleScene.SetActive(false);

		activeBattlers.Clear();
		currentTurn = 0;

		//GameManager.instense.battleActive = false;
		if (fleeing)
		{
			GameManager.instense.battleActive = false;
			fleeing = false;
		}
		else
		{
			BattleReward.instance.OpenRewardScreen(rewardXP, rewardItems);
		}


		AudioManager.instance.PlayBackgroundMusic(FindObjectOfType<CameraConroller>().musicToPlay);
	}

	public IEnumerator GameOverCo()
	{
		battleActive = false;
		UIFade.instance.FadeToBlack();

		yield return new WaitForSeconds(1.5f);
		BattleScene.SetActive(false);

		SceneManager.LoadScene(gameOverScene);

	}
}
