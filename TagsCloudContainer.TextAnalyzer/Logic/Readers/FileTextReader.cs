using TagsCloudContainer.Core;
using TagsCloudContainer.TextAnalyzer.Logic.Readers.Interfaces;

namespace TagsCloudContainer.TextAnalyzer.Logic.Readers;

internal sealed class FileTextReader : IFileTextReader
{
    public Result<string> ReadText(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new Error($"Файл по указанному пути '{filePath}' не существует.");
        }

        try
        {
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            using var reader = new StreamReader(fileStream);
            return reader.ReadToEnd();
        }
        catch (FileNotFoundException)
        {
            return new Error($"Файл по указанному пути '{filePath}' не найден");
        }
        catch (IOException ex)
        {
            return new Error($"Ошибка ввода-вывода при чтении файла '{filePath}': {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            return new Error($"Нет доступа к файлу '{filePath}': {ex.Message}");
        }
        catch (Exception ex)
        {
            return new Error($"Неизвестная ошибка при чтении файла '{filePath}': {ex.Message}");
        }
    }
}