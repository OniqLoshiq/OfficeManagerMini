using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMM.App.Areas.Management.Models.InputModels;
using OMM.App.Common;
using OMM.App.Infrastructure.CustomAuthorization;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Employees;

namespace OMM.App.Areas.Management.Controllers
{
    public class EmployeesController : BaseController
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IEmployeesService employeesService;

        public EmployeesController(ICloudinaryService cloudinaryService, IEmployeesService employeesService)
        {
            this.cloudinaryService = cloudinaryService;
            this.employeesService = employeesService;
        }

        public IActionResult All()
        {
            return View();
        }

        [MinimumAccessLevel(AccessLevelValue.Eight)]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [MinimumAccessLevel(AccessLevelValue.Eight)]
        public async Task<IActionResult> Register(EmployeeRegisterInputModel model)
        {
            if(!ModelState.IsValid)
            {
                return this.View();
            }

            string pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                model.ProfilePicture,
                model.FullName);

            var employeeRegisterDto = AutoMapper.Mapper.Map<EmployeeRegisterDto>(model);

            employeeRegisterDto.ProfilePicture = pictureUrl;

            await this.employeesService.RegisterEmployeeAsync(employeeRegisterDto);

            return this.Redirect("/");
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Release()
        {
            return View();
        }
    }
}