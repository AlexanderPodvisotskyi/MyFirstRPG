using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instense;

	public CharStats[] playerStats;

	public bool gameMenuOpen;
	public bool dialogActive;
	public bool fadingBetweenAreas;

	public string[] itemHeldArray;
	public int[] numberOfItemsArray;
	public Item[] referenceItems;

	// Start is called before the first frame update
	void Start()
	{
		instense = this;

		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update()
	{
		if (gameMenuOpen || dialogActive || fadingBetweenAreas)
		{
			PlayerController.instense.canMove = false;
		}
		else
		{
			PlayerController.instense.canMove = true;
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

	public void AddItem (string itemToAdd)
	{

	}

	public void RemoveItem(string itemToRemove)
	{

	}
}
