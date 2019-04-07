using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
	public GameObject theMenu;
	public GameObject[] windows;

	public Text[] nameTextArray;
	public Text[] hpTextArray;
	public Text[] mpTextArray;
	public Text[] lvlTextArray;
	public Image[] personalImage;

	public Text[] expTextArray;
	public Slider[] expSlider;

	public GameObject[] charStatHolder;
	private CharStats[] playerStats;


	public GameObject[] statusButtonsArray;

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
				CloseMenu();
			}
			else
			{
				theMenu.SetActive(true);
				UpdateMainStats();
				GameManager.instense.gameMenuOpen = true;
			}
		}
	}

	public void UpdateMainStats()
	{
		playerStats = GameManager.instense.playerStats;

		for (int i = 0; i < playerStats.Length; i++)
		{
			if (playerStats[i].gameObject.activeInHierarchy)
			{
				charStatHolder[i].SetActive(true);

				nameTextArray[i].text = playerStats[i].nameHero;
				hpTextArray[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
				mpTextArray[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
				lvlTextArray[i].text = "Level: " + playerStats[i].playerLevel;
				expTextArray[i].text = "" + playerStats[i].currentExp + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
				expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
				expSlider[i].value = playerStats[i].currentExp;
				personalImage[i].sprite = playerStats[i].charImage;
			}
			else
			{
				charStatHolder[i].SetActive(false);
			}
		}
	}

	public void ToggleWindow(int windowNumber)
	{
		UpdateMainStats();

		for (int i = 0; i < windows.Length; i++)
		{
			if (i == windowNumber)
			{
				windows[i].SetActive(!windows[i].activeInHierarchy);
			}
			else
			{
				windows[i].SetActive(false);
			}
		}
	}

	public void OpenStatus()
	{
		UpdateMainStats();
		for (int i = 0; i < statusButtonsArray.Length; i++)
		{
			statusButtonsArray[i].SetActive(playerStats[i].gameObject.activeInHierarchy);

			statusButtonsArray[i].GetComponentInChildren<Text>().text = playerStats[i].nameHero;
		}
	}

	public void CloseMenu()
	{
		for (int i = 0; i < windows.Length; i++)
		{
			windows[i].SetActive(false);
		}

		theMenu.SetActive(false);

		GameManager.instense.gameMenuOpen = false;
	}
}
