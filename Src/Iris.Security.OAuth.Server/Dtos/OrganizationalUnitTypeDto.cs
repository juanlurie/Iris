using System;
using System.Collections.Generic;

namespace Iris.Security.OAuth.Server.Dtos
{
    public class OrganizationalUnitTypeDto
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string OrganizationalUnitTypeName { get; set; }
        public string Description { get; set; }
        public IEnumerable<OrganizationalUnitTypeDto> SubOrganizationUnitTypes { get; set; }
    }
}