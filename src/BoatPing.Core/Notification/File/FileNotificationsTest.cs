using System;
using System.IO;
using BoatPing.Core.Model;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Text;

namespace BoatPing.Core.Notification
{
    public sealed class FileNotificationsTest
    {
        [Fact]
        public void CreatesNotification()
        {
            using(var dir = new TempDirectory())
            {
                var path = dir.Value().FullName;
                var notifications =
                    new FileNotifications(path);

                var notification =
                    new NewBoatNotification(
                        new SimpleAd(
                            "123",
                            "xunit.test",
                            "http://www.google.de",
                            0.99,
                            new MapOf("", "")
                        )
                    );

                notifications.Post(notification);

                Assert.Equal(
                    new NewBoatText(notification.Ad()).AsString(),
                    File.ReadAllText(
                        Path.Combine(
                            path,
                            "notifications",
                            notification.Ad().Source(),
                            new NotificationFilename(notification).AsString()
                        )
                    )
                );
            }
        }
    }
}
