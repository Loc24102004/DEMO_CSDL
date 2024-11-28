using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DEMO_CSDL.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LoaiNk> LoaiNks { get; set; }

    public virtual DbSet<NhatKy> NhatKies { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TrangThai> TrangThais { get; set; }

   /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-9BKO72M\\SQLEXPRESS;Initial Catalog=Nhat_Ky;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoaiNk>(entity =>
        {
            entity.HasKey(e => e.Idloaink);

            entity.ToTable("Loai_NK");

            entity.Property(e => e.Idloaink)
                .ValueGeneratedOnAdd()
                .HasColumnName("IDLOAINK");
            entity.Property(e => e.Mota)
                .HasMaxLength(100)
                .HasColumnName("MOTA");
            entity.Property(e => e.Ngaychinhsua)
                .HasColumnType("datetime")
                .HasColumnName("NGAYCHINHSUA");
            entity.Property(e => e.Ngaytao)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Nguoichinhsua)
                .HasMaxLength(50)
                .HasColumnName("NGUOICHINHSUA");
            entity.Property(e => e.Nguoitao)
                .HasMaxLength(50)
                .HasColumnName("NGUOITAO");
            entity.Property(e => e.Tenloai)
                .HasMaxLength(50)
                .HasColumnName("TENLOAI");
        });

        modelBuilder.Entity<NhatKy>(entity =>
        {
            entity.HasKey(e => e.Idnk);

            entity.ToTable("Nhat_Ky");

            entity.Property(e => e.Idnk)
                .ValueGeneratedOnAdd()
                .HasColumnName("IDNK");
            entity.Property(e => e.Hinhanh).HasColumnName("HINHANH");
            entity.Property(e => e.Idloaink).HasColumnName("IDLOAINK");
            entity.Property(e => e.Idtk).HasColumnName("IDTK");
            entity.Property(e => e.Idtrangthai).HasColumnName("IDTRANGTHAI");
            entity.Property(e => e.Mota)
                .HasMaxLength(50)
                .HasColumnName("MOTA");
            entity.Property(e => e.Ngaychinhsua)
                .HasColumnType("datetime")
                .HasColumnName("NGAYCHINHSUA");
            entity.Property(e => e.Ngaytao)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Nguoichinhsua)
                .HasMaxLength(50)
                .HasColumnName("NGUOICHINHSUA");
            entity.Property(e => e.Nguoitao)
                .HasMaxLength(50)
                .HasColumnName("NGUOITAO");
            entity.Property(e => e.Noidung).HasColumnName("NOIDUNG");
            entity.Property(e => e.Tieude)
                .HasMaxLength(50)
                .HasColumnName("TIEUDE");
            entity.Property(e => e.Video).HasColumnName("VIDEO");

            entity.HasOne(d => d.IdloainkNavigation).WithMany(p => p.NhatKies)
                .HasForeignKey(d => d.Idloaink)
                .HasConstraintName("FK_Nhat_Ky_Loai_NK");

            entity.HasOne(d => d.IdtkNavigation).WithMany(p => p.NhatKies)
                .HasForeignKey(d => d.Idtk)
                .HasConstraintName("FK_Nhat_Ky_Tai_Khoan");

            entity.HasOne(d => d.IdtrangthaiNavigation).WithMany(p => p.NhatKies)
                .HasForeignKey(d => d.Idtrangthai)
                .HasConstraintName("FK_Nhat_Ky_Trang_Thai");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.Idtk);

            entity.ToTable("Tai_Khoan");

            entity.Property(e => e.Idtk)
                .ValueGeneratedOnAdd()
                .HasColumnName("IDTK");
            entity.Property(e => e.Active).HasColumnName("ACTIVE");
            entity.Property(e => e.Diachi)
                .HasMaxLength(200)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Honguoidung)
                .HasMaxLength(10)
                .HasColumnName("HONGUOIDUNG");
            entity.Property(e => e.Mk)
                .HasMaxLength(50)
                .HasColumnName("MK");
            entity.Property(e => e.Ngaychinhsua)
                .HasColumnType("datetime")
                .HasColumnName("NGAYCHINHSUA");
            entity.Property(e => e.Ngaytao)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Nguoichinhsua)
                .HasMaxLength(50)
                .HasColumnName("NGUOICHINHSUA");
            entity.Property(e => e.Nguoitao)
                .HasMaxLength(50)
                .HasColumnName("NGUOITAO");
            entity.Property(e => e.Sdtnguoithan).HasColumnName("SDTNGUOITHAN");
            entity.Property(e => e.Sodienthoai).HasColumnName("SODIENTHOAI");
            entity.Property(e => e.Tennguoidung)
                .HasMaxLength(50)
                .HasColumnName("TENNGUOIDUNG");
            entity.Property(e => e.Tentk)
                .HasMaxLength(50)
                .HasColumnName("TENTK");
        });

        modelBuilder.Entity<TrangThai>(entity =>
        {
            entity.HasKey(e => e.Idtrangthai);

            entity.ToTable("Trang_Thai");

            entity.Property(e => e.Idtrangthai)
               .ValueGeneratedOnAdd()
                .HasColumnName("IDTRANGTHAI");
            entity.Property(e => e.Mota)
                .HasMaxLength(100)
                .HasColumnName("MOTA");
            entity.Property(e => e.Tentrangthai)
                .HasMaxLength(50)
                .HasColumnName("TENTRANGTHAI");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
