using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

	public static Shop instence;

	public GameObject shopMenu;
	public GameObject buyMenu;
	public GameObject sellMenu;

	public Text goldText;

	public string[] ItemsForSale = new string[60];

	public ItemButton[] buyItemButtonsArray;
	public ItemButton[] sellItemButtonsArray;

	public Item selectedItem;

	public Text buyItemName;
	public Text buyItemValue;
	public Text buyItemDescription;

	public Text sellItemName;
	public Text sellItemValue;
	public Text sellItemDescription;

	// Start is called before the first frame update
	void Start()
	{
		instence = this;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R) && !shopMenu.activeInHierarchy)
		{
			OpenShop();
		}
	}

	public void OpenShop()
	{
		shopMenu.SetActive(true);
		OpenBuyMenu();

		GameManager.instense.shopActive = true;

		goldText.text = GameManager.instense.currentGold.ToString() + "g";
	}

	public void CloseShop()
	{
		shopMenu.SetActive(false);

		GameManager.instense.shopActive = false;
	}

	public void OpenBuyMenu()
	{
		buyItemButtonsArray[0].Press();

		buyMenu.SetActive(true);
		sellMenu.SetActive(false);

		GameManager.instense.SortItems();

		for (int i = 0; i < buyItemButtonsArray.Length; i++)
		{
			buyItemButtonsArray[i].buttonValue = i;

			if (ItemsForSale[i] != "")
			{
				buyItemButtonsArray[i].buttonImage.gameObject.SetActive(true);
				buyItemButtonsArray[i].buttonImage.sprite = GameManager.instense.GetItemDetails(ItemsForSale[i]).itemSprite;
				buyItemButtonsArray[i].amountText.text = "";
			}
			else
			{
				buyItemButtonsArray[i].buttonImage.gameObject.SetActive(false);
				buyItemButtonsArray[i].amountText.text = "";
			}
		}
	}

	public void OpenSellMenu()
	{
		buyItemButtonsArray[0].Press();

		sellMenu.SetActive(true);
		buyMenu.SetActive(false);

		ShowSellItems();
	}

	private void ShowSellItems()
	{
		GameManager.instense.SortItems();

		for (int i = 0; i < sellItemButtonsArray.Length; i++)
		{
			sellItemButtonsArray[i].buttonValue = i;

			if (GameManager.instense.itemHeldArray[i] != "")
			{
				sellItemButtonsArray[i].buttonImage.gameObject.SetActive(true);
				sellItemButtonsArray[i].buttonImage.sprite = GameManager.instense.GetItemDetails(GameManager.instense.itemHeldArray[i]).itemSprite;
				sellItemButtonsArray[i].amountText.text = GameManager.instense.numberOfItemsArray[i].ToString();
			}
			else
			{
				sellItemButtonsArray[i].buttonImage.gameObject.SetActive(false);
				sellItemButtonsArray[i].amountText.text = "";
			}
		}
	}

	public void SelectBuyItem(Item buyItem)
	{
		if (buyItem)
		{
			selectedItem = buyItem;
			buyItemName.text = selectedItem.itemName;
			buyItemDescription.text = selectedItem.description;
			buyItemValue.text = "Value: " + selectedItem.value + "g";
		}
	}

	public void SelectSellItem(Item sellItem)
	{
		if (sellItem)
		{
			selectedItem = sellItem;
			sellItemName.text = selectedItem.itemName;
			sellItemDescription.text = selectedItem.description;
			sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.value * 0.5f).ToString() + "g";
		}
	}

	public void BuyItem()
	{
		if (selectedItem != null)
		{
			if (GameManager.instense.currentGold >= selectedItem.value)
			{
				GameManager.instense.currentGold -= selectedItem.value;

				GameManager.instense.AddItem(selectedItem.itemName);
			}
		}
		goldText.text = GameManager.instense.currentGold.ToString() + "g";
	}

	public void SellItem()
	{
		if (selectedItem != null)
		{
			GameManager.instense.currentGold += Mathf.FloorToInt(selectedItem.value * 0.5f);

			GameManager.instense.RemoveItem(selectedItem.itemName);
		}

		goldText.text = GameManager.instense.currentGold.ToString() + "g";

		ShowSellItems();
	}
}
