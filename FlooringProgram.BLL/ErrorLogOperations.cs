using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data;

namespace FlooringProgram.BLL
{
    public static class ErrorLogOperations
    {
        public static void LogError(string ErrorMessage)
        {
            ErrorLogger _errorLogger = new ErrorLogger();
            _errorLogger.WriteNewLine(ErrorMessage);
        }
    }
}
