using System;
using System.Collections.Generic;

namespace DEMO_CSDL.Models;

public partial class TrangThai
{
    public int Idtrangthai { get; set; }

    public string? Tentrangthai { get; set; }

    public string? Mota { get; set; }

    public virtual ICollection<NhatKy> NhatKies { get; set; } = new List<NhatKy>();
}
