using System.Linq;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo;

namespace Voodoo.CodeGeneration.Helpers
{
    public class TypeComparer
    {
        public TypeComparer(TypeFacade left, TypeFacade right)
        {
            LeftType = left;
            RightType = right;
            var commonProperties =
                left.Properties.Where(
                        c =>
                            right.Properties.Any(
                                p => p.Name == c.Name && p.PropertyType == c.PropertyType && p.IsWritable && c.IsWritable))
                    .ToArray();
            ScalarProperties = commonProperties.Where(c => c.PropertyType.IsScalar()).ToArray();
            ScalarPropertiesWithoutId = ScalarProperties.Where(c => c.Name != "Id").ToArray();
            CollectionProperties =
                commonProperties.Where(c => c.PropertyType.IsEnumerable()).Except(ScalarProperties).ToArray();
            ComplexProperties = commonProperties.Except(ScalarProperties).Except(CollectionProperties).ToArray();
        }

        public TypeFacade LeftType { get; set; }
        public TypeFacade RightType { get; set; }
        public PropertyFacade[] ScalarProperties { get; set; }
        public PropertyFacade[] CollectionProperties { get; set; }
        public PropertyFacade[] ComplexProperties { get; set; }
        public PropertyFacade[] ScalarPropertiesWithoutId { get; set; }
    }
}