using Telegram.Bot.Types;

namespace Portfolio.WebApi.Helpers
{
    public class ImageHelper
    {
        public static string MakeImageName(string fileName)
        {
            string guid = Guid.NewGuid().ToString();
            return "IMG_" + guid + fileName;
        }
        public static string MakeLogoAsync(string fileName)
        {
            return $"CV_{Guid.NewGuid():N}_{fileName}";
        }
    }
}
