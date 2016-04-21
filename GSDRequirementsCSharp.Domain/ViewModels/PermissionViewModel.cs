using GSDRequirementsCSharp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Domain.ViewModels
{
    public class PermissionViewModel
    {
        public UserViewModel User { get; set; }
        public Profile Profile { get; set; }
        public Guid Id { get; set; }

        public class ContactViewModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public Guid Id { get; set; }

            public static ContactViewModel FromModel(Contact model)
            {
                return new ContactViewModel
                {
                    Id = model.Id,
                    Email = model.Email,
                    Name = model.Name
                };
            }
        }

        public class UserViewModel
        {
            public ContactViewModel Contact { get; set; }
            public int Id { get; set; }

            public static UserViewModel FromModel(User model)
            {
                if (model == null || model.Contact == null)
                    return null;

                return new UserViewModel
                {
                    Id = model.Id,
                    Contact = ContactViewModel.FromModel(model.Contact)
                };
            }
        }

        public static PermissionViewModel FromModel(Permission model)
        {
            return new PermissionViewModel
            {
                User = UserViewModel.FromModel(model.User),
                Id = model.Id,
                Profile = model.Profile
            };
        }
    }
}
