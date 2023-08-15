using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpacebarSceneChanger : MonoBehaviour
{
    [SerializeField] string nextScene;
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0f;
    }

    // When player enters the collision area, the UI panel appears
    // If the player holds space in that area for the required seconds, the scene switches
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            loadingPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                slider.value += Time.deltaTime;

                if (slider.value >= slider.maxValue)
                {
                    SceneChanger.ChangeScene(nextScene);
                }
            }
        }
    }

    // If the player leaves the area, reset the slider
    void OnTriggerExit2D(Collider2D collider)
    {
        slider.value = 0f;
        loadingPanel.SetActive(false);
    }
}
