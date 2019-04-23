using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instense;

	public CharStats[] playerStats;

	public bool gameMenuOpen;
	public bool dialogActive;
	public bool fadingBetweenAreas;
	public bool shopActive;


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
		if (gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive)
		{
			PlayerController.instense.canMove = false;
		}
		else
		{
			PlayerController.instense.canMove = true;
		}

		if (Input.GetKeyDown(KeyCode.J))
		{
			AddItem("Iron Armor");
			AddItem("Iron Kick");

			RemoveItem("Health Potion");
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
		else
		{

		}
	}
}
