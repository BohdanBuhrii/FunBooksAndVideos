namespace EShop.Interfaces
{
  /// <summary>
  /// Interface to save files to the storage
  /// </summary>
  public interface IFilesStorage
  {
    Task SaveFile(string location, string fileName, string content);
  }
}
