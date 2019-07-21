﻿using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Departments;

namespace OMM.App.Infrastructure.ViewComponents.Models
{
    public class DepartmentViewComponentViewModel : IMapFrom<DepartmentNameDto>
    {
        public string Name { get; set; }
    }
}
