using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instense;

	public CharStats[] playerStats;

	// Start is called before the first frame update
	void Start()
	{
		instense = this;

		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
