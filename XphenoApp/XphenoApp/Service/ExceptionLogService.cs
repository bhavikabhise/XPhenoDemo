using System;
using XphenoApp.IService;

namespace XphenoApp.Service
{
    public class ExceptionLogService : IExceptionLogService
    {
        public void LogException(Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}

