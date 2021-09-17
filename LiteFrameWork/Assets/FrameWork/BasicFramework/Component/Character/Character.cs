using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// The GameObject is made to bounce using the space key.
// Also the GameOject can be moved forward/backward and left/right.
// Add a Quad to the scene so this GameObject can collider with a floor.

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float gravity;

    Vector3 moveDirection = Vector3.zero;
    public UnityEngine.CharacterController controller;
    
    private Animator animator;

    public static bool isOver;
    void Start()
    {        
        animator = transform.Find("_cyborg_base").GetComponent<Animator>();

        controller = GetComponent<UnityEngine.CharacterController>();

        if (speed <= 0)
        {
            speed = 6.0f;

            Debug.Log("Speed not set on " + name + ". Defaulting to " + speed);
        }

        if (jumpSpeed <= 0)
        {
            jumpSpeed = 8.0f;

            Debug.Log("JumpSpeed not set on " + name + ". Defaulting to " + jumpSpeed);
        }

        if (gravity <= 0)
        {
            gravity = 20.0f;
        }
    }

    void Update()
    {        
        if (Input.GetAxis("Vertical")>0)
        {
            animator.SetBool("Run",true);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            animator.SetBool("Back", true);
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Back", false);
        }

        if (Input.GetAxis("Horizontal")!=0)
        {
            animator.SetBool("Run", true);
        }

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                GetComponent<AudioSource>().Play();
                animator.SetTrigger("Jump");
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        controller.Move(moveDirection * Time.deltaTime);
        
    }
}