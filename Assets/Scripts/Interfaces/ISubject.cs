using System.Collections.Generic;

public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void Notify(object value, NotificationType notificationType);
}
