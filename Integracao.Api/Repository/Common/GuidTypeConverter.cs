using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;

namespace Integracao.Api.Repository.Common
{
    public class GuidTypeConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            var primitive = entry as Primitive;

            if (primitive == null || !(primitive.Value is String) ||
                string.IsNullOrEmpty((string)primitive.Value) || !Guid.TryParse((string)primitive.Value, out Guid guidValue))
                throw new ArgumentOutOfRangeException();

            return guidValue;
        }

        public DynamoDBEntry ToEntry(object value)
        {
            if (value == null || !Guid.TryParse(value.ToString(), out Guid guid))
                throw new ArgumentOutOfRangeException();

            DynamoDBEntry entry = new Primitive
            {
                Value = guid.ToString()
            };

            return entry;
        }
    }
}