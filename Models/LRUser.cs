using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    
namespace LoginAndRegistration.Models 
{   
    public class LRUser
    {
        [Key]
        public int UserId {get;set;}
        [Required(ErrorMessage="A first name is neccessary. Here are a few possibilities: Janet, Henry, Lawrence, Madeline")]
        [MinLength(2, ErrorMessage="The first name should at least be 2 characters.")]
        public string FirstName {get;set;}


        [Required(ErrorMessage="A last name is neccessary. Here are a few possibilities: Brown, Smith, Washington, Childs")]
        [MinLength(2, ErrorMessage="The last name should at least be 2 characters.")]
        public string LastName {get;set;}


        [Required(ErrorMessage="We'll need an email to contact you.")]
        [EmailAddress(ErrorMessage="Whoops. Something is missing? Is that a valid email address?")]
        public string Email {get;set;}


        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="Your password must be 8 or more characters in length.")]
        public string Password {get;set;}


        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}   

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        
    }
}