using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TunashaProjects.Models
{
    public class User
    {
        public User()
        {
            Replies = new List<Reply>();
            Posts = new List<Post>();
        }

        [Key]
        public int UserID { get; set; }     
        [Required]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }
        [DataType(DataType.Password), Required]
        public string Password { get; set; }
        [ScaffoldColumn (false)]
        public int RoleID { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual Role Role { get; set; }
    }

    public class Role
    {
        public Role()
        {
            Users = new List<User>();
        }

        [Key]
        public int ID { get; set; }
        [MaxLength(10)]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }

    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }
        [DataType(DataType.Password), Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class TempUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}