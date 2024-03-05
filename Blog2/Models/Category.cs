using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog2.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    /// <summary>
    /// 分类名
    /// </summary>
    public string? Name { get; set; }
    [JsonIgnore]
    public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
}
