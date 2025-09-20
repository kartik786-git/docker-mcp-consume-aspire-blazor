using ModelContextProtocol.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnCustomMcpServer
{
    internal class EmployeeMcpSerever
    {
        [McpServerTool]
        [Description("Gets employee list")]
        public List<Employee> GetEmployeeList()
        {
            return new List<Employee>
        {
            new Employee { Id = 100, Name = "Alice", Position = "Developer" },
            new Employee { Id = 222, Name = "Bob", Position = "Designer" },
            new Employee { Id = 333, Name = "Charlie", Position = "Manager" }
        };
        }

        // CREATE A TOOL TO RETURN A SINGLE EMPLOYEE BY ID
        [McpServerTool]
        [Description("Get an employee by ID.")]
        public Employee? GetEmployeeById([Description("Employee Id ")] int id)
        {
            var employees = GetEmployeeList();
            return employees.FirstOrDefault(e => e.Id == id);
        }

        // CREATE A TOOL TO RETURN LIST OF EMPLOYEES BY NAME
        [McpServerTool]
        [Description("Get employees by name.")]
        public List<Employee> GetEmployeesByName([Description("Employee Name ")] string name)
        {
            var employees = GetEmployeeList();
            return employees.Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }

}
