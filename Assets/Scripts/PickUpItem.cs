using UnityEngine;

public class PickUpItem : MonoBehaviour
{

	private bool canPickup;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (canPickup && Input.GetButtonDown("PickUpItems") && PlayerController.instense.canMove)
		{
			GameManager.instense.AddItem(GetComponent<Item>().itemName);
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			canPickup = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			canPickup = false;
		}
	}
}
