using AutoMapper;

namespace OMM.Services.AutoMapper
{ 


    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
