using System;
//for getting the name of the running program
using System.Diagnostics;
//for the file stream
using System.IO;
//for StringBuilder
using System.Text;
namespace hex_viewer
{
    class Program
    {
        static void Main(string[] args)
        {
            //input is given through the command line, so we check it's correct
            if (args.Length < 2)
            {
                //if not, print a lengthy and verbose error message, then exit with -1 error code
                Console.WriteLine($"usage:{Process.GetCurrentProcess().ProcessName} < path > < bytes per line>\narguments:\n*path: a path on the filesystem where the file can be read from\n*bytes per line: the maximum length of a displayed line\n\tFatal: not enough arguments provided, shutting down");
                Environment.Exit(-1);
            }
            //If we arrived to this point, command line arguments are what we expect. In that case, populate the variables for user input with them
            String file_name = args[0];
            int number_of_bytes_per_line = int.Parse(args[1]);
            //declare other variables
            int current_byte;
            int counter = 0;
            int offset = 0;
            StringBuilder ascii_text = new StringBuilder();
            StringBuilder hex_bytes = new StringBuilder();
            //initialise a file stream to read the content of the file given as path. Exceptions are not handled for now, planned for future versions
            using var input = new FileStream(file_name, FileMode.Open);
            //print the header of the table, three columns, offset, hex bytes, ascii representation
            Console.WriteLine("offset | bytes | ascii");
            //read one byte at a time from the file stream
            while ((current_byte = input.ReadByte()) != -1)
            {
                //if we still have bytes in the file, increment the counter, append the ascii and hex formatted version of the byte to their corresponding buffers.
                counter++;
                ascii_text.Append((char)current_byte);
                hex_bytes.Append(String.Format("0x{0:X2} ", current_byte));
                //did we already read number of bytes per line?
                if (counter % number_of_bytes_per_line == 0)
                {
                    //if so, make a new table row with the results
                    PrintNewTableRow(offset, ascii_text, hex_bytes);
                    //increment the offset so that it represents the beginning of the next chunk of bytes
                    offset += number_of_bytes_per_line;
                }
            }
            //what if the loop ended because we arrived to the end of the file, however the file size isn't a multiple of max bytes per line?
            if (counter % 10 != 0)
            {
                //in that case, the bytes would still be written in their corresponding buffers, the previous loop just didn't have the chance to display them. So, we just make a new table row and print them now
                PrintNewTableRow(offset, ascii_text, hex_bytes);
            }
        }

        //the function that prints a table row. It constructs the columns from the parameters given to it, using the | character as the field separator
        private static void PrintNewTableRow(int offset, StringBuilder text, StringBuilder hex)
        {
            //make a new line, to not write over the previous row
            Console.WriteLine();
            //write the fields, with separators
            Console.Write("0x{0:X}", offset);
            Console.Write(" | ");
            Console.Write(hex.ToString());
            Console.Write("|");
            Console.Write(text.ToString());
            //empty out the buffers, otherwise we would have duplicated data displayed all over the place
            text.Clear();
            hex.Clear();
        }
    }
}
