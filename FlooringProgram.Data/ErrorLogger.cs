using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlooringProgram.Data
{
    public class ErrorLogger
    {
        private string _filePath = @"ErrorLog/ErrorLog.txt";
        private DateTime date = new DateTime();
        
        public void WriteNewLine(string ErrorMessage)
        {
            date = DateTime.Now;
            using (var writer = File.AppendText(_filePath))
            {
                writer.WriteLine("{0} - {1}", date, ErrorMessage);
            }

        }
    }
}
