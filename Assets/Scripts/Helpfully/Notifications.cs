using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotificationSamples;
using System;

public class Notifications : MonoBehaviour
{
    [SerializeField] private GameNotificationsManager NotificationsManager;
    [SerializeField] private ProgressBar[] ProgressBars;

    private string[] NotiData = { "� ��� ���� ����!", "���� �� ������ �������?", "� ���� ���� �����!", "���i ����i��� �� ��������i!", "������ � ������� � ������� ����� ������", "��� ����� ���� ��� ����, �������� � ���!", "������ ����� �������, ������ � ������� � ������� ���� � ������.", "����� ����� �� ����� � ������, ������ � ������� �� �������� ����." };

    private void Start()
    {
        GameNotificationChannel channel = new GameNotificationChannel("need", "����������� ������", "��� ����������� ������� ��� �����, ����� ����� � ����.");
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
