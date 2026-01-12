using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Pharam_System___V6.CodeFuncation
{
    public class UploadCode
    {
        private readonly IWebHostEnvironment webHost;

        public UploadCode(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }
        //Upload File
        public string UploadFun(IFormFile file, string folder)
        {
            if (file != null)
            {
                var fileDir = Path.Combine(webHost.ContentRootPath, folder);
                var fileName = Guid.NewGuid() + "_" + file.FileName;
                var filePath = Path.Combine(fileDir, fileName);

                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                    return fileName;
                }
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
