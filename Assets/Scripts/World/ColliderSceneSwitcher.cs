using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSceneSwitcher : MonoBehaviour
{
    [SerializeField] string nextScene;
    public Vector2 playerPosition;
    public VectorValue playerStoredPosition;
    public SceneTransitionChange sceneChanger;
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerStoredPosition.initialValue = playerPosition;
            sceneChanger.LoadScene(nextScene);
        }
    }
}
