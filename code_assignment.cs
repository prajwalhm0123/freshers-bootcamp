using System;
using System.Collections.Generic;

class Device
{
    public string Id { get; set; }
    public int Code { get; set; }
    public string Description { get; set; }
}

class ObjectValidator
{
    public static bool Validate(Device device, out List<string> errors)
    {
        errors = new List<string>();

        if (string.IsNullOrWhiteSpace(device.Id))
            errors.Add("ID Property Requires Value");

        if (device.Code < 10 || device.Code > 100)
            errors.Add("Code Value Must Be Within 10-100");

        if (device.Description?.Length > 100)
            errors.Add("Max of 100 Characters are allowed for Description");

        return errors.Count == 0;
    }
}

class Program
{
    static void Main()
    {
        Device deviceObj = new Device();

        // Get input from the user
        Console.Write("Enter Device ID: ");
        deviceObj.Id = Console.ReadLine();

        Console.Write("Enter Device Code (between 10 and 100): ");
        deviceObj.Code = int.TryParse(Console.ReadLine(), out int code) ? code : 0;

        Console.Write("Enter Device Description (max 100 characters): ");
        deviceObj.Description = Console.ReadLine();

        List<string> errors;

        bool isValid = ObjectValidator.Validate(deviceObj, out errors);

        if (!isValid)
            errors.ForEach(Console.WriteLine);
        else
            DisplayDeviceInfo(deviceObj);
    }

    static void DisplayDeviceInfo(Device device)
    {
        Console.WriteLine("Validation successful. Device details:");
        Console.WriteLine($"ID: {device.Id}");
        Console.WriteLine($"Code: {device.Code}");
        Console.WriteLine($"Description: {device.Description}");
    }
}
