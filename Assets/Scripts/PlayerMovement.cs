using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Camera cam;
    public float mouseHorizontal = 3.0f;
    public float mouseVertical = 2.0f;
     public float minRotation = -65.0f;
    public float maxRotation = 60.0f;
    float h_mouse, v_mouse;
    public Animator animate;

    public float speed = 6f;
    public float runSpeed = 11f;
    public float jumpHeight = 2f;
    public float gravity = -9.18f;
    public float crouchHeight = 0.5f;
    public float normalHeight = 2f;

    private bool isGrounded;
    private float groundDistance = 3f;
    private Vector3 velocity;
    private bool isCrouching;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animate = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, LayerMask.GetMask("Ground"));
        if(isGrounded){
            animate.SetBool("IsJumping", false);
        }
        if(isGrounded && velocity.y < 0f){
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            animate.SetBool("Walk", true);
        }else{
            animate.SetBool("Walk", false);
        }

        float currentSpeed = isCrouching ? speed /2f : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed);
        if(Input.GetKey(KeyCode.LeftShift)){
            animate.SetBool("Walk", false);
            animate.SetBool("Run", true);
            Debug.Log("shift deberia animarse jueputa");
        }else{
            animate.SetBool("Run", false);
        }

        Vector3 moveDirection = transform.TransformDirection(direction) * currentSpeed * Time.deltaTime;
        controller.Move(moveDirection);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isGrounded = false;
            animate.SetTrigger("Jump");
            animate.SetBool("IsJumping", true);
            Debug.Log(isGrounded);
        }
        

        if(Input.GetKeyDown(KeyCode.LeftControl)){
            isCrouching = true;
            controller.height = crouchHeight;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl)){
            isCrouching = false;
            controller.height = normalHeight;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        h_mouse = mouseHorizontal * Input.GetAxis("Mouse X");
        v_mouse += mouseVertical * Input.GetAxis("Mouse Y");

        v_mouse = Mathf.Clamp(v_mouse, minRotation, maxRotation);


        cam.transform.localEulerAngles = new Vector3(-v_mouse, 0, 0);

        transform.Rotate(0, h_mouse, 0);
        /*float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * 100f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 100f;

        float rotationX = camTransform.rotation.eulerAngles.x;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        camTransform.rotation = Quaternion.Euler(rotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);*/
    }
}
