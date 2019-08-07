using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Assignments;

namespace OMM.App.Models.ViewModels
{
    public class AssignmentOngoingListViewModel : IMapFrom<AssignmentListDto>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int Priority { get; set; }

        public bool IsProjectRelated { get; set; }

        public string StartingDate { get; set; }

        public string Deadline { get; set; }

        public double Progress { get; set; }

        public string StatusName { get; set; }

        public string AssignorName { get; set; }

        public string ExecutorName { get; set; }
    }
}
