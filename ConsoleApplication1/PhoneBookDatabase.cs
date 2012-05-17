using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApplication1
{
	public class PhoneBookDatabase
	{
		Stream _stream;
		private byte[] _buffer;

		private IDictionary<string, long> posByName = new Dictionary<string, long>();

		public PhoneBookDatabase(Stream stream)
		{
			this._stream = stream;
			_buffer = new byte[50];
			BuildIndex();
		}

		private void BuildIndex()
		{
			_stream.Position = 0;
			while (_stream.Position < _stream.Length)
			{
				var pos = _stream.Position;
				var nameFromFile = ReadString();
				posByName[nameFromFile] = pos;
				_stream.Position += (50 + 4) * 2;
			}
		}

		public class PhoneNumber
		{
			public string Name;
			public string Type;
			public string Number;

			public override string ToString()
			{
				return string.Format("Name: {0}, Type: {1}, Number: {2}", Name, Type, Number);
			}
		}

		public void Save(PhoneNumber number)
		{
			var phoneNumber = Search(number.Name);
			if (phoneNumber == null)
				_stream.Position = _stream.Length;
			else
				_stream.Position -= RecordSize();

			posByName[number.Name] = _stream.Position;
			WriteString(number.Name);
			WriteString(number.Type);
			WriteString(number.Number);
		}

		private void WriteString(string str)
		{
			var bytes = Encoding.ASCII.GetBytes(str, 0, str.Length, _buffer, 0);

			_stream.Write(BitConverter.GetBytes(bytes), 0, 4);
			_stream.Write(_buffer, 0, _buffer.Length);
		}

		public PhoneNumber Search(string name)
		{
			_stream.Position = 0;
			long value;
			if (posByName.TryGetValue(name, out value) == false)
				return null;
			_stream.Position = value;
			return new PhoneNumber
			{
				Name = ReadString(),
				Type = ReadString(),
				Number = ReadString(),
			};
		}

		private static int RecordSize()
		{
			return (50 + 4)*3;
		}

		private string ReadString()
		{
			_stream.Read(_buffer, 0, 4);
			var len = BitConverter.ToInt32(_buffer, 0);
			_stream.Read(_buffer, 0, 50);
			var nameFromFile = Encoding.ASCII.GetString(_buffer, 0, len);
			return nameFromFile;
		}
	}
}