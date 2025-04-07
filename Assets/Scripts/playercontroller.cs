using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playercontroller : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce = 5f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject; 
    private Rigidbody rb; 
    private int count;
    private float movementX;
    private float movementY;
    private bool isGrounded = true;
    private bool canDbl = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump ()
    {
        if(isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            canDbl = true;
        }
        else if(canDbl)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canDbl = false;
        }
        
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        
        if(count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Basic ground check: assumes only the ground has the "Ground" tag
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
   
}
