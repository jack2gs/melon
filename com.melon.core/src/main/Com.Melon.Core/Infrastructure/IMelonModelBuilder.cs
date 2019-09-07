using Com.Melon.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Melon.Core.Infrastructure
{
    public interface IModelBuilderAdapter
    {
        EntityTypeBuilder<T> AddEntity<T>() where T : Entity<T>;

        EntityTypeBuilder<T> AddValueObject<T>() where T: IdentifiedValueObject<T>;

        ModelBuilder GetModelBuilder();
    }
}
