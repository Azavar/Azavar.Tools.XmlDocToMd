using System;

namespace Azavar.Tools.XmlDocToMd.Model
{
    public class MethodParameter
    {
        public MethodParameter(Method method,string typeName)
        {
            if (typeName == null)
                throw new ArgumentNullException();
            Method = method;
            TypeName = typeName;
        }

        public string Name { get; set; }
        public Method Method { get; set; }
        public string TypeName { get; set; }
        public Documentation Documentation { get; set; }
    }
}
