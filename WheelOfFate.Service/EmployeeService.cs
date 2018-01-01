using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WheelOfFate.Interfaces.DataAccess;
using WheelOfFate.Interfaces.Services;
using WheelOfFate.Models.DTO;
using WheelOfFate.Models.Entity;

namespace WheelOfFate.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IEnumerable<EmployeeDTO> Get()
        {
            return Mapper.Map<IEnumerable<EmployeeDTO>>(employeeRepository.Get());
        }

        public void Delete(int id)
        {
            var entity = employeeRepository.FindById(id);
            employeeRepository.Remove(entity);
        }

        public IEnumerable<EmployeeDTO> Save(IEnumerable<EmployeeDTO> employees)
        {
            var entitiesToUpdate = Mapper.Map<IEnumerable<Employee>>(employees);
            employeeRepository.Add(entitiesToUpdate);

            return Mapper.Map<IEnumerable<EmployeeDTO>>(entitiesToUpdate);
        }
    }
}
