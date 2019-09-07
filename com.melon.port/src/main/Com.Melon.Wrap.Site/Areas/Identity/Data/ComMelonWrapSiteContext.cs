using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Com.Melon.Wrap.Site.Areas.Users.Models;

namespace Com.Melon.Wrap.Site.Models
{
    public class ComMelonWrapSiteContext : DbContext
    {
        public ComMelonWrapSiteContext (DbContextOptions<ComMelonWrapSiteContext> options)
            : base(options)
        {
        }

        public DbSet<Com.Melon.Wrap.Site.Areas.Users.Models.RegisterUserViewModel> LoginViewModel { get; set; }
    }
}
