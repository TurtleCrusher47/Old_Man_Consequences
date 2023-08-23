using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpacebarSceneChanger : MonoBehaviour
{
    [SerializeField] string nextScene;
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Slider slider;

    private bool inCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0f;
    }

    void Update()
    {
        if (inCollider)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                slider.value += Time.deltaTime;

                if (slider.value >= slider.maxValue)
                {
                    SceneChanger.ChangeScene(nextScene);
                }
            }
            else if (slider.value > slider.minValue)
            {
                slider.value -= Time.deltaTime * 0.5f;
            }
        }
    }

    // When player enters the collision area, the UI panel appears
    // If the player holds space in that area for the required seconds, the scene switches
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            loadingPanel.SetActive(true);

            inCollider = true;
        }
    }

    // If the player leaves the area, reset the slider
    void OnTriggerExit2D(Collider2D collider)
    {
        inCollider = false;
        
        slider.value = 0f;
        loadingPanel.SetActive(false);
    }
}
