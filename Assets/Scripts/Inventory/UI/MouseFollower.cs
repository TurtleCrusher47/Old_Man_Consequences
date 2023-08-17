using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 

HOW TO USE THIS CLASS:
-Create a MouseFollower object in Canvas
-Add this script + content size fitter to game object
-content size fitter set to min/min


WHAT THIS CLASS DOES:
-Object tracks the mouse within the context of the inventory
-Tracks position and mouse clicks to be parsed into inventory controller
*/
public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private Camera mainCam;

    [SerializeField]
    private InventoryItem item;

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        mainCam = Camera.main;
        item = GetComponentInChildren<InventoryItem>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }

    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position);

        transform.position = canvas.transform.TransformPoint(position);
        
    }

    public void Toggle(bool val)
    {
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
