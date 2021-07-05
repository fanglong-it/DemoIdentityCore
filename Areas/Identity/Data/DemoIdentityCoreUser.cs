using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DemoIdentityCore.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the DemoIdentityCoreUser class
    public class DemoIdentityCoreUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]

        public string Firstname { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        
        public string LastName { get; set; }



    }
}
