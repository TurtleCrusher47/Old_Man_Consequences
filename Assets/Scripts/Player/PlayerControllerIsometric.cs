using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerIsometric : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    private IsometricCharacterRenderer isometricCharacterRenderer;
    private Rigidbody2D rb;
    Vector2 moveDirection;
    Vector2 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isometricCharacterRenderer = GetComponent<IsometricCharacterRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        moveDirection = Vector2.ClampMagnitude(moveDirection, 1);
    }

    void FixedUpdate()
    {
        Vector2 velocity = moveDirection * playerData.MovementSpeed;

        rb.velocity = velocity;

        // Update the sprite to show
        isometricCharacterRenderer.SetDirection(velocity);
    }
}
