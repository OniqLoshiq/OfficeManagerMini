using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Areas.Management.Models.InputModels;
using OMM.App.Areas.Management.Models.ViewModels;
using OMM.App.Common;
using OMM.App.Infrastructure.CustomAuthorization;
using OMM.Services.AutoMapper;
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

        [MinimumAccessLevel(AccessLevelValue.Five)]
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
            if (!ModelState.IsValid)
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

        [MinimumAccessLevel(AccessLevelValue.Seven)]
        public async Task<IActionResult> Edit(string id)
        {
            var employeeViewModel = await this.employeesService.GetEmployeeDtoByIdAsync<EmployeeEditDto>(id).To<EmployeeEditViewModel>().FirstOrDefaultAsync();

            return this.View(employeeViewModel);
        }

        [HttpPost]
        [MinimumAccessLevel(AccessLevelValue.Seven)]
        public async Task<IActionResult> Edit(EmployeeEditViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            if (input.ProfilePictureNew != null)
            {
                string pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                input.ProfilePictureNew,
                input.FullName);

                input.ProfilePicture = pictureUrl;
            }

            var employeeToEdit = AutoMapper.Mapper.Map<EmployeeEditDto>(input);

            await this.employeesService.EditAsync(employeeToEdit);

            return this.RedirectToAction(nameof(All));
        }

        
        [MinimumAccessLevel(AccessLevelValue.Eight)]
        public async Task<IActionResult> Release(string id)
        {
            var employeeViewModel = await this.employeesService.GetEmployeeDtoByIdAsync<EmployeeReleaseDto>(id).To<EmployeeReleaseViewModel>().FirstOrDefaultAsync();

            return View(employeeViewModel);
        }

        [HttpPost]
        [MinimumAccessLevel(AccessLevelValue.Eight)]
        public async Task<IActionResult> Release(EmployeeReleaseViewModel input)
        {
            return this.View();
        }
    }
}