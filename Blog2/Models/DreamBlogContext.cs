using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blog2.Models;

public partial class DreamBlogContext : DbContext
{
    public DreamBlogContext()
    {
    }

    public DreamBlogContext(DbContextOptions<DreamBlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

   // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DreamBlog;User ID=sa;pwd=123;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__BlogPost__AA126018B426CBF2");

            entity.ToTable("BlogPost");

            entity.Property(e => e.CategoryId).HasComment("分类ID");
            entity.Property(e => e.Content)
                .HasComment("文章内容")
                .HasColumnType("text");
            entity.Property(e => e.CreatedAt)
                .HasComment("文章发布时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Likes).HasComment("点赞数");
            entity.Property(e => e.Tags).HasColumnType("text");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("文章标题");
            entity.Property(e => e.UserId).HasComment("外键用户id");
            entity.Property(e => e.Views).HasComment("浏览量");

            entity.HasOne(d => d.Category).WithMany(p => p.BlogPosts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_BlogPost_CID");

            entity.HasOne(d => d.User).WithMany(p => p.BlogPosts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__BlogPost__UserId__398D8EEE");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B57E8ABC9");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("分类名");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFCA24242C83");

            entity.ToTable("Comment");
            entity.Property(e => e.Content)
           .HasColumnType("nvarchar(max)"); // 将评论内容映射到 nvarchar(max) 数据库列类型
         
            entity.Property(e => e.CreatedAt)
                .HasComment("评论时间")
                .HasColumnType("datetime");
            entity.Property(e => e.PostId).HasComment("评论文章ID");
            entity.Property(e => e.UserId).HasComment("评论用户ID");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Comment__PostId__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Comment__UserId__403A8C7D");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tag__657CF9AC75B0F725");

            entity.ToTable("Tag");

            entity.Property(e => e.TagId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasComment("用户创建时间")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasComment("电子邮件地址");
            entity.Property(e => e.ImgUrl).HasColumnType("text");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasComment("密码");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasComment("用户名");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
