using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;

    private Vector3 moveDirection;

    [SerializeField]
    private int speed;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float gravity;

    [SerializeField]
    private float angularSpeed;


    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
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

            moveDirection = transform.forward * 0.8f * speed;
            transform.Rotate(new Vector3(0,Input.GetAxis("Horizontal")* angularSpeed, 0));
        }else
        {
            moveDirection = transform.forward * Input.GetAxis("Vertical")*speed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");

            moveDirection.y = jumpSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
