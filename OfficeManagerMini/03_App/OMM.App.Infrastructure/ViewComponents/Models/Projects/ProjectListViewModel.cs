using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;

namespace OMM.App.Infrastructure.ViewComponents.Models.Projects
{
    public class ProjectListViewModel : IMapFrom<ProjectListDto>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
