using System;
using System.Collections.Generic;

namespace DEMO_CSDL.Models;

public partial class LoaiNk
{
    public int Idloaink { get; set; }

    public string? Tenloai { get; set; }

    public string? Mota { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngaychinhsua { get; set; }

    public string? Nguoichinhsua { get; set; }

    public string? Nguoitao { get; set; }

    public virtual ICollection<NhatKy> NhatKies { get; set; } = new List<NhatKy>();
}
