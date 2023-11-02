using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation.Interfaces
{
    public interface IUserValidation
    {
        void ValidateUsername(string username);
        void ValidateIsManager(int? id);
        void ValidateExistence(int id);
        void ValidateUserManager(int managerID, int userId);
    }
}
