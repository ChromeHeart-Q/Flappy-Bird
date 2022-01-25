using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private const float JUMP_FORCE = 100.0f;
    private Rigidbody2D birdRigidbody2D;

    private void Awake()
    {
        birdRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            BirdJump();
        }
    }

    private void BirdJump()
    {
        birdRigidbody2D.velocity = Vector2.up * JUMP_FORCE;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Score Zone")
        {
            UIManager.Instance.AddScore();
        }
        else
        {
            UIManager.Instance.MinusScore();
        }

    }
}
