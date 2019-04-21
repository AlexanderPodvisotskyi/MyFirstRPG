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
		if (GameManager.instense.itemHeldArray[buttonValue] != "")
		{
			GameMenu.instance.SelectItem(GameManager.instense.GetItemDetails(GameManager.instense.itemHeldArray[buttonValue]));
		}
	}
}
