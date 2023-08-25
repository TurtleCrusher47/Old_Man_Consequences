using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    private float horizontal;
    private bool isFacingRight = true;
    private Rigidbody2D rb;
    private SpriteRenderer sr;  
    private Animator ar;
    private IsoPlayerSoundController isoPlayerSoundController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ar = GetComponent<Animator>();
        isoPlayerSoundController = GetComponent<IsoPlayerSoundController>();
        ar.SetBool("FacingRight", isFacingRight);
        ar.SetBool("FacingLeft", !isFacingRight);
    }

    // Update is called once per frame
    void Update()
    {
        // Change direction
        horizontal = Input.GetAxis("Horizontal");

        // Flip the sprite
        Flip();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = new Vector2(horizontal * playerData.MovementSpeed, rb.velocity.y);
        rb.velocity = velocity;

        if (rb.velocity.magnitude >= 0.1f)
        {
            ar.SetBool("Walking", true);
            isoPlayerSoundController.PlaySound(0);
        }
        else
        {
            ar.SetBool("Walking", false);
            isoPlayerSoundController.PauseSound();
        }
        
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            // Vector3 localScale = transform.localScale;
            // localScale.x *= - 1f;
            // transform.localScale = localScale;

            ar.SetBool("FacingRight", isFacingRight);
            ar.SetBool("FacingLeft", !isFacingRight);
        }
    }
}
