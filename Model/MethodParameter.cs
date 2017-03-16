using System;

namespace Azavar.Tools.XmlDocToMd.Model
{
    /// <summary>
    /// Represents a method parameter.
    /// </summary>
    public class MethodParameter
    {
        /// <summary>
        /// Initializes a method parameter with its parent method and its type ID.
        /// </summary>
        /// <param name="method">The parent method.</param>
        /// <param name="typeId">The ID of the type as it appears in the method ID.</param>
        /// <exception cref="ArgumentNullException">Either parameter is null.</exception>
        public MethodParameter(Method method, string typeId)
        {
            if(method == null)
                throw new ArgumentNullException(nameof(method));
            if (typeId == null)
                throw new ArgumentNullException(nameof(typeId));
            Method = method;
            TypeId = typeId;
        }

        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets the parent <see cref="Model.Method"/>.
        /// </summary>
        public Method Method { get; }
        /// <summary>
        /// Gets the ID of the type as it appears in the method ID.
        /// </summary>
        public string TypeId { get; }
        /// <summary>
        /// Gets or sets the <see cref="Model.Documentation"/> object associated with the parameter.
        /// </summary>
        public Documentation Documentation { get; set; }
    }
}
