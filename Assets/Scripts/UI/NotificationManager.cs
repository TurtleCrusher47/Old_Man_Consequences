using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image image;

     //public NotificationSO[] notificationList;
    [SerializeField] private List<NotificationStruct> notificationList;
    private Dictionary<string, Notification> notifications;

    [Serializable] public struct NotificationStruct
    {
        public string notificationKey;
        public Notification notification;
    }

    void Awake()
    {

        notifications = new Dictionary<string, Notification>();
        for (int i = 0; i < notificationList.Count; i++)
        {
            notifications.Add(notificationList[i].notificationKey, notificationList[i].notification);
        }

        panel.SetActive(false);
    }

    public void LoadNotification(string notificationKey)
    {
        text.text = notifications[notificationKey].Message;
        image.sprite = notifications[notificationKey].Sprite;
    }

    public IEnumerator ShowNotification(string currentKey)
    {
        LoadNotification(currentKey);
        panel.SetActive(true);
        // Play animation
        yield return new WaitForSeconds(5);
        //Play reverse anim
        yield return new WaitForSeconds(5);
        panel.SetActive(false);
    }
}
