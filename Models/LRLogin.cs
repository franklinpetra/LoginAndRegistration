using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace LoginAndRegistration.Models 
{   
    public class LRLogin
    {

        [Required(ErrorMessage="An email is required for login.")]
        [EmailAddress(ErrorMessage="Whoops. Something is missing? Is that a valid email address? Typo perhaps?")]
        public string Email {get;set;}

        [Required(ErrorMessage="A password is required.")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        
    }
}