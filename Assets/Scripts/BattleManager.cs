using System.Collections.Generic;
using UnityEngine;

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
			Debug.Log("Work");
			BattleStart(new string[] { "Goblin", "Spider", "Goblin", "Goblin", "Goblin" });
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
	}
}
