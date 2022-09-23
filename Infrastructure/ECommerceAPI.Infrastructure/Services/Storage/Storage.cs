using ECommerceAPI.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);

        protected async Task<string> FileRenameAsync(string pathOrContainerName, HasFile hasFileMethod, string fileName, int num = 1)
        {
            return await Task.Run(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldName = $"{Path.GetFileNameWithoutExtension(fileName)}-{num}";
                string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";

                //if (File.Exists($"{path}\\{newFileName}"))
                if (hasFileMethod(pathOrContainerName, newFileName))
                {
                    return await FileRenameAsync(pathOrContainerName, hasFileMethod, $"{newFileName.Split($"-{num}")[0]}{extension}", ++num);
                }
                return newFileName;
            });
        }
    }
}
