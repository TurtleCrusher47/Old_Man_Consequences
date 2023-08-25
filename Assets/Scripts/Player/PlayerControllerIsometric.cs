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
    public VectorValue startingPosition;
    private IsoPlayerSoundController isoSoundController;
    private int footStepsIndex;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isometricCharacterRenderer = GetComponent<IsometricCharacterRenderer>();
        isoSoundController = GetComponent<IsoPlayerSoundController>();
        transform.position = startingPosition.initialValue;
        footStepsIndex = 0;
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
        
        if (velocity.magnitude > 0)
        {
            if (!isoSoundController.IsPlaying())
            {
                Debug.Log(footStepsIndex);
                isoSoundController.PlaySound(footStepsIndex);
            }
        }
        else
        {
            isoSoundController.PauseSound();
        }

        // Update the sprite to show
        isometricCharacterRenderer.SetDirection(velocity);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Grass toucher");
        if (collision.gameObject.CompareTag("Grass"))
        {
            footStepsIndex = 1;
            isoSoundController.PauseSound();
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    //private void OnCollisionExit2D(Collision2D collision)
    {
        footStepsIndex = 0;
        isoSoundController.PauseSound();
    }
}
