using UnityEngine;

public class AreaEntrance : MonoBehaviour
{

	public string transitionName;
	// Start is called before the first frame update
	void Start()
	{
		if (transitionName == PlayerController.instense.areaTransitionName)
		{
			PlayerController.instense.transform.position = transform.position;
		}

		UIFade.instance.FadeFromBlack();
		GameManager.instense.fadingBetweenAreas = false;

	}

	// Update is called once per frame
	void Update()
	{

	}
}
