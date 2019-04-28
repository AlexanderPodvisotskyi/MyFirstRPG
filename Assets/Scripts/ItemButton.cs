using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
	public Image buttonImage;
	public Text amountText;
	public int buttonValue;


	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Press()
	{
		if (GameMenu.instance.theMenu.activeInHierarchy)
		{
			if (GameManager.instense.itemHeldArray[buttonValue] != "")
			{
				GameMenu.instance.SelectItem(GameManager.instense.GetItemDetails(GameManager.instense.itemHeldArray[buttonValue]));
			}
		}

		if (Shop.instence.shopMenu.activeInHierarchy)
		{
			if (Shop.instence.buyMenu.activeInHierarchy)
			{
				Shop.instence.SelectBuyItem(GameManager.instense.GetItemDetails(Shop.instence.ItemsForSale[buttonValue]));
			}

			if (Shop.instence.sellMenu.activeInHierarchy)
			{
				Shop.instence.SelectSellItem(GameManager.instense.GetItemDetails(GameManager.instense.itemHeldArray[buttonValue]));
			}
		}
	}
}
