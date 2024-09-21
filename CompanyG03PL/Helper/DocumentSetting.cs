namespace CompanyG03PL.Helper
{
    public class DocumentSetting
    {
        //Upload
        public static string UploadFile(IFormFile file,string folderName)
        {
            //1- GEt location of the folder
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName); /*Directory.GetCurrentDirectory()+ @"wwwroot\Files"+folderName;*/
            //2- get file name make it unique
            string filename=$"{Guid.NewGuid().ToString()}{file.FileName}";
            //3- Get path --> folder path+filename

            string filePath=Path.Combine(folderPath, filename);

            //4- Save File as stream  : Data per time

            using var fileStream=new FileStream(filePath,FileMode.Create);
            file.CopyTo(fileStream);
            return filename;



        }

         




        //Delete

        public static   void DeleteFile(string fileName,string folderName)
        {
            //1- GEt location of the folder
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName,fileName); /*Directory.GetCurrentDirectory()+ @"wwwroot\Files"+folderName;*/

            if (File.Exists(filePath)) {
              File.Delete(filePath);
            
            
            }



        }




    }
}
