using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Repositories.Entities;
using System.Text.Json.Serialization;


namespace Repositories.FluentApi
{
    public class BackgroundDoctorConfiguration : IEntityTypeConfiguration<BackgroundDoctor>
    {
        public void Configure(EntityTypeBuilder<BackgroundDoctor> builder)
        {
            builder.HasKey(b => b.BgDoctorId);
            builder.HasOne(b => b.ApplicationUser)
                   .WithOne(a => a.BackgroundDoctor)
                   .HasForeignKey<BackgroundDoctor>(b => b.userId);

            var stringListComparer = new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2),  // So sánh các phần tử trong List
                c => c.Aggregate(0, (a, v) => a ^ v.GetHashCode()),  // Tính hash code cho List
                c => c.ToList()  // Clone lại List
            );

            builder.Property(b => b.Specialization)
                   .HasConversion(
                        b => JsonConvert.SerializeObject(b), // Convert List<string> to JSON string
                        b => JsonConvert.DeserializeObject<List<string>>(b))
                   .Metadata.SetValueComparer(stringListComparer); // Convert JSON string back to List<string>
            builder.Property(b => b.Certifications)
                   .HasConversion(
                        c => JsonConvert.SerializeObject(c), 
                        c => JsonConvert.DeserializeObject<List<string>>(c))
                   .Metadata.SetValueComparer(stringListComparer);
            builder.Property(b => b.DateOfBirth)
                   .HasColumnType("date");
            builder.Property(b => b.CreatedDate)
                   .HasColumnType("date");
            builder.Property(b => b.UpdatedDate)
                   .HasColumnType("date");
        }
    }
}
