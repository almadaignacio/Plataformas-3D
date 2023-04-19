using System.Collections;
using System.Collections.Generic;
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

    CharacterController character;
    Rigidbody rb;
    Vector3 moveVector;
    Transform Cam;
    float yRotation;

    float horizontalInput;

    bool Is_Grounded;

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
        
        bool forward = Input.GetKeyDown(KeyCode.W);
        bool back = Input.GetKeyDown(KeyCode.S);
        bool left = Input.GetKeyDown(KeyCode.A);
        bool right = Input.GetKeyDown(KeyCode.D);

        playerDirection = Vector3.zero;
        //Elegimos una dirección en función de la tecla que se mantiene presionada.
        if (Input.GetKey(KeyCode.W)) playerDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) playerDirection += Vector3.back;
        if (Input.GetKey(KeyCode.D)) playerDirection += Vector3.right;
        if (Input.GetKey(KeyCode.A)) playerDirection += Vector3.left;
        //Nos movemos solo si hay una dirección diferente que vector zero.
        if (playerDirection != Vector3.zero) MovePlayer(playerDirection);
        
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
        cameraAxisX += Input.GetAxis("Mouse X");
        Quaternion newRotation = Quaternion.Euler(0, cameraAxisX, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 2f * Time.deltaTime);
    }

    public void Jump()
    {
        Is_Grounded = false;
        rb.AddForce(0, jumpValue, 0, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Is_Grounded = true;
        }
    }
}
