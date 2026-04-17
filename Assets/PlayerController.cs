using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Reference to the Player's Rigidbody2D component
    private Rigidbody2D rb;
    // Movement speed variable
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    public float facingDirection = 1f; // 1 for right, -1 for left

    // add implulse variable
    public float jumpImpulse = 1f;

    



    // Reference to the InputAction object
    private InputSystem_Actions controls;
    // Attack input action
    private InputAction attackAction;

    //if the player is grounded.
    private bool isGrounded;

    // Timer to check if player is grounded
    private float groundTimer = 0f;

    // idk why i decided to do it like this but here we are
    private float lastYPosition;

    // Layer Mask so that i can raytrace (is that optimal in unity?, unreal is line trace and its 'okay' as long as its not often or complex) attack
    public LayerMask attackMask;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();


        controls = new InputSystem_Actions();


        attackAction = controls.Player.Attack;
    }

    void OnEnable()
    {
      
        controls.Enable();

  
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Jump.performed += ctx => Jump();



        attackAction.performed += ctx => PerformAttack();
    }

    void OnDisable()
    {
    
        controls.Disable();
    }

    void Update()
    {
        // Move the player
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // Update facing direction if moving
        if (moveInput.x > 0)
            facingDirection = 1;
        else if (moveInput.x < 0)
            facingDirection = -1;
        //if sprite will flip rotation here 
       
        //Ground check
        if (Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            if (rb.position.y == lastYPosition)
                groundTimer += Time.deltaTime;
            else
                groundTimer = 0f;

            if (groundTimer >= 0.3f)
                isGrounded = true;
        }

        lastYPosition = rb.position.y;
    }
    void Jump()
    {
        // Apply an impulse if the player is grounded
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);  // Apply an upward impulse force
            isGrounded = false;  // Set isGrounded to false when jumping
            groundTimer = 0f;  // Reset the grounded timer when jumping
        }
    }


    void PerformAttack()
    {
        // get origin then shoot raycast in facing direction, check if has collider, if has collider check layer
        Vector2 origin = (Vector2)transform.position + Vector2.right * 0.5f * facingDirection;

        Debug.DrawRay(origin, Vector2.right * facingDirection * 1f, Color.red, 0.5f);

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.right * facingDirection,
            1f,
            attackMask
        );

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Block"))
            {
                Debug.Log("HIT BLOCK");
                hit.collider.GetComponent<BlockItem>()?.OnHit();
            }
            { //if enemy then take damage from enemy
                if (hit.collider.CompareTag("Enemy"))
                    hit.collider.GetComponent<SimpleEnemy>()?.TakeDamage();
            }
        }
        else
        {
            Debug.Log("No hit");
        }
    }
}