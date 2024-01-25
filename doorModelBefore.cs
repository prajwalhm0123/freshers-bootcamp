using System;

namespace DoorEvent
{

    public class DoorStateChangedEventArgs : EventArgs
    {
        public string Message { get; set; }

        public DoorStateChangedEventArgs(string message)
        {
            Message = message;
        }
    }
    
    public delegate void DoorStateChangedEventHandler(object sender, DoorStateChangedEventArgs e);



    class Program
    {
        static void Main(string[] args)
        {
            IDoor simpleDoor = new SimpleDoor();
            simpleDoor.DoorStateChanged += new Operator().Notify;

            IDoor smartDoor = new SmartDoor(buzzer: true, pager: true, autoClose: true, autoCloseTime: 5);
            smartDoor.DoorStateChanged += new Operator().Notify;
            smartDoor.DoorStateChanged += new Buzzer().Notify;
            smartDoor.DoorStateChanged += new Pager().Notify;
            smartDoor.DoorStateChanged += new AutoClose().Notify;

            Operator operator1 = new Operator();

            operator1.OperateDoor(simpleDoor);

            Console.WriteLine();

            operator1.OperateDoor(smartDoor);
            Console.ReadLine();
        }
    }
}
using System;

namespace DoorEvent
{
    public class AutoClose : IDoorSubscriber
    {
        public void Notify(object sender, DoorStateChangedEventArgs e)
        {
            Console.WriteLine($"AutoClose: {e.Message}");
        }
    }
}
using System;

namespace DoorEvent
{
    public class Buzzer : IDoorSubscriber
    {
        public void Notify(object sender, DoorStateChangedEventArgs e)
        {
            Console.WriteLine($"Buzzer: {e.Message}");
        }
    }
}
namespace DoorEvent
{
    public interface IDoor
    {
        event DoorStateChangedEventHandler DoorStateChanged;

        void Open();
        void Close();
    }
}
namespace DoorEvent
{
    public interface IDoorSubscriber
    {
        void Notify(object sender, DoorStateChangedEventArgs e);

    }
}
using System;
using System.Threading;

namespace DoorEvent
{
    public class Operator : IDoorSubscriber
    {
        public void Notify(object sender, DoorStateChangedEventArgs e)
        {
            Console.WriteLine($"Operator received notification: {e.Message}");
        }

        public void OperateDoor(IDoor door)
        {
            door.Open();
            Thread.Sleep(2000); 
            door.Close();
        }
    }

}
using System;

namespace DoorEvent
{
    public class Pager : IDoorSubscriber
    {
        public void Notify(object sender, DoorStateChangedEventArgs e)
        {
            Console.WriteLine($"Pager: {e.Message}");
        }
    }
}
namespace DoorEvent
{
    public class SimpleDoor : IDoor
    {
        private bool isOpen;

        public SimpleDoor()
        {
            isOpen = false;
        }
        public event DoorStateChangedEventHandler DoorStateChanged;

        public void Open()
        {
            isOpen = true;
            OnDoorStateChanged("Door is opened.");
        }

        public void Close()
        {
            isOpen = false;
            OnDoorStateChanged("Door is closed.");
        }

        public bool IsOpen()
        {
            return isOpen;
        }
        protected virtual void OnDoorStateChanged(string message)
        {
            DoorStateChanged?.Invoke(this, new DoorStateChangedEventArgs(message));
        }
    }
}
using System.Threading;

namespace DoorEvent
{
    public class SmartDoor : SimpleDoor
    {
        private bool isBuzzerEnabled;
        private bool isPagerEnabled;
        private bool isAutoCloseEnabled;
        private int autoCloseTimeInSeconds;
        private Timer autoCloseTimer;


        public SmartDoor(bool buzzer, bool pager, bool autoClose, int autoCloseTime)
        {
            isBuzzerEnabled = buzzer;
            isPagerEnabled = pager;
            isAutoCloseEnabled = autoClose;
            autoCloseTimeInSeconds = autoCloseTime;
        }

        public new void Open()
        {
            base.Open();

            if (isAutoCloseEnabled)
            {
                autoCloseTimer = new Timer(AutoCloseCallback, null, autoCloseTimeInSeconds * 1000, Timeout.Infinite);
            }
        }

        public new void Close()
        {
            base.Close();

            if (isAutoCloseEnabled)
            {
                autoCloseTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        private void AutoCloseCallback(object state)
        {
            if (IsOpen())
            {
                Close();
                OnDoorStateChanged("The door is not shut.");
            }
        }

    }
}
