using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog2.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 评论用户ID
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// 评论文章ID
    /// </summary>
    public int? PostId { get; set; }

    /// <summary>
    /// 评论时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    [JsonIgnore]
    public virtual BlogPost? Post { get; set; }
    [JsonIgnore]
    public virtual User? User { get; set; }
}
