using Com.Melon.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Com.Melon.Core.Infrastructure
{
    public class ModelBuilderAdapter : IModelBuilderAdapter
    {
        private readonly ModelBuilder _modelBuilder;

        private readonly ISystemDataTypeBuilder _systemDataTypeBuilder;

        public ModelBuilderAdapter(ModelBuilder modelBuilder, ISystemDataTypeBuilder systemDataTypeBuilder)
        {
            _modelBuilder = modelBuilder;
            _systemDataTypeBuilder = systemDataTypeBuilder;
        }

        public ModelBuilder GetModelBuilder()
        {
            return _modelBuilder;
        }

        public EntityTypeBuilder<T> AddEntity<T>() where T : Entity<T>
        {
            var entityType = typeof(T);
            var entityBaseType = typeof(Entity<>);
            if (!entityBaseType.IsAssignableFrom(entityType))
            {
                throw new ArgumentException("${T} should be a type derived from Entity<T>.");
            }
            var result = _modelBuilder.Entity<T>();
            foreach(var propertyInfo in entityType.GetProperties())
            {
                ISystemDataType systemDataType = _systemDataTypeBuilder.Build(propertyInfo.PropertyType);
                if (systemDataType.CanBeStoredInDbDirectly())
                {
                    result.Property(propertyInfo.Name);
                }
            }

            return result;
        }

        public EntityTypeBuilder<T> AddValueObject<T>() where T : IdentifiedValueObject<T>
        {
            throw new System.NotImplementedException();
        }
    }
}
