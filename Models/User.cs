using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace thewall.Models
{
  public class User
  {
    [Key]
    public int UserId {get;set;}

    [Required]
    [MinLength(2, ErrorMessage = "First Name must be at least 2 characters long")]
    public string FirstName {get; set;}

    [Required]
    [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters long")]
    public string LastName {get; set;}

    [Required]
    [EmailAddress]
    public string Email {get; set;}

    [Required]
    [MinLength(8, ErrorMessage = "Password must be 8 characters long"), DataType(DataType.Password)]
    public string Password {get; set;}

    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string Confirm {get; set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    public List<Message> Messages {get; set;}
    public List<Comment> Comments {get; set;}
    public User(){
    }
  }
}