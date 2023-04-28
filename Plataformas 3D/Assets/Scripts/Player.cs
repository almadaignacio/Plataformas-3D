using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 direction = new Vector3(0f, 0f, 1f);
    public float Speed = 6f;
    public float cameraAxisX = 0f;
    public float SpeedRotation = 200.0f, Sensitivity = 70f;
    public float x, y;

    private Vector3 playerDirection;

    public float jumpValue;

    public SceneController sceneControl;

    CharacterController character;
    Rigidbody rb;
    Vector3 moveVector;
    Transform Cam;
    float yRotation;

    float horizontalInput;

    bool Is_Grounded;

    [SerializeField] Animator playerAnimator;

    public float speed;
    Vector3 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Is_Grounded = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cam = Camera.main.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float h = Input.GetAxisRaw("Horizontal");
        movement.Set(h, 0f, 0f);
        movement = movement * Speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        */
        RotatePlayer();

        float ymouse = Input.GetAxis("Mouse Y") * Time.deltaTime * Sensitivity;

        bool forward = Input.GetKeyDown(KeyCode.W);
        bool back = Input.GetKeyDown(KeyCode.S);
        bool left = Input.GetKeyDown(KeyCode.A);
        bool right = Input.GetKeyDown(KeyCode.D);
        bool jump = Input.GetKeyDown(KeyCode.Space);


        playerDirection = Vector3.zero;
        //Elegimos una dirección en función de la tecla que se mantiene presionada.
        if (Input.GetKey(KeyCode.W)) playerDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) playerDirection += Vector3.back;
        if (Input.GetKey(KeyCode.D)) playerDirection += Vector3.right;
        if (Input.GetKey(KeyCode.A)) playerDirection += Vector3.left;
        //Nos movemos solo si hay una dirección diferente que vector zero.
        if (playerDirection != Vector3.zero) MovePlayer(playerDirection);

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (forward) playerAnimator.SetTrigger("FORWARD");
        }

        
        if (jump) playerAnimator.SetTrigger("JUMP");

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (!IsAnimation("IDLE")) playerAnimator.SetTrigger("IDLE");
        }

        if (Input.GetKeyDown(KeyCode.Space) && Is_Grounded == true)
        {
            Jump();
        }
    }

    private void MovePlayer(Vector3 direction)
    {
        transform.Translate(direction * Speed * Time.deltaTime);
    }

    public void RotatePlayer()
    {
        float xmouse = Input.GetAxis("Mouse X") * Time.deltaTime * Sensitivity;
        float ymouse = Input.GetAxis("Mouse Y") * Time.deltaTime * Sensitivity;
        transform.Rotate(Vector3.up * xmouse);
        yRotation -= ymouse;
        yRotation = Mathf.Clamp(yRotation, -85f, 60f);
        Cam.localRotation = Quaternion.Euler(yRotation, 0, 0);
    }

    public void Jump()
    {
        Is_Grounded = false;
        rb.AddForce(0, jumpValue, 0, ForceMode.Impulse);
    }

    private bool IsAnimation(string animName)
    {
        return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animName);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Is_Grounded = true;
        }

        if (collision.gameObject.CompareTag("Water"))
        {
            sceneControl.ChangeLevel(2);
        }

        if (collision.gameObject.CompareTag("Victory"))
        {
            sceneControl.ChangeLevel(3);
        }
    }
}
