using System;
using System.IO;
using System.Text;

namespace hex_viewer
{
    class Program
    {
        static void Main(string[] args)
        {
            String file_name = "./main.rs";
            int number_of_bytes_per_line = 10;
            int current_byte;
            int counter = 0;
            int offset = 0;
            StringBuilder ascii_text = new StringBuilder();
            StringBuilder hex_bytes = new StringBuilder();
            using var input = new FileStream(file_name, FileMode.Open);

            Console.WriteLine("offset | bytes | ascii");
            while ((current_byte = input.ReadByte()) != -1)
            {
                //Console.Write("{0:X2} ", current_byte);
                counter++;
                ascii_text.Append((char)current_byte);
                hex_bytes.Append(String.Format("0x{0:X2} ", current_byte));
                if (counter % number_of_bytes_per_line == 0)
                {
                    offset += number_of_bytes_per_line;
                    Console.WriteLine();
                    Console.Write("base+0x{0:X}", offset);
                    Console.Write(" | ");
                    Console.Write(hex_bytes.ToString());
                    Console.Write("|");
                    Console.Write(ascii_text.ToString());
                    ascii_text.Clear();
                    hex_bytes.Clear();
                }
                //Console.Write("{0:X2} ", current_byte);
            }

        }
    }
}
