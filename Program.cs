using System;
using System.IO;
using System.Text;
using System.Diagnostics;
namespace hex_viewer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine($"usage:{Process.GetCurrentProcess().ProcessName} < path > < bytes per line>\narguments:\n*path: a path on the filesystem where the file can be read from\n*bytes per line: the maximum length of a displayed line\n\tFatal: not enough arguments provided, shutting down");
            Environment.Exit(-1);
            }
            String file_name = args[0];
            int number_of_bytes_per_line = int.Parse(args[1]);

            int current_byte;
            int counter = 0;
            int offset = 0;
            StringBuilder ascii_text = new StringBuilder();
            StringBuilder hex_bytes = new StringBuilder();
            using var input = new FileStream(file_name, FileMode.Open);
            Console.WriteLine("offset | bytes | ascii");
            while ((current_byte = input.ReadByte()) != -1)
            {
                counter++;
                ascii_text.Append((char)current_byte);
                hex_bytes.Append(String.Format("0x{0:X2} ", current_byte));
                if (counter % number_of_bytes_per_line == 0)
                {

                    Console.WriteLine();
                    Console.Write("base+0x{0:X}", offset);
                    Console.Write(" | ");
                    Console.Write(hex_bytes.ToString());
                    Console.Write("|");
                    Console.Write(ascii_text.ToString());
                    ascii_text.Clear();
                    hex_bytes.Clear();
                    offset += number_of_bytes_per_line;
                }
                //Console.Write("{0:X2} ", current_byte);
            }
            if (counter % 10 != 0)
            {
                Console.WriteLine();
                Console.Write("base+0x{0:X}", offset);
                Console.Write(" | ");
                Console.Write(hex_bytes.ToString());
                Console.Write("|");
                Console.Write(ascii_text.ToString());
            }
        }
    }
}
