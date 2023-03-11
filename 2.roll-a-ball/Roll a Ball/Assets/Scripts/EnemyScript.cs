using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 2.8f;
    private GameObject player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");

        // lock y axis
        rb.constraints = RigidbodyConstraints.FreezePositionY;
    }

    // Update is called once per frame
    void Update()
    {
        // get the direction to the player
        Vector3 direction = player.transform.position - transform.position;

        direction.Normalize();

        if (Vector3.Distance(transform.position, player.transform.position) < 10)
        {
            rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        }
    }

}
