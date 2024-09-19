﻿using CompanyG03DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyG03PL.ViewModels
{
    public class EmployeeViewModel
    {


        [Required(ErrorMessage = "Name is Requierd")]
        public string Name { get; set; }

        [Range(25, 60, ErrorMessage = "Age must be between 25 ,60")]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{4,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address must be Like 123-Street-City-Country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime HireDate { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public int? WorkForId { get; set; } //FK
        public Department? WorkFor { get; set; }//NavigationalProp






    }
}
