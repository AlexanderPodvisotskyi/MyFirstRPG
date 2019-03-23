using UnityEngine;

public class DialogActivator : MonoBehaviour
{
	public string[] lines;

	private bool canActivate;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (canActivate && Input.GetButtonDown("EnterDialog") && !DialogManager.instance.DialogBox.activeInHierarchy)
		{
 			DialogManager.instance.ShowDialog(lines);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			canActivate = true; 
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			canActivate = false;
		}
	}

}
