using DevFreela.Core.DTOS.Output.Projects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _dbContext;
    
    public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveAsync (Project project)
    {
        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();

        return project.Id;
    }

    public async Task<List<ProjectListDTO>> GetAll(string query)
    {
        var projectsQuery = _dbContext.Projects.AsQueryable();
        if (!string.IsNullOrWhiteSpace(query))
        {
            projectsQuery = projectsQuery.Where(p => p.Title.Contains(query));
        }
        return await projectsQuery
            .Select(p => new ProjectListDTO(
                p.Id,
                p.Title,
                p.Description,
                p.CreatedAt
            ))
            .ToListAsync();
    }

    public async Task<ProjectDetailsDTO?> GetById(int id)
    {
        return await _dbContext.Projects
            .Where(p => p.Id == id)
            .Select(p => new ProjectDetailsDTO(
                p.Id,
                p.Title,
                p.Description,
                p.TotalCost,
                p.StartedAt,
                p.FinishAt,
                p.Client.FullName,
                p.FreeLancer.FullName
            ))
            .FirstOrDefaultAsync();
    }

    public async Task AddCommentAsync(ProjectComment projectComment)
    {
        await _dbContext.Comments.AddAsync(projectComment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Project?> CancelAsync(int id)
    {
        var project = await _dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
        if (project != null)
        {
            project.Cancel();
            await _dbContext.SaveChangesAsync();
        }
        return project;
    }

    public async Task UpdateAsync(Project project)
    {
        await _dbContext.SaveChangesAsync();
    }
}