using System;
namespace XphenoApp.IService
{
    public interface IExceptionLogService
    {
        void LogException(Exception exception);
    }
}

