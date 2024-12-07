using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AllUpProject.Utils
{
    public static class  FileValidator
    {
        public static string BuildPath(string fileName, params string[] roots)
        {
            string path = string.Empty;
            for (int i = 0; i < roots.Length; i++)
            {
                path = Path.Combine(path, roots[i]);
            }

            path = Path.Combine(path, fileName);
            return path;
        }

        public static async Task<string> CreateFileAsync(this IFormFile file, params string[] roots)
        {
            string originalFileName = file.FileName;
            int lastDotIndex = originalFileName.LastIndexOf('.');
            string fileExtension = originalFileName.Substring(lastDotIndex);

            string fileName = string.Concat(Guid.NewGuid().ToString(), fileExtension);

            string path = BuildPath(fileName, roots);

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }

    }
}
