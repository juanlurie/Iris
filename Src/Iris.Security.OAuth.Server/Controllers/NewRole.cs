using System;

namespace Iris.Security.OAuth.Server.Controllers
{
    public class NewRole
    {
        public Guid CommissionerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}