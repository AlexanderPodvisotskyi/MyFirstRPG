using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
	private bool canOpen;

	public string[] ItemsForSale;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (canOpen && Input.GetButtonDown("EnterDialog") && PlayerController.instense.canMove && !Shop.instence.shopMenu.activeInHierarchy)
		{
			Shop.instence.ItemsForSale = ItemsForSale;

			Shop.instence.OpenShop();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			canOpen = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			canOpen = false;
		}
	}
}
