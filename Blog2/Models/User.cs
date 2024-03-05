using System;
using System.Collections.Generic;

namespace Blog2.Models;

public partial class User
{
    public int UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = null!;

    public string? ImgUrl { get; set; }

    /// <summary>
    /// 电子邮件地址
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// 用户创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
