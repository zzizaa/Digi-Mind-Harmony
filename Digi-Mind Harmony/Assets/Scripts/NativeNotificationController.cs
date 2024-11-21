using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NativeNotificationController : MonoBehaviour
{
    [SerializeField] private AndroidNotificationController androidNotificationController;
    

    private void Start()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        androidNotificationController.RequestAuthorization();
        androidNotificationController.RegisterNotificationChannel();
        //androidNotificationController.SendNotification("Test", "Notification from Unity App", 10);
        androidNotificationController.ScheduleRecurringNotifications("Stay Hydrated!", "Don't forget to drink water");
    }

    
}
