using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;

    [Header("Ground Check")]
    public float playerHeight;
    public float groundDrag;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;


    float horizInput;
    float vertInput;

    Vector3 moveDir;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        getInput();

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // drag
        if(grounded){
            rb.drag = groundDrag;
        }else{
            rb.drag = 0.0f;
        }
    }

    void FixedUpdate(){
        movePlayer();
        speedControl();
    }

    private void getInput(){
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");

        // Jumping
        if(Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded){
            Debug.Log("jumping");
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void movePlayer(){
        // calculate movement direction
        moveDir = orientation.forward * vertInput + orientation.right * horizInput;

        if(grounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDir.normalized * moveSpeed * airMultiplier * 10f, ForceMode.Force);
    }

    private void speedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        if(flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump(){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump(){
        readyToJump = true;
    }
}
