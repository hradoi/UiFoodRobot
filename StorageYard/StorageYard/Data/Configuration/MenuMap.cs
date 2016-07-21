using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageYard.Data.Configuration
{
    class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            Property(one => one.Name).HasMaxLength(128).IsVariableLength();
        }
    }
}
