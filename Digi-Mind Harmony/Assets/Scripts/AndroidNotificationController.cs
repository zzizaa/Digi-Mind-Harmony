using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;

public class AndroidNotificationController : MonoBehaviour
{
    [SerializeField] private int numberOfNotifications = 10;
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

    public void ScheduleRecurringNotifications(string title, string text)
    {
        for (int i = 0; i < numberOfNotifications; i++)
        {
            var notification = new AndroidNotification()
            {
                Title = title,
                Text = text,
                FireTime = System.DateTime.Now.AddSeconds(10 * i),
            };

            AndroidNotificationCenter.SendNotification(notification, "default_channel");
        }
    }
}
