using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbodyes;
    public float moveSpeed;

    public Animator myAnimator;

    public static PlayerController instense;

    // Start is called before the first frame update
    void Start() {
        if (instense == null)
        {
            instense = this;
        } else 
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {

        rigidbodyes.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        myAnimator.SetFloat("moveX", rigidbodyes.velocity.x);
        myAnimator.SetFloat("moveY", rigidbodyes.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || 
            Input.GetAxisRaw("Vertical")   == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            myAnimator.SetFloat("lastMoveX", Input.GetAxis("Horizontal"));
            myAnimator.SetFloat("lastMoveY", Input.GetAxis("Vertical"));
        }
    }
}
