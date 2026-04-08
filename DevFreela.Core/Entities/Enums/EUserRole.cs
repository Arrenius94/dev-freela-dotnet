using System.ComponentModel;

namespace DevFreela.Core.Entities;

public enum EUserRole
{
    [Description("CLIENT")]
    Client = 1,
    [Description("FREELANCER")]
    Freelancer = 2,
}