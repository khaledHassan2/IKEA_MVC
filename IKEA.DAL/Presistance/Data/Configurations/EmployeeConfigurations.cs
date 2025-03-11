using IKEA.DAL.Common.Enum;
using IKEA.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKEA.DAL.Presistance.Data.Configurations
{
    class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(100)");
            builder.Property(E => E.Salary).HasColumnType("decimal(8,5)");
            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(E => E.Gender).HasConversion((gender) => gender.ToString(), gender => (Gender)Enum.Parse(typeof(Gender), gender));
            builder.Property(E => E.EmployeeType).HasConversion((type) => type.ToString(), type => (EmployeeType)Enum.Parse(typeof(EmployeeType), type));


        }
    }
}
