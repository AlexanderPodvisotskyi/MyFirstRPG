using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

	public static Shop instence;

	public GameObject shopMenu;
	public GameObject buyMenu;
	public GameObject sellMenu;

	public Text goldText;

	public string[] ItemsForSale;

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
		buyMenu.SetActive(true);
		sellMenu.SetActive(false);
	}

	public void OpenSellMenu()
	{
		sellMenu.SetActive(true);
		buyMenu.SetActive(false);
	}
}
