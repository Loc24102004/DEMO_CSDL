using System;
using System.Collections.Generic;

namespace DEMO_CSDL.Models;

public partial class TaiKhoan
{
    public int Idtk { get; set; }

    public string Tentk { get; set; } = null!;

    public string Mk { get; set; } = null!;

    public string? Honguoidung { get; set; }

    public string? Tennguoidung { get; set; }

    public string? Diachi { get; set; }

    public int? Sodienthoai { get; set; }

    public int? Sdtnguoithan { get; set; }

    public string? Email { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngaychinhsua { get; set; }

    public string? Nguoichinhsua { get; set; }

    public string? Nguoitao { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<NhatKy> NhatKies { get; set; } = new List<NhatKy>();
}
