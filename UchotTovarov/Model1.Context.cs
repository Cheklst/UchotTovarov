//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UchotTovarov
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Computers> Computers { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Goods> Goods { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<CheckList> CheckList { get; set; }
        public virtual DbSet<Supplies> Supplies { get; set; }
        public virtual DbSet<Type> Type { get; set; }
        public virtual DbSet<CheckGoods> CheckGoods { get; set; }
        public virtual DbSet<SuppliesGoods> SuppliesGoods { get; set; }
    }
}
