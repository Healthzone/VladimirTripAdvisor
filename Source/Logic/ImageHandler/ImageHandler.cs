namespace VladimirTripAdvisor.Logic.ImageHandler
{
    public class ImageHandler
    {

        public async Task<byte[]> ConvertFileToByteArray(IFormFile file)
        {
            long length = file.Length;
            if (length <= 0)
            {
                return null;
            }
            using var fileStream = file.OpenReadStream();
            byte[] bytes = new byte[length];
            await fileStream.ReadAsync(bytes, 0, (int)file.Length);

            return bytes;
        }
    }
}
