using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.DTOS.Output.Projects
{
    public class ProjectListDTO
    {
        private string description;

        public ProjectListDTO(string title, string description, DateTime createdAt)
        {
            Title = title;
            this.description = description;
            CreatedAt = createdAt;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
