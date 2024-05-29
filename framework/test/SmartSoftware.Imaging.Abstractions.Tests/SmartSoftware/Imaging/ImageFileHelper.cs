using System;
using System.IO;

namespace SmartSoftware.Imaging;

public static class ImageFileHelper
{
    public static Stream GetJpgTestFileStream()
    {
        return GetTestFileStream("ss.jpg");
    }
    
    public static Stream GetPngTestFileStream()
    {
        return GetTestFileStream("ss.png");
    }
    
    public static Stream GetWebpTestFileStream()
    {
        return GetTestFileStream("ss.webp");
    }
    
    private static Stream GetTestFileStream(string fileName)
    {
        var assembly = typeof(ImageFileHelper).Assembly;
        var resourceStream = assembly.GetManifestResourceStream("SmartSoftware.Imaging.Files." + fileName);
        if (resourceStream == null)
        {
            throw new Exception($"File {fileName} does not exists!");
        }
        
        return resourceStream;
    }
}