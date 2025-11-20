using System;
using System.Collections.Generic;

namespace AuthLibrary.Models;

public partial class CinemaRolePrivilege
{
    public int RoleId { get; set; }

    public int PrivilegeId { get; set; }

    public virtual CinemaPrivilege Privilege { get; set; } = null!;
}
