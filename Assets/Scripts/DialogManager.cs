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

	// Start is called before the first frame update
	void Start()
	{
		dialogText.text = dialogLinesArray[currentLine];
	}

	// Update is called once per frame
	void Update()
	{
		if (DialogBox.activeInHierarchy)
		{
			if (Input.GetButtonUp("EnterDialog"))
			{
				currentLine++;

				if (currentLine >= dialogLinesArray.Length)
				{
					DialogBox.SetActive(false);
				}
				else
				{
					dialogText.text = dialogLinesArray[currentLine];
				}
			}
		}

	}
}
