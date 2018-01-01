using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WheelOfFate.Interfaces.DataAccess;
using WheelOfFate.Models.Entity;
using WheelOfFate.Services;
using WheelOfFate.Web;
using Xunit;

namespace WheelOfFate.Tests
{
    public class BAUServiceTests
    {
        static BAUServiceTests()
        {
            Mappings.Configure();
        }

        private readonly List<HistoryRecord> historyRecords1;
        private readonly List<HistoryRecord> historyRecords2;
        private readonly List<HistoryRecord> historyRecords3;
        private readonly List<Employee> employeesZeroHistory;
        private readonly List<Employee> employeesWithHistory1;
        private readonly List<Employee> employeesWithHistory2;
        private readonly List<Employee> employeesWithHistory3;
        private readonly Mock<IRepository<Employee>> employeeRepositoryMock;
        private readonly Mock<IRepository<HistoryRecord>> historyRepositoryMock;

        public BAUServiceTests()
        {
            historyRecords1 = new List<HistoryRecord>()
            {
                    new HistoryRecord() { EmployeeId = 1, Start = DateTime.UtcNow - TimeSpan.FromDays(2), Duration = TimeSpan.FromHours(12) },
                    new HistoryRecord() { EmployeeId = 2, Start = DateTime.UtcNow - TimeSpan.FromDays(2), Duration = TimeSpan.FromHours(12) },
                    new HistoryRecord() { EmployeeId = 3, Start = DateTime.UtcNow - TimeSpan.FromDays(1), Duration = TimeSpan.FromHours(12) },
                    new HistoryRecord() { EmployeeId = 4, Start = DateTime.UtcNow - TimeSpan.FromDays(1), Duration = TimeSpan.FromHours(12) },
            };

            historyRecords2 = new List<HistoryRecord>()
            {
                    new HistoryRecord() { EmployeeId = 1, Start = DateTime.UtcNow - TimeSpan.FromDays(5), Duration = TimeSpan.FromHours(12) },
                    new HistoryRecord() { EmployeeId = 2, Start = DateTime.UtcNow - TimeSpan.FromDays(2), Duration = TimeSpan.FromHours(12) },
                    new HistoryRecord() { EmployeeId = 2, Start = DateTime.UtcNow - TimeSpan.FromDays(4), Duration = TimeSpan.FromHours(12) },
                    new HistoryRecord() { EmployeeId = 3, Start = DateTime.UtcNow - TimeSpan.FromDays(2), Duration = TimeSpan.FromHours(12) },
                    new HistoryRecord() { EmployeeId = 3, Start = DateTime.UtcNow - TimeSpan.FromDays(4), Duration = TimeSpan.FromHours(12) },
                    new HistoryRecord() { EmployeeId = 4, Start = DateTime.UtcNow - TimeSpan.FromDays(6), Duration = TimeSpan.FromHours(12) },
                    new HistoryRecord() { EmployeeId = 4, Start = DateTime.UtcNow - TimeSpan.FromDays(7), Duration = TimeSpan.FromHours(12) },
            };

            historyRecords3 = new List<HistoryRecord>()
            {
                    new HistoryRecord() { EmployeeId = 1, Start = DateTime.UtcNow - TimeSpan.FromDays(1), Duration = TimeSpan.FromHours(12) },
                    new HistoryRecord() { EmployeeId = 2, Start = DateTime.UtcNow - TimeSpan.FromDays(1), Duration = TimeSpan.FromHours(12) },                    
                    new HistoryRecord() { EmployeeId = 3, Start = DateTime.UtcNow - TimeSpan.FromDays(1), Duration = TimeSpan.FromHours(12) },                    
                    new HistoryRecord() { EmployeeId = 4, Start = DateTime.UtcNow - TimeSpan.FromDays(1), Duration = TimeSpan.FromHours(12) },                    
            };

            employeesZeroHistory = new List<Employee>()
            {
                    new Employee() { Id = 1, Name = "Firstname1 Lastname1" },
                    new Employee() { Id = 2, Name = "Firstname2 Lastname2" },
                    new Employee() { Id = 3, Name = "Firstname3 Lastname3" },
                    new Employee() { Id = 4, Name = "Firstname4 Lastname4" },
            };

            employeesWithHistory1 = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Firstname1 Lastname1", HistoryRecords = historyRecords1.Where(x => x.EmployeeId == 1).ToList() },
                new Employee() { Id = 2, Name = "Firstname2 Lastname2", HistoryRecords = historyRecords1.Where(x => x.EmployeeId == 2).ToList() },
                new Employee() { Id = 3, Name = "Firstname3 Lastname3", HistoryRecords = historyRecords1.Where(x => x.EmployeeId == 3).ToList() },
                new Employee() { Id = 4, Name = "Firstname4 Lastname4", HistoryRecords = historyRecords1.Where(x => x.EmployeeId == 4).ToList() },
            };

