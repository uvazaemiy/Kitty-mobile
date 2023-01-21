using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotificationSamples;
using System;

public class Notifications : MonoBehaviour
{
    [SerializeField] private GameNotificationsManager NotificationsManager;
    [SerializeField] private ProgressBar[] ProgressBars;

    private string[] NotiData = { "Я вже хочу їсти!", "Коли мы будемо гратися?", "Я дуже хочу спати!", "Менi потрiбно до вбиральнi!", "Заходь у Дивинку і погодуй свого котика", "Твій котик сумує без тебе, пограйся з ним!", "Котику треба поспати, заходь у Дивинку і поклади його в ліжечко.", "Котик давно не ходив у туалет, заходь у Дивинку та допоможи йому." };

    private void Start()
    {
        GameNotificationChannel channel = new GameNotificationChannel("need", "Уведомления котика", "Эти уведомления помогут вам знать, когда котик в беде.");
        NotificationsManager.Initialize(channel);
        InitNotifications();
    }

    public void InitNotifications()
    {
        NotificationsManager.CancelAllNotifications();
        for (int i = 0; i < ProgressBars.Length; i++)
            SendNotification(i, ProgressBars[i]._increaseTime - ProgressBars[i]._increaseTime * ProgressBars[i].Fill);
    }

    private void SendNotification(int n, float time)
    {
        if (time == 0)
            return;
        IGameNotification notification = NotificationsManager.CreateNotification();
        if (notification != null)
        {
            notification.Title = NotiData[n];
            notification.Body = NotiData[n + 4];
            notification.SmallIcon = "small_icon";
            notification.LargeIcon = "large_icon";
            notification.DeliveryTime = DateTime.Now.AddSeconds(time);
        }
        NotificationsManager.ScheduleNotification(notification);
    }
}
