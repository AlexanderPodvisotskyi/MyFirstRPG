using UnityEngine;

public class GameMenu : MonoBehaviour
{
	public GameObject theMenu;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("OpenMenu"))
		{
			if (theMenu.activeInHierarchy)
			{
				theMenu.SetActive(false);
			} 
			else
			{
				theMenu.SetActive(true);
			}
		}

	}
}
