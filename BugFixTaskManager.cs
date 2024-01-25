using System;

namespace codeBugFix1
{
    public interface IPrinter
    {
        void Print(string path);
    }

    public interface IScanner
    {
        void Scan(string path);
    }

    public class Device
    {
        public void Operation(string operationType, string path)
        {
            Console.WriteLine($"{operationType} .....{path}");
        }
    }

    public class Printer : IPrinter
    {
        private readonly Device device;

        public Printer(Device device)
        {
            this.device = device;
        }

        public void Print(string path) => device.Operation("Printing", path);
    }

    public class Scanner : IScanner
    {
        private readonly Device device;

        public Scanner(Device device)
        {
            this.device = device;
        }

        public void Scan(string path) => device.Operation("Scanning", path);
    }

    public class PrintScanner : IPrinter, IScanner
    {
        private readonly Device device;

        public PrintScanner(Device device)
        {
            this.device = device;
        }

        public void Print(string path) => device.Operation("Printing", path);

        public void Scan(string path) => device.Operation("Scanning", path);
    }

    public static class TaskManager
    {
        public static void ExecutePrintTask(IPrinter printer, string documentPath) => printer.Print(documentPath);

        public static void ExecuteScanTask(IScanner scanner, string documentPath) => scanner.Scan(documentPath);
    }

    public class Program
    {
        static void Main()
        {
            Device commonDevice = new Device();

            Printer printerObj = new Printer(commonDevice);
            Scanner scannerObj = new Scanner(commonDevice);
            PrintScanner printScannerObj = new PrintScanner(commonDevice);

            TaskManager.ExecutePrintTask(printerObj, "Test.doc");
            TaskManager.ExecuteScanTask(scannerObj, "MyImage.png");

            TaskManager.ExecutePrintTask(printScannerObj, "NewDoc.doc");
            TaskManager.ExecuteScanTask(printScannerObj, "YourImage.png");
        }
    }
}
