using UnityEngine;

public class Item : MonoBehaviour
{
	[Header("Item Type")]
	public bool isItem;
	public bool isWeapon;
	public bool isArmour;

	[Header ("Item Details")]
	public string itemName;
	public string description;
	public int value;
	public Sprite itemSprite;

	[Header("Item Details")]
	public int amountToChange;
	public bool affectHP;
	public bool affectMP;
	public bool affectStrength;

	[Header ("Weapon/Armor Details")]
	public int weaponStrength;
	public int armorStrength;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Use(int charToUseOn)
	{
		CharStats selectedChar = GameManager.instense.playerStats[charToUseOn];

		if (isItem)
		{
			if (affectHP)
			{
				selectedChar.currentHP += amountToChange;

				if (selectedChar.currentHP > selectedChar.maxHP)
				{
					selectedChar.currentHP = selectedChar.maxHP;
				}
			}

			if (affectMP)
			{
				selectedChar.currentMP += amountToChange;

				if (selectedChar.currentMP > selectedChar.maxMP)
				{
					selectedChar.currentMP = selectedChar.maxMP;
				}
			}

			if (affectStrength)
			{
				selectedChar.strength += amountToChange;
			}
		}

		if (isWeapon)
		{
			if (selectedChar.equippedWeapon != "")
			{
				GameManager.instense.AddItem(selectedChar.equippedWeapon);
			}

			selectedChar.equippedWeapon = itemName;
			selectedChar.weaponPower = weaponStrength;
		}

		if (isArmour)
		{
			if (selectedChar.equippedArmor != "")
			{
				GameManager.instense.AddItem(selectedChar.equippedArmor);
			}

			selectedChar.equippedArmor = itemName;
			selectedChar.armorPower = armorStrength;
		}

		GameManager.instense.RemoveItem(itemName);
	}
}
