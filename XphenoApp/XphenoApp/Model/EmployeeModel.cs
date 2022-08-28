using System;
using SQLite;

namespace XphenoApp.Model
{
    public class EmployeeModel
    {
        [PrimaryKey]
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

