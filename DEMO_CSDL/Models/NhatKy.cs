using System;
using System.Collections.Generic;

namespace DEMO_CSDL.Models;

public partial class NhatKy
{
    public int Idnk { get; set; }

    public int? Idloaink { get; set; }

    public int? Idtrangthai { get; set; }

    public string? Tieude { get; set; }

    public string? Noidung { get; set; }

    public string? Mota { get; set; }

    public string? Hinhanh { get; set; }

    public string? Video { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngaychinhsua { get; set; }

    public string? Nguoichinhsua { get; set; }

    public string? Nguoitao { get; set; }

    public int? Idtk { get; set; } 

    public virtual LoaiNk? IdloainkNavigation { get; set; }

    public virtual TaiKhoan IdtkNavigation { get; set; } = null!;

    public virtual TrangThai? IdtrangthaiNavigation { get; set; }
}
