namespace DevFreela.Infrastructure.CloudServices.Interfaces;

public interface IFileStorageService
{
    void UploadFile(byte[] file, string fileName);
}