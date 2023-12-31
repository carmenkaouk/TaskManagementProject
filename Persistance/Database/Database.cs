﻿using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database; 

internal class Database
{
    public List<User> Users { get; set; }
    public List<Department> Departments { get; set; }
    public List<TaskToDo> Tasks { get; set; }
    public List<ReportingLine> ReportingLines { get; set; }
}
