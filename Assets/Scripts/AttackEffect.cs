using UnityEngine;

public class AttackEffect : MonoBehaviour
{
	public float effectLength;
	public int soundEffect;
	// Start is called before the first frame update
	void Start()
	{
		AudioManager.instance.PlaySoundEffects(soundEffect);
	}

	// Update is called once per frame
	void Update()
	{
		Destroy(gameObject, effectLength);
	}
}
