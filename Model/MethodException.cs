using System;

namespace Azavar.Tools.XmlDocToMd.Model
{
    public class MethodException
    {
        public MethodException(string exceptionClass)
        {
            if (exceptionClass == null)
                throw new ArgumentNullException();
            ExceptionClass = exceptionClass;
        }
        public string ExceptionClass { get; set; }
        public Documentation Documentation { get; set; }
    }
}