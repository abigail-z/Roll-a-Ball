using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float movementSpeed;
    public Text countText;
    public Text winText;
    public AudioSource audioSource;

    private Rigidbody rb;
    private int count;
    private int winCount;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        winCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        count = 0;
        SetCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * movementSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count;
        if (count >= winCount)
        {
            audioSource.Play();
            winText.text = "You Win!";
        }
    }
}
