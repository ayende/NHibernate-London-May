using System;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var file = File.Open("phone.bin", FileMode.OpenOrCreate))
			{
				var phoneBookDatabase = new PhoneBookDatabase(file);
				//phoneBookDatabase.Save(new PhoneBookDatabase.PhoneNumber
				//{
				//    Name = "Oren",
				//    Number = "12345",
				//    Type = "Home"
				//});
				//phoneBookDatabase.Save(new PhoneBookDatabase.PhoneNumber
				//{
				//    Name = "Alon",
				//    Number = "12312",
				//    Type = "work"
				//});
				var phoneNumber = phoneBookDatabase.Search("Alon");

				Console.WriteLine(phoneNumber);

				file.Flush();
			}
		}
	}
}
