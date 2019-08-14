using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.ProjectPositions
{
    public class ProjectPostionDto : IMapFrom<ProjectPosition>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
