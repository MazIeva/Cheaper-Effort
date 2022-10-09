using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Cheaper_Effort.Models
{ 
    public class Account
    {
    
        public uint Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                                ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        public string LastName { get; set; } = String.Empty;

        [Required]
        public string Username { get; set; } = String.Empty;


        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$",
                                ErrorMessage = "Password is not save, it must contain : at least: 8 characters, 1 uppercase, 1 lowercase, 1 digit ")]
        public string Password { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password did not match")]
        public string ConfirmPassword { get; set; } = String.Empty;


    }
}

