using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AccountService.Models
{
    public sealed class Role : EnumClass
    {
        public static readonly Role Owner = new Role(1, "Owner");
        public static readonly Role Admin = new Role(2, "Admin");
        public static readonly Role User = new Role(3, "User");
        public static readonly Role AHL = new Role(2003, "AHL");

        [JsonConstructor]
        private Role(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public static Role GetRole(int id)
        {
            switch (id)
            {
                case 1: return Owner;
                case 2: return Admin;
                case 3: return User;
                case 2003: return AHL;
                default: return null;
            }
        }

        public static List<Role> GetRoles()
        {
            return new List<Role>()
            {
                Owner,
                Admin,
                User,
                AHL
            };
        }
    }
}
