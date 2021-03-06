﻿using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Projects
{
    public class ProjectParticipantDto : IMapTo<EmployeesProjectsPositions>
    {
        public string EmployeeId { get; set; }

        public string ProjectId { get; set; }

        public int ProjectPositionId { get; set; }
    }
}
