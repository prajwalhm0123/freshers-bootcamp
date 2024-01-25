using System;
using System.Threading;

namespace DoorEvent
{
    public class AutoCloser : IDoorSubscriber
    {
        private Timer autoCloseTimer;
        private IDoor door;

        public AutoCloser(IDoor door, int autoCloseTimeInSeconds)
        {
            this.door = door;
            autoCloseTimer = new Timer(AutoCloseCallback, null, autoCloseTimeInSeconds * 1000, Timeout.Infinite);
        }

        public void Notify(object sender, DoorStateChangedEventArgs e)
        {
            if (door.IsOpen())
            {
                door.Close();
                door.OnDoorStateChanged("The door is not shut.");
            }
        }

        private void AutoCloseCallback(object state)
        {
            Notify(this, new DoorStateChangedEventArgs("Auto close triggered."));
        }
    }

    public class BuzzerNotifier : IDoorSubscriber
    {
        public void Notify(object sender, DoorStateChangedEventArgs e)
        {
            Console.WriteLine($"Buzzer: {e.Message}");
        }
    }

    public class PagerNotifier : IDoorSubscriber
    {
        public void Notify(object sender, DoorStateChangedEventArgs e)
        {
            Console.WriteLine($"Pager: {e.Message}");
        }
    }

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

    public class Pager : IDoorSubscriber
    {
        public void Notify(object sender, DoorStateChangedEventArgs e)
        {
            Console.WriteLine($"Pager: {e.Message}");
        }
    }

    public interface IDoor
    {
        event DoorStateChangedEventHandler DoorStateChanged;

        void Open();
        void Close();
        bool IsOpen();
        void OnDoorStateChanged(string message);
    }

    public interface IDoorSubscriber
    {
        void Notify(object sender, DoorStateChangedEventArgs e);
    }

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

        public void OnDoorStateChanged(string message)
        {
            DoorStateChanged?.Invoke(this, new DoorStateChangedEventArgs(message));
        }
    }

    public class SmartDoor : SimpleDoor
    {
        private bool isBuzzerEnabled;
        private bool isPagerEnabled;
        private bool isAutoCloseEnabled;

        private BuzzerNotifier buzzerNotifier;
        private PagerNotifier pagerNotifier;
        private AutoCloser autoCloser;

        public SmartDoor(bool buzzer, bool pager, bool autoClose, int autoCloseTime)
        {
            isBuzzerEnabled = buzzer;
            isPagerEnabled = pager;
            isAutoCloseEnabled = autoClose;

            buzzerNotifier = isBuzzerEnabled ? new BuzzerNotifier() : null;
            pagerNotifier = isPagerEnabled ? new PagerNotifier() : null;
            autoCloser = isAutoCloseEnabled ? new AutoCloser(this, autoCloseTime) : null;
        }

        // Use 'new' to hide the base class method
        public new void OnDoorStateChanged(string message)
        {
            base.OnDoorStateChanged(message);

            buzzerNotifier?.Notify(this, new DoorStateChangedEventArgs(message));
            pagerNotifier?.Notify(this, new DoorStateChangedEventArgs(message));
            autoCloser?.Notify(this, new DoorStateChangedEventArgs(message));
        }
    }

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
            Console.WriteLine("simple door");
            IDoor simpleDoor = new SimpleDoor();
            simpleDoor.DoorStateChanged += new Operator().Notify;
            
            IDoor smartDoor = new SmartDoor(buzzer: true, pager: true, autoClose: true, autoCloseTime: 5);
            smartDoor.DoorStateChanged += new Operator().Notify;

            Operator operator1 = new Operator();

            operator1.OperateDoor(simpleDoor);
            Console.WriteLine("smart door");
            Console.WriteLine();

            operator1.OperateDoor(smartDoor);
            Console.ReadLine();
        }
    }
}
