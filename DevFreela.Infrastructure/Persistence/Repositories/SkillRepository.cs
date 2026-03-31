using Dapper;
using DevFreela.Core.DTOS;
using DevFreela.Core.DTOS.Output.Projects;
using DevFreela.Core.DTOS.Output.SkillsOutputDto;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly string _connectionString;
    private readonly DevFreelaDbContext _dbContext;
    public SkillRepository(IConfiguration configuration, DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
        _connectionString = configuration.GetConnectionString("DevFreelaCs");
    }
    public async Task<int> SaveAsync(Skill skill)
    {
        await _dbContext.Skills.AddAsync(skill);
        await _dbContext.SaveChangesAsync();
        return skill.Id;
    }
    public async Task<List<SkillDTO>> GetAll(string? query)
    {
        using (var sqlConnection = new SqlConnection(_connectionString))
        {
            var script = "SELECT Id, Description FROM Skills WHERE Description LIKE @Query";
            var searchPattern = $"%{query ?? ""}%"; 
            var skills = await sqlConnection.QueryAsync<SkillDTO>(script, new { Query = searchPattern });
            return skills.ToList();
        }
    }
}