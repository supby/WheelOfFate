using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheelOfFate.Models.DTO;
using WheelOfFate.Models.Entity;

namespace WheelOfFate.Web
{
    public class Mappings
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<EmployeeDTO, Employee>();
            });
        }
    }
}
