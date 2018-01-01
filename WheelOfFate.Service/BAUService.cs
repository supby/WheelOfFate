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
    public class BAUService : IBAUService
    {
        private const int MAX_RND_ATTEMPTS = 20;
        
        private IRepository<Employee> employeeRepository;
        private readonly IRepository<HistoryRecord> historyRepository;

        public BAUService(
                IRepository<Employee> employeeRepository, 
                IRepository<HistoryRecord> historyRepository)
        {   
            this.employeeRepository = employeeRepository;
            this.historyRepository = historyRepository;
        }

        public IEnumerable<EmployeeDTO> GetFor(int bauCapacity, int minShiftDays, int workingWindow, int reqDaysPerWindow)
        {
            var employeeDurations = historyRepository.Get()
                        .GroupBy(x => x.EmployeeId)
                        .ToDictionary(x => x.Key,
                                      x => x.Where(y => (DateTime.UtcNow - y.Start).TotalDays <= workingWindow)
                                            .Select(y => y.Duration)
                                            .Aggregate((y, z) => y + z));

            var suitableEmployees =
                Mapper.Map<IEnumerable<EmployeeDTO>>(
                    employeeRepository.Get().Where(x => LastStartLongerThen(x, minShiftDays)
                                                && RequiredDaysPerWindow(reqDaysPerWindow, x, employeeDurations)));

            return GetUniquieRandomIndexes(bauCapacity, suitableEmployees.Count()).Select(i => suitableEmployees.ElementAt(i));
        }

        private static bool RequiredDaysPerWindow(int reqDaysPerWindow, Employee x, Dictionary<int, TimeSpan> employeeDurations)
        {
            return !employeeDurations.ContainsKey(x.Id) 
                   || employeeDurations[x.Id].TotalDays < reqDaysPerWindow;
        }

        private static bool LastStartLongerThen(Employee x, int minShiftDays)
        {
            return x.HistoryRecords == null
                   || x.HistoryRecords.Count() == 0
                   || x.HistoryRecords.OrderBy(y => y.Id).LastOrDefault(y => (DateTime.UtcNow - y.Start - y.Duration).TotalDays > minShiftDays) != null;
        }

        private static List<int> GetUniquieRandomIndexes(int bauCapacity, int maxValue)
        {
            if (bauCapacity <= 0 || maxValue <= 0)
                return new List<int>();

            var usedIndexes = new HashSet<int>();
            var rndIndexes = new List<int>();
            var rnd = new Random();
            int i = 0;

            // just in case if we got already picked up random value we try MAX_RND_ATTEMPTS
            while (rndIndexes.Count < bauCapacity && i < MAX_RND_ATTEMPTS)
            {
                int nextVal = rnd.Next(maxValue);
                if (!usedIndexes.Contains(nextVal))
                {
                    rndIndexes.Add(nextVal);
                    usedIndexes.Add(nextVal);
                }
                i++;
            }

            return rndIndexes;
        }
    }
}
