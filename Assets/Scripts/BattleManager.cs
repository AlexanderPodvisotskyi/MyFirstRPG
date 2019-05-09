﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		}

		turnWaiting = true;
		currentTurn = Random.Range(0, activeBattlers.Count);
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

			}
			else
			{
				if (activeBattlers[i].isPlayer)
				{
					allPlayersDead = false;
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
			}
			else
			{
				// Fail battle
			}

			BattleScene.SetActive(false);
			GameManager.instense.battleActive = false;
			battleActive = false;
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

		int selectedTarger = players[Random.Range(0, players.Count)];

		int selectAttack = Random.Range(0, activeBattlers[currentTurn].movesAvailable.Length);
		int movePower = 0;

		for (int i = 0; i < movesList.Length; i++)
		{
			if (movesList[i].moveName == activeBattlers[currentTurn].movesAvailable[selectAttack])
			{
				Instantiate(movesList[i].theEffect, activeBattlers[selectedTarger].transform.position, activeBattlers[selectedTarger].transform.rotation);
				movePower = movesList[i].movePower;
			}
		}

		Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);

		DealDamage(selectedTarger, movePower);
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
					playerHP[i].text = playerData.currentHP + "/" + playerData.maxHP;
					playerMP[i].text = playerData.currentMP + "/" + playerData.maxMP;
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
}