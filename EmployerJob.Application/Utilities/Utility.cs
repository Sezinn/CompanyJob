using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployerJob.Application.Utilities
{
    public static class Utility
    {
        public static string GetExceptionTypeName(Exception e)
        {
            Type type = e.GetType();
            var exceptionTypeName = type?.Name;
            return exceptionTypeName;
        }

        public static string GetFileName(Exception e)
        {
            var strackTrace = new StackTrace(e, true);
            var lastFrame = strackTrace.GetFrame(0);
            if (lastFrame != null)
            {
                string csFileName = lastFrame.GetFileName()?.Split("\\")?.LastOrDefault();
                var lineNumber = lastFrame.GetFileLineNumber();
                var columnNumber = lastFrame.GetFileColumnNumber();
                return $"{csFileName}:{lineNumber}:{columnNumber}";
            }

            return "Can not reach the stack trace!";
        }
    }
}
