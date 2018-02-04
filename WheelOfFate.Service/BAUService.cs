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
    /// <summary>
    /// Service is responsible for all BAU related operation
    /// </summary>
    public class BAUService : IBAUService
    {
        // max attempts to pick up suitable employee
        // every roll we will have different employees
        private const int MAX_RND_ATTEMPTS = 20;
        
        private IRepository<Employee> employeeRepository;
        private readonly IRepository<HistoryRecord> historyRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeRepository">Repository for Employee DataAccess</param>
        /// <param name="historyRepository">Repository for Employees History DataAccess</param>
        public BAUService(
                IRepository<Employee> employeeRepository, 
                IRepository<HistoryRecord> historyRepository)
        {   
            this.employeeRepository = employeeRepository;
            this.historyRepository = historyRepository;
        }

        /// <summary>
        /// Return random employees for BAU according params
        /// </summary>
        /// <param name="bauCapacity">How many employee</param>
        /// <param name="minShift">Minimal distance for employee between shifts </param>
        /// <param name="workingWindow">Window in days which considered as working cycle</param>
        /// <param name="reqDaysPerWindow">How many days eployee should have per working cycle</param>
        /// <returns></returns>
        public IEnumerable<EmployeeDTO> GetFor(int bauCapacity, int minShiftDays, int workingWindow, int reqDaysPerWindow)
        {
            var employeeDurations = historyRepository.Get()
                        .GroupBy(x => x.EmployeeId)
                        .ToDictionary(x => x.Key,
                                      x => x.Where(y => (DateTime.UtcNow - y.Start).TotalDays <= workingWindow)
                                            .Select(y => y.Duration)
                                            .Aggregate((y, z) => y + z));

            var historyByEmployeeId = 
                    historyRepository.Get().GroupBy(x => x.EmployeeId).ToDictionary(x => x.Key, x => x.AsEnumerable());

            var suitableEmployees =
                Mapper.Map<IEnumerable<EmployeeDTO>>(
                    employeeRepository.Get().Where(x => LastStartLongerThen(x, minShiftDays, historyByEmployeeId)
                                                && RequiredDaysPerWindow(reqDaysPerWindow, x, employeeDurations)));

            return GetUniquieRandomIndexes(bauCapacity, suitableEmployees.Count()).Select(i => suitableEmployees.ElementAt(i));
        }

        /// <summary>
        /// condition for max days in shift per cycle
        /// </summary>
        private static bool RequiredDaysPerWindow(int reqDaysPerWindow, Employee x, Dictionary<int, TimeSpan> employeeDurations)
        {
            return !employeeDurations.ContainsKey(x.Id) 
                   || employeeDurations[x.Id].TotalDays < reqDaysPerWindow;
        }

        /// <summary>
        /// Condition for min time from last shift
        /// </summary>
        private static bool LastStartLongerThen(Employee x, int minShiftDays, 
                                                Dictionary<int, IEnumerable<HistoryRecord>> historyByEmployeeId)
        {   
            return !historyByEmployeeId.ContainsKey(x.Id)
                   || historyByEmployeeId[x.Id].Count() == 0
                   || historyByEmployeeId[x.Id]
                        .OrderBy(y => y.Id)
                        .LastOrDefault(y => (DateTime.UtcNow - y.Start - y.Duration).TotalDays > minShiftDays) != null;
        }

        /// <summary>
        /// Randomly pick up employees from suitable ones
        /// </summary>
        /// <param name="bauCapacity"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private static List<int> GetUniquieRandomIndexes(int bauCapacity, int maxValue)
        {
            if (bauCapacity <= 0 || maxValue <= 0)
                return new List<int>();

            var usedIndexes = new HashSet<int>();
            var rndIndexes = new List<int>();
            var rnd = new Random();
            int i = 0;

            // just in case if we got already picked up random value we try MAX_RND_ATTEMPTS
            // every roll we will have different employees
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

        /// <summary>
        /// Add Employees to shift
        /// </summary>
        /// <param name="employeeIds">Ids of employees to add</param>
        /// <param name="shiftDurationHours">Shift duration in hours</param>
        public void AddShift(IEnumerable<int> employeeIds, int shiftDurationHours)
        {
            if (employeeIds == null)
                throw new ArgumentException("employeeIds");

            historyRepository.Add(employeeIds.Select(x => new HistoryRecord()
            {
                Duration = TimeSpan.FromHours(shiftDurationHours/2),
                EmployeeId = x,
                Start = DateTime.UtcNow
            }));
        }

        /// <summary>
        /// Return list of employees in current shift
        /// </summary>
        /// <returns>List of EmployeeInShiftDTO</returns>
        public IEnumerable<EmployeeInShiftDTO> GetCurrentShift()
        {
            var historyRecords = historyRepository.Get(x => x.Start.Add(x.Duration) > DateTime.UtcNow);
            var employeeIds = historyRecords.Select(x => x.EmployeeId);
            var employeesInShift = employeeRepository.Get(x => employeeIds.Contains(x.Id));

            var employeeToHistory = historyRecords.GroupBy(x => x.EmployeeId).ToDictionary(x => x.Key, x => x.OrderBy(y => y.Id).Last());

            return employeesInShift.Select(x => 
            {
                var emp = Mapper.Map<EmployeeInShiftDTO>(x);
                emp.TimeLeft = employeeToHistory[x.Id].Start + employeeToHistory[x.Id].Duration - DateTime.UtcNow;

                return emp;
            });
        }
    }
}
