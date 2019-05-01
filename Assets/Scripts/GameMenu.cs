using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
	public GameObject theMenu;
	public GameObject[] windows;

	public Text[] nameTextArray;
	public Text[] hpTextArray;
	public Text[] mpTextArray;
	public Text[] lvlTextArray;
	public Image[] personalImage;

	public Text[] expTextArray;
	public Slider[] expSlider;

	public GameObject[] charStatHolder;
	private CharStats[] playerStats;


	public GameObject[] statusButtonsArray;

	public Text statusName;
	public Text statusHP;
	public Text statusMP;
	public Text statusStrength;
	public Text statusDefence;
	public Text statusWeaponEquipped;
	public Text statusWeaponPower;
	public Text statusArmorEquipped;
	public Text statusArmorPower;
	public Text statusExp;
	public Image statusImage;

	public ItemButton[] itemButtonsArray;
	public string selectedItem;
	public Item activeItem;

	public Text itemName;
	public Text itemDescription;
	public Text useButtonText;

	public GameObject itemCharChoiceMenu;
	public Text[] itemCharChoiceNames;

	public static GameMenu instance;

	public string mainMenuName;

	public Text goldText;
	// Start is called before the first frame update
	void Start()
	{
		instance = this;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("OpenMenu"))
		{
			if (theMenu.activeInHierarchy)
			{
				CloseMenu();
			}
			else
			{
				theMenu.SetActive(true);
				UpdateMainStats();
				GameManager.instense.gameMenuOpen = true;
			}

			AudioManager.instance.PlaySoundEffects(6);
		}
	}

	public void UpdateMainStats()
	{
		playerStats = GameManager.instense.playerStats;

		for (int i = 0; i < playerStats.Length; i++)
		{
			if (playerStats[i].gameObject.activeInHierarchy)
			{
				charStatHolder[i].SetActive(true);

				nameTextArray[i].text = playerStats[i].nameHero;
				hpTextArray[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
				mpTextArray[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
				lvlTextArray[i].text = "Level: " + playerStats[i].playerLevel;
				expTextArray[i].text = "" + playerStats[i].currentExp + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
				expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
				expSlider[i].value = playerStats[i].currentExp;
				personalImage[i].sprite = playerStats[i].charImage;
			}
			else
			{
				charStatHolder[i].SetActive(false);
			}
		}

		goldText.text = GameManager.instense.currentGold.ToString() + "g";
	}

	public void ToggleWindow(int windowNumber)
	{
		UpdateMainStats();

		for (int i = 0; i < windows.Length; i++)
		{
			if (i == windowNumber)
			{
				windows[i].SetActive(!windows[i].activeInHierarchy);
			}
			else
			{
				windows[i].SetActive(false);
			}
		}

		itemCharChoiceMenu.SetActive(false);
	}

	public void OpenStatus()
	{
		UpdateMainStats();
		StatusPlayersUpdate(0);
		for (int i = 0; i < statusButtonsArray.Length; i++)
		{
			statusButtonsArray[i].SetActive(playerStats[i].gameObject.activeInHierarchy);

			statusButtonsArray[i].GetComponentInChildren<Text>().text = playerStats[i].nameHero;
		}
	}

	public void StatusPlayersUpdate(int selectedPlayer)
	{
		statusName.text = playerStats[selectedPlayer].nameHero;

		statusHP.text = playerStats[selectedPlayer].currentHP + "/" + playerStats[selectedPlayer].maxHP;
		statusMP.text = playerStats[selectedPlayer].currentMP + "/" + playerStats[selectedPlayer].maxMP;

		statusStrength.text = playerStats[selectedPlayer].strength.ToString();
		statusDefence.text = playerStats[selectedPlayer].defence.ToString();

		statusExp.text = (playerStats[selectedPlayer].expToNextLevel[playerStats[selectedPlayer].playerLevel] - playerStats[selectedPlayer].currentExp).ToString();

		statusImage.sprite = playerStats[selectedPlayer].charImage;

		if (playerStats[selectedPlayer].equippedWeapon != "")
		{
			statusWeaponEquipped.text = playerStats[selectedPlayer].equippedWeapon;
		}

		statusWeaponPower.text = playerStats[selectedPlayer].weaponPower.ToString();

		if (playerStats[selectedPlayer].equippedArmor != "")
		{
			statusArmorEquipped.text = playerStats[selectedPlayer].equippedArmor;
		}

		statusArmorPower.text = playerStats[selectedPlayer].armorPower.ToString();
	}

	public void CloseMenu()
	{
		for (int i = 0; i < windows.Length; i++)
		{
			windows[i].SetActive(false);
		}

		theMenu.SetActive(false);

		GameManager.instense.gameMenuOpen = false;
		itemCharChoiceMenu.SetActive(false);
	}

	public void ShowItems()
	{
		GameManager.instense.SortItems();
		for (int i = 0; i < itemButtonsArray.Length; i++)
		{
			itemButtonsArray[i].buttonValue = i;

			if (GameManager.instense.itemHeldArray[i] != "")
			{
				itemButtonsArray[i].buttonImage.gameObject.SetActive(true);
				itemButtonsArray[i].buttonImage.sprite = GameManager.instense.GetItemDetails(GameManager.instense.itemHeldArray[i]).itemSprite;
				itemButtonsArray[i].amountText.text = GameManager.instense.numberOfItemsArray[i].ToString();
			}
			else
			{
				itemButtonsArray[i].buttonImage.gameObject.SetActive(false);
				itemButtonsArray[i].amountText.text = "";
			}
		}
	}

	public void SelectItem(Item newItem)
	{
		activeItem = newItem;

		if (activeItem.isItem)
		{
			useButtonText.text = "Use";
		}

		if (activeItem.isWeapon || activeItem.isArmour)
		{
			useButtonText.text = "Equip";
		}

		itemName.text = activeItem.itemName;
		itemDescription.text = activeItem.description;
	}

	public void DiscardItem()
	{
		if (activeItem != null)
		{
			GameManager.instense.RemoveItem(activeItem.itemName);
		}
	}

	public void OpenItemCharacterChoice()
	{
		itemCharChoiceMenu.SetActive(true);

		for (int i = 0; i < itemCharChoiceNames.Length; i++)
		{
			itemCharChoiceNames[i].text = GameManager.instense.playerStats[i].nameHero;
			itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instense.playerStats[i].gameObject.activeInHierarchy);
		}
	}

	public void CloseItemCharacterChoice()
	{
		itemCharChoiceMenu.SetActive(false);
	}

	public void UseItem(int selectChar)
	{
		activeItem.Use(selectChar);
		CloseItemCharacterChoice();
	}

	public void SaveGame()
	{
		GameManager.instense.SaveData();
		QuestManager.instance.SaveQuestData();
	}

	public void playButtonSound()
	{
		AudioManager.instance.PlaySoundEffects(5);
	}

	public void QuitGame()
	{
		SceneManager.LoadScene(mainMenuName);

		Destroy(GameManager.instense.gameObject);
		Destroy(PlayerController.instense.gameObject);
		Destroy(AudioManager.instance.gameObject);
		Destroy(gameObject);
	}
}
