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

    public class Printer : IPrinter
    {
        public void Print(string path)
        {
            Console.WriteLine($"Printing .....{path}");
        }
    }

    public class Scanner : IScanner
    {
        public void Scan(string path)
        {
            Console.WriteLine($"Scanning .....{path}");
        }
    }

    public class PrintScanner : IPrinter, IScanner
    {
        public void Print(string path)
        {
            Console.WriteLine($"Printing .....{path}");
        }

        public void Scan(string path)
        {
            Console.WriteLine($"Scanning .....{path}");
        }
    }

    public static class TaskManager
    {
        public static void ExecutePrintTask(IPrinter printer, string documentPath)
        {
            printer.Print(documentPath);
        }

        public static void ExecuteScanTask(IScanner scanner, string documentPath)
        {
            scanner.Scan(documentPath);
        }
    }

    public class Program
    {
        static void Main()
        {
            Printer printerObj = new Printer();
            Scanner scannerObj = new Scanner();
            PrintScanner printScannerObj = new PrintScanner();

            TaskManager.ExecutePrintTask(printerObj, "Test.doc");
            TaskManager.ExecuteScanTask(scannerObj, "MyImage.png");

            TaskManager.ExecutePrintTask(printScannerObj, "NewDoc.doc");
            TaskManager.ExecuteScanTask(printScannerObj, "YourImage.png");
            Console.ReadLine();
        }
    }
}
