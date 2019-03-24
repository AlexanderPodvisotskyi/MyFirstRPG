using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
	public string nameHero;

	public int playerLevel = 1;

	public int currentExp;
	public int[] expToNextLevel;
	public int maxLevel = 100;
	public int baseExp = 1000;

	public int currentHP;
	public int maxHP = 100;

	public int currentMP;
	public int maxMP = 30;
	public int[] mpLvlBonus;

	public int strength;
	public int defence;

	public int weaponPower;
	public int armorPower;

	public string equippedWeapon;
	public string equippedArmor;

	public Sprite charImage;

	// Start is called before the first frame update
	void Start()
	{
		expToNextLevel = new int[maxLevel];
		expToNextLevel[1] = baseExp;

		for (int i = 2; i < expToNextLevel.Length; i++)
		{
			expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f) - (Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f) % 50);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			AddExp(1000);
		}
	}

	public void AddExp(int ExpToAdd)
	{
		currentExp += ExpToAdd;

		if (playerLevel < maxLevel)
		{
			if (currentExp >= expToNextLevel[playerLevel])
			{
				currentExp -= expToNextLevel[playerLevel];

				maxHP = Mathf.FloorToInt(maxHP * 1.05f);
				currentHP = maxHP;

				maxMP += mpLvlBonus[playerLevel - 1];
				currentMP = maxMP;

				var addParametr = Random.Range(2, 5);

				if (playerLevel % 2 == 0)
				{

					strength += addParametr;
				}
				else
				{
					defence += addParametr;
				}
				playerLevel++;
			}
		}

		if (playerLevel >= maxLevel)
		{
			currentExp = 0;
		}
	}
}
