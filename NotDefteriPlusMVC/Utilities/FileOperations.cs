namespace NotDefteriPlusMVC
{
    public static class FileOperations
    {
        //public static string UploadFile(IFormFile file, string folderPath)
        //{
        //    // dosya adı
        //    var fileName = Path.GetFileName(file.FileName);
        //    // dosya yolu
        //    var filePath = Path.Combine(folderPath, fileName);
        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        file.CopyTo(stream);
        //    }
        //    return fileName;
        //}
        //public static void DeleteFile(string filePath)
        //{
        //    if (System.IO.File.Exists(filePath))
        //    {
        //        System.IO.File.Delete(filePath);
        //    }
        //}

        public static string UploadImage(IFormFile imageName, string folderPath = "wwwroot/images/")
        {
            string guid = Guid.NewGuid().ToString();
            string fileName = guid + "_" + imageName.FileName;
            string filePath = folderPath + fileName; // çok fazla resim olduğunda db de her seferinde bu path adı geçecek. gerek yok sadece dosya adı yeterli. bu da sıkıntı. aynı isimli dosya yüklenebilir farklı kullanıcılar tarafından. bu sefer sonradan eklenen önceki resmin üstüne yazar. veri kaybı olur. bu sebeple guid tanımlamak en iyisidir.
            FileStream fileStream = new FileStream(filePath, FileMode.Create); // fileMode.Create yeni oluşturulması gerektiğini söylemiş oluyoruz.
            // imageName.CopyTo(new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageName.FileName), FileMode.Create));
            imageName.CopyTo(fileStream);
            fileStream.Close();
            return fileName;

            // File.Delete(filePath); // dosyayı silmek için (çok tercih edilmez silme işlemi.)
        }
    }
}
