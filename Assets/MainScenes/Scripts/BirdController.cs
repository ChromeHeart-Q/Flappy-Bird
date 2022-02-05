using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private const float JUMP_FORCE = 100.0f;
    private Rigidbody2D birdRigidbody2D;
    public MapRender mapRender;
    Animator birdAnimator;
    private void Awake()
    {
        birdAnimator = GetComponent<Animator>();
        birdRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && mapRender.isPlaying)
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

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        mapRender.isPlaying = false;
        birdRigidbody2D.simulated = false;
        birdAnimator.SetTrigger("Death");
    }
}
