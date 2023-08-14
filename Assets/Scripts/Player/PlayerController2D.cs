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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ar = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        // Change direction
        horizontal = Input.GetAxis("Horizontal");
        ar.SetFloat("Speed", Mathf.Abs(horizontal));

        // Flip the sprite
        Flip();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = new Vector2(horizontal * playerData.MovementSpeed, 0);
        rb.velocity = velocity;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= - 1f;
            transform.localScale = localScale;
        }
    }
}
