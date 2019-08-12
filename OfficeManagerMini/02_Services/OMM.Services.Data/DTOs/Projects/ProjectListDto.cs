using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Projects
{
    public class ProjectListDto : IMapFrom<Project>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
