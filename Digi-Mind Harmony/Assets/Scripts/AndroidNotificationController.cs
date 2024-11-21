using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;

public class AndroidNotificationController : MonoBehaviour
{
    public void RequestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
            {
                Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
            }
    }

    public void RegisterNotificationChannel()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(string title, string text, int fireTimeInSeconds)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = System.DateTime.Now.AddSeconds(fireTimeInSeconds);

        AndroidNotificationCenter.SendNotification(notification, "default_channel");
    }

    public void ScheduleRecurringNotifications(string title, string text, int time, int numberOfNotifications)
    {
        for (int i = 0; i < numberOfNotifications; i++)
        {
            var notification = new AndroidNotification()
            {
                Title = title,
                Text = text,
                FireTime = System.DateTime.Now.AddSeconds(time * i),
            };

            AndroidNotificationCenter.SendNotification(notification, "default_channel");
        }
    }

    public void ScheduleDailyNotification(string title, string text, int hour, int minute)
    {
        DateTime now = DateTime.Now;
        DateTime scheduledTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);

        if (scheduledTime < now)
        {
            scheduledTime = scheduledTime.AddDays(1);
        }

        // Configura la notifica
        var notification = new AndroidNotification()
        {
            Title = title,
            Text = text,
            FireTime = scheduledTime,
            RepeatInterval = TimeSpan.FromDays(1) 
        };

        // Invia la notifica
        AndroidNotificationCenter.SendNotification(notification, "default_channel");

        Debug.Log($"Notifica pianificata per: {scheduledTime}");
    }
}
