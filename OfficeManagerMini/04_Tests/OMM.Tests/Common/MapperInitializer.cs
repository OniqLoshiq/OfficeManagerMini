using System.Reflection;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;

namespace OMM.Tests.Common
{
    public static class MapperInitializer
    {
            public static void InitializeMapper()
            {
                AutoMapperConfig.RegisterMappings(
                    typeof(EmployeeRegisterDto).GetTypeInfo().Assembly,
                    typeof(Employee).GetTypeInfo().Assembly);
            }
    }
}
