using FinanceHelper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Data
{
    public class IdentityContext : IdentityDbContext<IdentityUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AccountsModel>().HasKey(x => new { x.AccountID, x.UserID });
            base.OnModelCreating(builder);
        }

        public DbSet<FinanceHelper.Models.AccountsModel> Accounts { get; set; }

        public DbSet<FinanceHelper.Models.AmortizationEntryModel> AmortizationEntryModel { get; set; }
    }
}
