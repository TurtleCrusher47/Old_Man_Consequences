using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image image;

    //public NotificationSO[] notificationList;
    [SerializeField] Dictionary<string, Notification> notification;

    public void LoadNotification(string notificationToSend)
    {
        text.text = notification[notificationToSend].Message;
        image.sprite = notification[notificationToSend].Sprite;
    }
}
