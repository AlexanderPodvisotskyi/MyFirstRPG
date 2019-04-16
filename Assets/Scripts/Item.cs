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
}
