public interface IObserver
{
    void OnNotify(object value, NotificationType notificationType);
    void OnNotify(object value, object value2, NotificationType notificationType);
}
