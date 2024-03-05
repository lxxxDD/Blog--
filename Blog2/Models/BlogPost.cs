using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog2.Models;

public partial class BlogPost
{
    public int PostId { get; set; }

    /// <summary>
    /// 文章标题
    /// </summary>
    public string? Title { get; set; }

    public string? Tags { get; set; }

    /// <summary>
    /// 分类ID
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// 文章内容
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 外键用户id
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// 点赞数
    /// </summary>
    public int? Likes { get; set; }

    /// <summary>
    /// 浏览量
    /// </summary>
    public int? Views { get; set; }

    /// <summary>
    /// 文章发布时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    [JsonIgnore]
    public virtual Category? Category { get; set; }
    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    [JsonIgnore]
    public virtual User? User { get; set; }
}
