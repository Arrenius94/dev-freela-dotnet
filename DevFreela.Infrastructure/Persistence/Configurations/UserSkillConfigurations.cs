using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations;

public class UserSkillConfigurations : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        builder
            .ToTable("UserSkill")
            .HasKey(s => s.Id);

        builder
            .HasOne(us => us.Skill)          
            .WithMany()                      
            .HasForeignKey(us => us.IdSkill)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(us => us.User)           
            .WithMany()                      
            .HasForeignKey(us => us.IdUser)  
            .OnDelete(DeleteBehavior.Restrict);
    }
}