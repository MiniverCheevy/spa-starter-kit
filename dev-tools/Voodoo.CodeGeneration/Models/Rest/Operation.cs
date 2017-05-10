using System;
using System.Linq;

namespace Voodoo.CodeGeneration.Models.Rest
{
    [Serializable]
    public class Operation
    {
        public Type RequestType { get; set; }
        public Type ResponseType { get; set; }
        public Type OperationType { get; set; }
        public string RequestTypeName { get; set; }
        public string ResponseTypeName { get; set; }
        public string OperationTypeName { get; set; }

        public bool IsAsync { get; set; }

        public static Operation DiscoverTypes(Type operationType)
        {
            return DiscoverTypes(operationType, new Operation());
        }

        public static Operation DiscoverTypes(Type operationType, Operation operation)
        {
            var type = operationType;

            while (type.BaseType != null && type.GetGenericArguments().Count() != 2)
                type = type.BaseType;
            if (type.Namespace != null)
                operation.IsAsync = type.Namespace.Contains(".Async");

            var typeArguments = type.GetGenericArguments();

            if (typeArguments.Count() != 2)
                return null;

            operation.RequestType = typeArguments[0];
            operation.ResponseType = typeArguments[1];
            operation.OperationType = operationType;

            operation.RequestTypeName = typeArguments[0].FixUpTypeName();
            operation.ResponseTypeName = typeArguments[1].FixUpTypeName();
            operation.OperationTypeName = operationType.FixUpTypeName();

            return operation;
        }
    }
}