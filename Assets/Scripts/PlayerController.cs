using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D rigidbodyes;
	public float moveSpeed;

	public Animator myAnimator;

	public static PlayerController instense;

	public string areaTransitionName;

	private Vector3 bottomLeftLimit;
	private Vector3 topRightLimit;

	public bool canMove = true;

	// Start is called before the first frame update
	void Start()
	{
		if (instense == null)
		{
			instense = this;
		}
		else
		{
			if (instense != this)
				Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update()
	{
		if (canMove)
		{
			rigidbodyes.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
			myAnimator.SetFloat("moveX", rigidbodyes.velocity.x);
			myAnimator.SetFloat("moveY", rigidbodyes.velocity.y);

			if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 ||
			Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
			{
				myAnimator.SetFloat("lastMoveX", Input.GetAxis("Horizontal"));
				myAnimator.SetFloat("lastMoveY", Input.GetAxis("Vertical"));
			}

			transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
		}
		else
		{
			rigidbodyes.velocity = Vector2.zero;
		}
	}

	public void SetBounds(Vector3 bottomLeft, Vector3 topRight)
	{
		bottomLeftLimit = bottomLeft + new Vector3(0.5f, 1f, 0f);
		topRightLimit = topRight + new Vector3(-0.5f, -1f, 0f);
	}
}
