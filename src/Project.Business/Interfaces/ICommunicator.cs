using Project.Business.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Interfaces
{
    public interface ICommunicator
    {
        bool HasNotifications();
        List<Notification> GetNotification();
        void Handle(Notification notification);

    }
}
