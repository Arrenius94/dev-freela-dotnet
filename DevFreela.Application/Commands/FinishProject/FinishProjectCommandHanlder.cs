using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public class FinishProjectCommandHanlder : IRequestHandler<FinishProjectCommand, Unit>
{
    private readonly DevFreelaDbContext _dbContext;
    
    public FinishProjectCommandHanlder(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
    {
        
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == request.Id);
        
        if(project == null) return Unit.Value;
        
        project.Finish();
        await _dbContext.SaveChangesAsync();
        return Unit.Value;
    }
}