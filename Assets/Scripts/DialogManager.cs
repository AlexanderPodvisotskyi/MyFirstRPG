using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
	public Text dialogText;
	public Text nameNPSText;

	public GameObject DialogBox;
	public GameObject NPSBox;

	public string[] dialogLinesArray;

	public int currentLine;
	private bool justStartet;

	public static DialogManager instance;

	// Start is called before the first frame update
	void Start()
	{
		instance = this;

		// dialogText.text = dialogLinesArray[currentLine];
	}

	// Update is called once per frame
	void Update()
	{
		if (DialogBox.activeInHierarchy)
		{
			if (Input.GetButtonUp("EnterDialog"))
			{
				if (!justStartet)
				{
					currentLine++;

					if (currentLine >= dialogLinesArray.Length)
					{
						DialogBox.SetActive(false);

						PlayerController.instense.canMove = true;
					}
					else
					{
						checkName();

						dialogText.text = dialogLinesArray[currentLine];
					}
				}
				else
				{
					justStartet = false;
				}
			}
		}
	}

	public void ShowDialog(string [] newLines, bool isNPS)
	{
		dialogLinesArray = newLines;

		currentLine = 0;

		checkName();

		dialogText.text = dialogLinesArray[currentLine];
		DialogBox.SetActive(true);

		NPSBox.SetActive(isNPS);

		justStartet = true;
		PlayerController.instense.canMove = false;
	}

	public void checkName()
	{
		if(dialogLinesArray[currentLine].StartsWith("n-"))
		{
			nameNPSText.text = dialogLinesArray[currentLine].Replace("n-", "");
			currentLine++;
		}
	}
}
