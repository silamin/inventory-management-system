using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class Roles
    {
        public const string INVENTORY_MANAGER = "INVENTORY_MANAGER";
        public const string WAREHOUSE_WORKER = "WAREHOUSE_WORKER";

        public enum UserRole
        {
            INVENTORY_MANAGER,
            WAREHOUSE_WORKER
        }
    }

}
