using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instense;

	public CharStats[] playerStats;

	public bool gameMenuOpen;
	public bool dialogActive;
	public bool fadingBetweenAreas;
	public bool shopActive;
	public bool battleActive;



	public string[] itemHeldArray;
	public int[] numberOfItemsArray;
	public Item[] referenceItems;

	public int currentGold;

	// Start is called before the first frame update
	void Start()
	{
		instense = this;

		DontDestroyOnLoad(gameObject);

		SortItems();
	}

	// Update is called once per frame
	void Update()
	{
		if (gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive || battleActive)
		{
			PlayerController.instense.canMove = false;
		}
		else
		{
			PlayerController.instense.canMove = true;
		}

		if (Input.GetKeyDown(KeyCode.O))
		{
			SaveData();
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			LoadData();
		}

	}

	public Item GetItemDetails(string itemToGrab)
	{
		for (int i = 0; i < referenceItems.Length; i++)
		{
			if (referenceItems[i].itemName == itemToGrab)
			{
				return referenceItems[i];
			}
		}

		return null;
	}

	public void SortItems()
	{
		bool itemAfterSpace = true;

		while (itemAfterSpace)
		{
			itemAfterSpace = false;
			for (int i = 0; i < itemHeldArray.Length - 1; i++)
			{
				if (itemHeldArray[i] == "")
				{
					itemHeldArray[i] = itemHeldArray[i + 1];
					itemHeldArray[i + 1] = "";

					numberOfItemsArray[i] = numberOfItemsArray[i + 1];
					numberOfItemsArray[i + 1] = 0;

					if (itemHeldArray[i] != "")
					{
						itemAfterSpace = true;
					}
				}
			}
		}
	}

	public void AddItem(string itemToAdd)
	{
		int newItemPosition = 0;
		bool foundSpace = false;

		for (int i = 0; i < itemHeldArray.Length; i++)
		{
			if (itemHeldArray[i] == "" || itemHeldArray[i] == itemToAdd)
			{
				newItemPosition = i;
				i = itemHeldArray.Length;
				foundSpace = true;
			}
		}

		if (foundSpace)
		{
			bool itemExist = false;

			for (int i = 0; i < referenceItems.Length; i++)
			{
				if (referenceItems[i].itemName == itemToAdd)
				{
					itemExist = true;

					i = referenceItems.Length;
				}
			}

			if (itemExist)
			{
				itemHeldArray[newItemPosition] = itemToAdd;
				numberOfItemsArray[newItemPosition]++;
			}
			else
			{
				Debug.LogError(itemToAdd + " Does not Exist!");
			}
		}

		GameMenu.instance.ShowItems();
	}

	public void RemoveItem(string itemToRemove)
	{
		bool foundItem = false;
		int ItemPosition = 0;

		for (int i = 0; i < itemHeldArray.Length; i++)
		{
			if (itemHeldArray[i] == itemToRemove)
			{
				foundItem = true;
				ItemPosition = i;

				i = itemHeldArray.Length;
			}
		}

		if (foundItem)
		{
			numberOfItemsArray[ItemPosition]--;

			if (numberOfItemsArray[ItemPosition] <= 0)
			{
				itemHeldArray[ItemPosition] = "";
			}

			GameMenu.instance.ShowItems();
		}
	}

	public void SaveData()
	{
		PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
		PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instense.transform.position.x);
		PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instense.transform.position.y);
		PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instense.transform.position.z);


		// save character info
		for (int i = 0; i < playerStats.Length; i++)
		{
			if (playerStats[i].gameObject.activeInHierarchy)
			{
				PlayerPrefs.SetInt("Player_" + playerStats[i].nameHero + "_active", 1);
			}
			else
			{
				PlayerPrefs.SetInt("Player_" + playerStats[i].nameHero + "_active", 0);
			}

			PlayerPrefs.SetInt("Player_" + playerStats[i].nameHero + "_Level", playerStats[i].playerLevel);
			PlayerPrefs.SetInt("Player_" + playerStats[i].nameHero + "_CurrentExp", playerStats[i].currentExp);
			PlayerPrefs.SetInt("Player_" + playerStats[i].nameHero + "_CurrentHP", playerStats[i].currentHP);
			PlayerPrefs.SetInt("Player_" + playerStats[i].nameHero + "_MaxHP", playerStats[i].maxHP);
			PlayerPrefs.SetInt("Player_" + playerStats[i].nameHero + "_CurrentMP", playerStats[i].currentMP);
			PlayerPrefs.SetInt("Player_" + playerStats[i].nameHero + "_MaxMP", playerStats[i].maxMP);
			PlayerPrefs.SetInt("Player_" + playerStats[i].nameHero + "_Strength", playerStats[i].strength);
			PlayerPrefs.SetInt("Player_" + playerStats[i].nameHero + "_Defence", playerStats[i].defence);
			PlayerPrefs.SetInt("Player_" + playerStats[i].nameHero + "_WeaponPower", playerStats[i].weaponPower);
			PlayerPrefs.SetInt("Player_" + playerStats[i].nameHero + "_ArmorPower", playerStats[i].armorPower);
			PlayerPrefs.SetString("Player_" + playerStats[i].nameHero + "_EquipmentWeapon", playerStats[i].equippedWeapon);
			PlayerPrefs.SetString("Player_" + playerStats[i].nameHero + "_EquipmentArmor", playerStats[i].equippedArmor);
		}

		// Store inventory data
		for (int i = 0; i < itemHeldArray.Length; i++)
		{
			PlayerPrefs.SetString("ItemInInventory_" + i, itemHeldArray[i]);
			PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItemsArray[i]);
		}
	}

	public void LoadData()
	{
		PlayerController.instense.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));

		for (int i = 0; i < playerStats.Length; i++)
		{
			if (PlayerPrefs.GetInt("Player_" + playerStats[i].nameHero + "_active") == 0)
			{
				playerStats[i].gameObject.SetActive(false);
			}
			else
			{
				playerStats[i].gameObject.SetActive(true);
			}

			playerStats[i].playerLevel    = PlayerPrefs.GetInt("Player_" + playerStats[i].nameHero + "_Level");
			playerStats[i].currentExp     = PlayerPrefs.GetInt("Player_" + playerStats[i].nameHero + "_CurrentExp");
			playerStats[i].currentHP      = PlayerPrefs.GetInt("Player_" + playerStats[i].nameHero + "_CurrentHP");
			playerStats[i].maxHP          = PlayerPrefs.GetInt("Player_" + playerStats[i].nameHero + "_MaxHP");
			playerStats[i].currentMP      = PlayerPrefs.GetInt("Player_" + playerStats[i].nameHero + "_CurrentMP");
			playerStats[i].maxMP          = PlayerPrefs.GetInt("Player_" + playerStats[i].nameHero + "_MaxMP");
			playerStats[i].strength       = PlayerPrefs.GetInt("Player_" + playerStats[i].nameHero + "_Strength");
			playerStats[i].defence        = PlayerPrefs.GetInt("Player_" + playerStats[i].nameHero + "_Defence");
			playerStats[i].weaponPower    = PlayerPrefs.GetInt("Player_" + playerStats[i].nameHero + "_WeaponPower");
			playerStats[i].armorPower     = PlayerPrefs.GetInt("Player_" + playerStats[i].nameHero + "_ArmorPower");
			playerStats[i].equippedWeapon = PlayerPrefs.GetString("Player_" + playerStats[i].nameHero + "_EquipmentWeapon");
			playerStats[i].equippedArmor  = PlayerPrefs.GetString("Player_" + playerStats[i].nameHero + "_EquipmentArmor");
		}

		for (int i = 0; i < itemHeldArray.Length; i++)
		{
			itemHeldArray[i]      = PlayerPrefs.GetString("ItemInInventory_" + i);
			numberOfItemsArray[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
		}
	}
}
