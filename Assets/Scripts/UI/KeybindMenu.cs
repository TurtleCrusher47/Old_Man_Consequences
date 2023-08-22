using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeybindMenu : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    [Header("Keys")]
    [SerializeField] private TMP_Text up, down, left, right, interact;

    [Header("Menu Elements")]
    [SerializeField] private GameObject keybindMenu;

    private GameObject currKey;

    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Up", (KeyCode)PlayerPrefs.GetInt("Up", (int)KeyCode.W));
        keys.Add("Down", (KeyCode)PlayerPrefs.GetInt("Down", (int)KeyCode.S));
        keys.Add("Left", (KeyCode)PlayerPrefs.GetInt("Left", (int)KeyCode.A));
        keys.Add("Right", (KeyCode)PlayerPrefs.GetInt("Right", (int)KeyCode.D));
        keys.Add("Interact", (KeyCode)PlayerPrefs.GetInt("Interact", (int)KeyCode.E));

        up.SetText(keys["Up"].ToString());
        down.SetText(keys["Down"].ToString());
        left.SetText(keys["Left"].ToString());
        right.SetText(keys["Right"].ToString());
        interact.SetText(keys["Interact"].ToString());

        // Print dictionary contents
        /*
        foreach (var keyValuePair in keys)
        {
            Debug.Log("Key: " + keyValuePair.Key + " - KeyCode: " + keyValuePair.Value);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keys["Up"]))
        {
            Debug.Log("Up");
        }
        if (Input.GetKeyDown(keys["Down"]))
        {
            Debug.Log("Down");
        }
        if (Input.GetKeyDown(keys["Left"]))
        {
            Debug.Log("Left");
        }
        if (Input.GetKeyDown(keys["Right"]))
        {
            Debug.Log("Right");
        }
        if (Input.GetKeyDown(keys["Interact"]))
        {
            Debug.Log("Interact");
        }
    }

    private void OnGUI()
    {
        if (currKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currKey.name] = e.keyCode;
                TMP_Text keyText = currKey.transform.GetChild(0).GetComponent<TMP_Text>();
                keyText.text = e.keyCode.ToString();
                PlayerPrefs.SetString(currKey.name, e.keyCode.ToString()); // Save the key change
                currKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currKey = clicked;
        TMP_Text keyText = currKey.transform.GetChild(0).GetComponent<TMP_Text>();
        keyText.text = "Press a Key...";
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetInt(key.Key, (int)key.Value);
        }

        PlayerPrefs.Save();
        Debug.Log("Keybinds Saved");
    }
}
