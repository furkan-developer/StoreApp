using Entities.Contract;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DataShaper<T> : IDataShaper<T>
        where T : IEntity, new()
    {
        private readonly PropertyInfo[] _propertyInfos;

        public DataShaper()
        {
            _propertyInfos = typeof(T).GetProperties();
        }

        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> resources, string fieldsPattern)
        {
            var requiredFields = ExtractRequiredPropertiesByQueryParameter(fieldsPattern);
            return FetchData(resources, requiredFields);
        }

        public ExpandoObject ShapeData(T resource, string fieldsPattern)
        {
            var requiredProperties = ExtractRequiredPropertiesByQueryParameter(fieldsPattern);
            return FetchDataForEntity(resource, requiredProperties);
        }

        // fields pattern Example: id,title,price
        private IEnumerable<PropertyInfo> ExtractRequiredPropertiesByQueryParameter(string fieldsPattern)
        {
            var requiredFields = new List<PropertyInfo>();
            if (!string.IsNullOrWhiteSpace(fieldsPattern))
            {
                var fields = fieldsPattern.Split(',',
                    StringSplitOptions.RemoveEmptyEntries);
                foreach (var field in fields)
                {
                    var property = _propertyInfos
                        .FirstOrDefault(pi => pi.Name.Equals(field.Trim(),
                        StringComparison.InvariantCultureIgnoreCase));
                    if (property is null)
                        continue;
                    requiredFields.Add(property);
                }
            }
            else
            {
                requiredFields = _propertyInfos.ToList();
            }

            return requiredFields;
        }

        private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new ExpandoObject();

            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.TryAdd(property.Name, objectPropertyValue);
            }
            return shapedObject;
        }

        private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ExpandoObject>();
            foreach (var entity in entities)
            {
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }
            return shapedData;
        }

    }
}
