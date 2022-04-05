using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using PhoneGuide.Reports.Services.Abstract;
using PhoneGuide.Shared.Enums;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PhoneGuide.Reports.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IReportManager _reportManager;
        private readonly IWebHostEnvironment _env;
        public FilesController(IReportManager reportManager, IWebHostEnvironment env)
        {
            _reportManager = reportManager;
            _env = env;
        }
        [HttpGet("DownloadFile/{fileName}")]
        public async Task<ActionResult> DownloadFile(string fileName)
        {
            var filePath = $"{_env.WebRootPath}/files/{fileName}";
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string fileId)
        {
            if (file is not { Length: > 0 }) return BadRequest();

            var dataResult = await _reportManager.GetByIdAsync(fileId);
            var report = dataResult.Data;
            var filePath = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", filePath);
            
            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);

            report.CreatedDate = DateTime.Now.ToString();
            report.FilePath = filePath;
            report.ReportState = ReportState.Completed;

            await _reportManager.UpdateAsync(report);

            return Ok();
        }
    }
}
