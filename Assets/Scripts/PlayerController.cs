using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float movementSpeed;
    public float jumpForce;
    public Text countText;
    public Text winText;
    public AudioSource audioSource;

    private Rigidbody rb;
    private int count;
    private int winCount;
    private bool isGrounded;
    private float groundDistance;
    // input bools
    private bool jumpPressed;

    private void Start()
    {
        // get rigidbody from player
        rb = GetComponent<Rigidbody>();
        // get distance of collider from ground
        groundDistance = GetComponent<Collider>().bounds.extents.y;
        // count number of pickups needed to trigger win
        winCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        count = 0;
        // initialize ui text
        winText.text = "";
        countText.text = "Count: 0";
        // input bool intialization
        jumpPressed = false;
    }

    void Update()
    {
        // if Spacebar pressed, attempt jump on next FixedUpdate
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        // create movement vector from inputs
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        movement *= movementSpeed;
        // make sure force isn't larger than movementSpeed
        movement = Vector3.ClampMagnitude(movement, movementSpeed);
        // apply it to the player
        rb.AddForce(movement);

        // if jump input detected, check for ground before jumping
        if (jumpPressed && Physics.Raycast(rb.position, Vector3.down, groundDistance + 0.1f))
        {
            jumpPressed = false;
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // if trigger is pickup, do some picking up
        if (other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject); // won't be reusing, just delete
            count++;
            countText.text = "Count: " + count;
            // if all pickups found, do victory stuff
            if (count >= winCount)
            {
                audioSource.Play();
                winText.text = "You Win!";
            }
        }
    }
}
