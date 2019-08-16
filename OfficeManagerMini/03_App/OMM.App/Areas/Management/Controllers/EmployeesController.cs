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

            string pictureName = string.Join("_",model.FirstName, model.MiddleName[0], model.LastName);

            string pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                model.ProfilePicture,
                pictureName);

            var employeeRegisterDto = AutoMapper.Mapper.Map<EmployeeRegisterDto>(model);

            employeeRegisterDto.ProfilePicture = pictureUrl;

            await this.employeesService.RegisterEmployeeAsync(employeeRegisterDto);

            return this.RedirectToAction("All", "Employees", new { area = "Management"});
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
                string pictureName = string.Join("_", input.FirstName, input.MiddleName[0], input.LastName);

                string pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                input.ProfilePictureNew,
                pictureName);

                input.ProfilePicture = pictureUrl;
            }

            var employeeToEdit = AutoMapper.Mapper.Map<EmployeeEditDto>(input);

            await this.employeesService.EditAsync(employeeToEdit);

            return this.RedirectToAction(nameof(All));
        }

        
        [MinimumAccessLevel(AccessLevelValue.Eight)]
        public async Task<IActionResult> Release(string id)
        {
            var employeeViewModel = (await this.employeesService.GetEmployeeDtoByIdAsync<EmployeeReleaseDto>(id).SingleOrDefaultAsync()).To<EmployeeReleaseViewModel>();

            return View(employeeViewModel);
        }

        [HttpPost]
        [MinimumAccessLevel(AccessLevelValue.Eight)]
        public async Task<IActionResult> Release(EmployeeReleaseViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var employeeToRelease = input.To<EmployeeReleaseDto>();

            await this.employeesService.ReleaseAsync(employeeToRelease);

            return this.RedirectToAction(nameof(All));
        }

        [MinimumAccessLevel(AccessLevelValue.Eight)]
        public async Task<IActionResult> HireBack(string id)
        {
            var employeeViewModel = await this.employeesService.GetEmployeeDtoByIdAsync<EmployeeHireBackDto>(id).To<EmployeeHireBackViewModel>().FirstOrDefaultAsync();

            return this.View(employeeViewModel);
        }

        [HttpPost]
        [MinimumAccessLevel(AccessLevelValue.Eight)]
        public async Task<IActionResult> HireBack(EmployeeHireBackViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            if (input.ProfilePictureNew != null)
            {
                string pictureName = string.Join("_", input.FirstName, input.MiddleName[0], input.LastName);

                string pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                input.ProfilePictureNew,
                pictureName);

                input.ProfilePicture = pictureUrl;
            }

            var employeeToHireBack = input.To<EmployeeHireBackDto>();

            await this.employeesService.HireBackAsync(employeeToHireBack);

            return this.RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> LoadProjectParticipantAdditionalData (string employeeId)
        {
            var employeeInfo = (await this.employeesService.GetEmployeeDtoByIdAsync<EmployeeProjectParticipantAdditionalInfoDto>(employeeId).FirstOrDefaultAsync())
                .To<EmployeeProjectParticipantAdditionalInfoViewModel>();

            return new JsonResult(employeeInfo);
        }
    }
}