using System;
using System.Collections.Generic;

namespace AuthLibrary.Models;

public partial class CinemaPrivilege
{
    public int PrivilegeId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<CinemaRolePrivilege> CinemaRolePrivileges { get; set; } = new List<CinemaRolePrivilege>();
}
