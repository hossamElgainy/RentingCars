using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Core.Abstraction;
using Core.Enums;

namespace Core.DomainModels
{
    public class User:BaseEntityGuid
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public RolesEnum Role { get; set; }
    }
}
