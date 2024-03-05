using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Blog2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        

        [HttpPost]
        public async Task<string> UploadFile(IFormFile file)
        {
            ResultModel result = new ResultModel();
            if (!new[] { "image/jpeg", "image/png" }.Contains(file.ContentType))
            {
                result.code = 2;
                result.msg = "图片仅支持jpg和png格式";
                return JsonConvert.SerializeObject(result);//引用Newtonsoft库
            }

            if (file is { Length: > 0 })
            {
                try
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string staticFileRoot = "wwwroot";
                    // 这里是文件路径，不包含文件名
                    string fileUrlWithoutFileName = @$"uploadImg\UserImg";
                    // 创建文件夹，如果文件夹已存在，则什么也不做
                    Directory.CreateDirectory($"{staticFileRoot}/{fileUrlWithoutFileName}");

                    // 使用哈希的原因是前端可能传递相同的文件，服务端不想保存多个相同的文件
                    SHA256 hash = SHA256.Create();
                    // 读取文件的流 把文件流转为哈希值
                    byte[] hashByte = await hash.ComputeHashAsync(file.OpenReadStream());
                    // 再把哈希值转为字符串 当作文件的文件名
                    string hashedFileName = BitConverter.ToString(hashByte).Replace("-", "");

                    // 重新获得一个文件名
                    string newFileName = hashedFileName + "." + fileName.Split('.').Last();
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), $@"{staticFileRoot}\{fileUrlWithoutFileName}", newFileName);
                    await using var fileStream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fileStream);
                    result.code = 1;
                    result.msg = Path.Combine(fileUrlWithoutFileName, newFileName);
                    return JsonConvert.SerializeObject(result);
                }
                catch (Exception e)
                {
                    result.code = 2;
                    result.msg = e.Message;
                    return JsonConvert.SerializeObject(result);
                }
            }
            result.code = 2;
            result.msg = "请上传文件";
            return JsonConvert.SerializeObject(result);

        }
    }
}
