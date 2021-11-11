using System;
using System.IO;

namespace TaskyJ.Globals.Data.Helpers
{
    public class ImageHelper
    {
        public static TaskyJImage Base64StringToImage(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return null;
            byte[] imageBytes = Convert.FromBase64String(base64String);
            var memStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
            memStream.Write(imageBytes, 0, imageBytes.Length);

            return new TaskyJImage();
        }

        public static string ImageToBase64(TaskyJImage sourcebitmap)
        {
            if (sourcebitmap == null)
                return string.Empty;
            string base64String = string.Empty;
            /*using (var memStream = new MemoryStream())
            {
            sourcebitmap.SetNull
            //sourcebitmap.Save(Splat.CompressedBitmapFormat.Png, 0, memStream).Wait();
            return Convert.ToBase64String(memStream.ToArray());
            }*/
            return base64String;
        }
    }
}
