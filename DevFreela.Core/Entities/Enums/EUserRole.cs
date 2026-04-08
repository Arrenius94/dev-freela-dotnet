using System.ComponentModel;

namespace DevFreela.Core.Entities;

public enum EUserRole
{ 
    [Description ("CLIENTE")]
    Client = 1,
    [Description ("FREELANCER")]
    Freelancer = 2
}