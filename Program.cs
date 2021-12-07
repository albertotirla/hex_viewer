using System;
using System.IO;
namespace hex_viewer
{
    class Program
    {
        static void Main(string[] args)
        {
            String file_name = "./main.rs";
            int number_of_bytes_per_line=10;
            using var input = new FileStream(file_name, FileMode.Open);

            int current_byte;
            int counter = 0;
int offset=0;
            while ((current_byte = input.ReadByte()) != -1)
            {

                Console.Write("{0:X2} ", current_byte);
                counter++;

                if (counter % number_of_bytes_per_line == 0)
                {
                    offset+=number_of_bytes_per_line;
                    Console.WriteLine("base+{0:X}", offset);
                }
            }

        }
    }
}
