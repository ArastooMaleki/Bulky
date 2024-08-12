using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.ViewModels
{
    public class UserManagerEditVM
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public List<SelectListItem>? RoleselectListItems { get; set; }
    }
}
