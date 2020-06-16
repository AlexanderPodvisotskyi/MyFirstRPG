using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D rigidbodyes;
	public float moveSpeed;

	public Animator myAnimator;

	public Joystick joystick;

	public static PlayerController instense;

	public string areaTransitionName;

	private Vector3 bottomLeftLimit;
	private Vector3 topRightLimit;

	public bool canMove = true;

	private float horizontalMove;
	private float verticalMove;

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
			if (joystick.Horizontal >= .2f)
			{
				horizontalMove = moveSpeed;
			}
			else if (joystick.Horizontal <= -.2f)
			{
				horizontalMove = -moveSpeed;
			}
			else
			{
				horizontalMove = 0;
			}

			if (joystick.Vertical >= .2f)
			{
				verticalMove = moveSpeed;
			}
			else if (joystick.Vertical <= -.2f)
			{
				verticalMove = -moveSpeed;
			}
			else
			{
				verticalMove = 0;
			}

			rigidbodyes.velocity = new Vector2(horizontalMove, verticalMove) * moveSpeed;
			myAnimator.SetFloat("moveX", rigidbodyes.velocity.x);
			myAnimator.SetFloat("moveY", rigidbodyes.velocity.y);

			if (horizontalMove == 1 || horizontalMove == -1 ||
			verticalMove == 1 || verticalMove == -1)
			{
				myAnimator.SetFloat("lastMoveX", horizontalMove);
				myAnimator.SetFloat("lastMoveY", verticalMove);
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
