namespace StudentEnrollment.Api.Services;

public class FileUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) : IFileUpload
{
    private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

    public string UploadStudentFile(byte[] file, string imageName)
    {
        string studentFileUrl = string.Empty;

        if (file != null)
        {
            string folderPath = "studentpictures";
            string? url = httpContextAccessor.HttpContext?.Request.Host.Value;
            string extension = Path.GetExtension(imageName);
            string fileName = $"{Guid.NewGuid()}{extension}";

            string path = $"{webHostEnvironment.WebRootPath}\\{folderPath}\\{fileName}";
            UploadImage(file, path);
            studentFileUrl = $"https://{url}/{folderPath}/{fileName}";
        }

        return studentFileUrl;
    }

    private static void UploadImage(byte[] fileBytes, string filePath)
    {
        FileInfo file = new(filePath);
        file?.Directory?.Create();

        using FileStream? fileStream = file?.Create();
        fileStream?.Write(fileBytes, 0, fileBytes.Length);
    }
}
