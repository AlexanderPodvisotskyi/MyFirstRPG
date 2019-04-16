using System.Collections;
using System.Collections.Generic;
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
}