            employeesWithHistory2 = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Firstname1 Lastname1", HistoryRecords = historyRecords2.Where(x => x.EmployeeId == 1).ToList() },
                new Employee() { Id = 2, Name = "Firstname2 Lastname2", HistoryRecords = historyRecords2.Where(x => x.EmployeeId == 2).ToList() },
                new Employee() { Id = 3, Name = "Firstname3 Lastname3", HistoryRecords = historyRecords2.Where(x => x.EmployeeId == 3).ToList() },
                new Employee() { Id = 4, Name = "Firstname4 Lastname4", HistoryRecords = historyRecords2.Where(x => x.EmployeeId == 4).ToList() },
            };

            employeesWithHistory3 = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Firstname1 Lastname1", HistoryRecords = historyRecords3.Where(x => x.EmployeeId == 1).ToList() },
                new Employee() { Id = 2, Name = "Firstname2 Lastname2", HistoryRecords = historyRecords3.Where(x => x.EmployeeId == 2).ToList() },
                new Employee() { Id = 3, Name = "Firstname3 Lastname3", HistoryRecords = historyRecords3.Where(x => x.EmployeeId == 3).ToList() },
                new Employee() { Id = 4, Name = "Firstname4 Lastname4", HistoryRecords = historyRecords3.Where(x => x.EmployeeId == 4).ToList() },
            };

            employeeRepositoryMock = new Mock<IRepository<Employee>>();
            historyRepositoryMock = new Mock<IRepository<HistoryRecord>>();
        }

        // Get 2 employees from 4 suitable.
        [Fact]
        public void GetTwoFromAllFresh()
        {   
            employeeRepositoryMock
                .Setup(x => x.Get())
                .Returns(employeesZeroHistory);
            
            historyRepositoryMock
                .Setup(x => x.Get())
                .Returns(new List<HistoryRecord>());

            var target = new BAUService(employeeRepositoryMock.Object, historyRepositoryMock.Object);
            var res = target.GetFor(2, 1, 14, 1);

            Assert.Equal(2, res.Count());
        }

        // Get 2 employees from 4 suitable. 
        // 2 emps have last support on previous days. 
        // 2 emps have last support 2 days ago. 
        [Fact]
        public void GetTwoFromTwoFresh()
        {   
            employeeRepositoryMock
                .Setup(x => x.Get())
                .Returns(employeesWithHistory1);
            
            historyRepositoryMock
                .Setup(x => x.Get())
                .Returns(historyRecords1);

            var target = new BAUService(employeeRepositoryMock.Object, historyRepositoryMock.Object);
            var res = target.GetFor(2, 1, 14, 1).ToList();

            Assert.Equal(2, res.Count());
            Assert.Contains(res, x => x.Id == 1);
            Assert.Contains(res, x => x.Id == 2);
        }

        // get one employee from 4 suitable
        // but this employee has less than 1 day support per last 14 days.
        [Fact]
        public void GetOneFromAllFreshButWithLessThanReqPerWindow()
        {
            employeeRepositoryMock
                .Setup(x => x.Get())
                .Returns(employeesWithHistory2);

            historyRepositoryMock
                .Setup(x => x.Get())
                .Returns(historyRecords2);

            var target = new BAUService(employeeRepositoryMock.Object, historyRepositoryMock.Object);
            var res = target.GetFor(1, 1, 14, 1).ToList();

            Assert.Single(res);
            Assert.Contains(res, x => x.Id == 1);
        }

        // there are only 2 suitable employee
        // try to get 3 but will return 2 anyway
        [Fact]
        public void TryGetThreeFromTwoFresh()
        {
            employeeRepositoryMock
                .Setup(x => x.Get())
                .Returns(employeesWithHistory1);

            historyRepositoryMock
                .Setup(x => x.Get())
                .Returns(historyRecords1);

            var target = new BAUService(employeeRepositoryMock.Object, historyRepositoryMock.Object);
            var res = target.GetFor(3, 1, 14, 1).ToList();

            Assert.Equal(2, res.Count());
            Assert.Contains(res, x => x.Id == 1);
            Assert.Contains(res, x => x.Id == 2);
        }

        // if there is no suitable employees, return 0
        [Fact]
        public void TryGetTwoFromAllNoSuitable()
        {
            employeeRepositoryMock
                .Setup(x => x.Get())
                .Returns(employeesWithHistory3);

            historyRepositoryMock
                .Setup(x => x.Get())
                .Returns(historyRecords3);

            var target = new BAUService(employeeRepositoryMock.Object, historyRepositoryMock.Object);
            var res = target.GetFor(3, 1, 14, 1).ToList();

            Assert.Empty(res);
        }
    }
}
