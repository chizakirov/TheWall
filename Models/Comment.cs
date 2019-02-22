using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace thewall.Models
{
  public class Comment
  {
    [Key]
    public int CommentId {get; set;}
    [Required]
    public string Text {get; set;}
    public int UserId {get; set;}
    public User User {get; set;}
    public int MessageId {get; set;}
    public Message Message {get; set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public Comment(){
    }

  }
}