using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.DTOS.Output.Projects
{
    public class ProjectListDTO
    {
      

        public ProjectListDTO(int id, string title, string description, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
