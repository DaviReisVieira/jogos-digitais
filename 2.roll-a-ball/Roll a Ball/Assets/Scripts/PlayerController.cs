using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float speed = 3.0f;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;
    public GameObject winPanelObject;
    public GameObject losePanelObject;

    private Rigidbody rb;
    
    private int count;
    private float timer = 90.0f;
    private bool isWin = false;

    private int life = 4;
    public GameObject[] lifeObjects;


    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0; 
        SetCountText();   
        winPanelObject.SetActive(false);   
        losePanelObject.SetActive(false); 
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        
        Debug.Log(movementVector);
        
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Targets: " + count.ToString() + "/12";
        if (count >= 12)
        {
            winPanelObject.SetActive(true);
            isWin = true;
            // lock moviment
            movementX = 0;
            movementY = 0;

            // lock all
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;     

            SetCountText();       
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WallEnemy"))
        {
            Debug.Log("Collision with WallEnemy");

            life = life - 1;
            lifeObjects[life].SetActive(false);

            if (life <= 0)
            {
                losePanelObject.SetActive(true);
                // lock moviment
                movementX = 0;
                movementY = 0;

                // lock all
                rb.constraints = RigidbodyConstraints.FreezeAll;


            }
        }
    }

    void UpdateTimer(float timerValue)
    {
        timerValue += 1;

        float minutes = Mathf.FloorToInt(timerValue / 60);
        float seconds = Mathf.FloorToInt(timerValue % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        if (timer > 0 && !isWin && life > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimer(timer);
        }
        else if (timer <= 0 && !isWin)
        {
            losePanelObject.SetActive(true);
            // lock moviment
            movementX = 0;
            movementY = 0;

            // lock all
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        if (transform.position.y < -20)
        {
            // lost all lifes
            for (int i = 0; i < life; i++)
            {
                lifeObjects[i].SetActive(false);
            }
            
            life = 0;

            losePanelObject.SetActive(true);
            // lock moviment
            movementX = 0;
            movementY = 0;

            // lock all
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }
}
