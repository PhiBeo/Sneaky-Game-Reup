using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Moving Setting")] 
    [SerializeField] CharacterController character;
    [SerializeField] float speed = 5f;
    [SerializeField] float turnSmoothTime = .1f;
    [SerializeField] private Transform cam;

    [Header("Ground and Jump Setting")]
    [SerializeField] private bool airControl = true;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.5f;
    [SerializeField] private LayerMask whatIsGround; 
    
    
    private float horizontal = 0f;
    private float vertical = 0f;
    private float turnSmoothVelocity;
    private Vector3 direction;
    private Vector3 moveDirection;
    private Vector3 velocity;
    bool isGrounded;
    private bool _isDead;

    private void Start()
    {
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;

        Physics.autoSyncTransforms = enabled;

        _isDead = false;
    }

    void Update()
    {
        if(!_isDead)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);

            if (isGrounded && velocity.y < 0f)
            {
                velocity.y = -2f;
            }

            if (airControl)
            {
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");
            }
            else if (!airControl)
            {
                if (isGrounded)
                {
                    horizontal = Input.GetAxisRaw("Horizontal");
                    vertical = Input.GetAxisRaw("Vertical");
                }
            }

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            character.Move(velocity * Time.deltaTime);

            direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                character.Move(moveDirection.normalized * speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _isDead = true;
        }
    }

    public bool isDead()
    {
        return _isDead;
    }
}
