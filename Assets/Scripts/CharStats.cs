using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
	public string nameHero;
	public int playerLevel = 1;
	public int currentExp;

	public int currentHP;
	public int maxHP = 100;

	public int currentMP;
	public int maxMP = 30;

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

	}

	// Update is called once per frame
	void Update()
	{

	}
}
