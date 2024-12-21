using System.Threading.Tasks;

namespace Miwen.Abp.OssManagement;

public interface IFileValidater
{
    Task ValidationAsync(UploadFile input);
}
