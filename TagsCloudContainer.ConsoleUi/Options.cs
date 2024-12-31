using System.Drawing;
using System.Drawing.Imaging;
using CommandLine;

namespace TagsCloudContainer.ConsoleUi;

public class Options
{
    [Option('p', "parts", Default = new[] { "V", "S", "A", "ADV", "NUM" }, HelpText =
        "Валидные части речи (V - глагол, S - существительное, A - прилагательное, ADV - наречие, NUM - числительное). "
        + "Дополнительная информация: https://yandex.ru/dev/mystem/doc/ru/grammemes-values")]
    public ICollection<string> ValidSpeechParts { get; set; } = [];

    [Option('w', "width", Default = 1500, HelpText = "Ширина")]
    public int Width { get; set; }

    [Option('h', "height", Default = 1500, HelpText = "Высота")]
    public int Height { get; set; }

    [Option('b', "background", HelpText = "Цвет фона")]
    public Color BackgroundColor { get; set; } = Color.White;

    [Option('c', "color", HelpText = "Цвет слов")]
    public Color WordColor { get; set; } = Color.Black;

    [Option('f', "font", HelpText = "Шрифт")]
    public FontFamily? FontFamily { get; set; } = new("Consolas");

    [Option('i', "input", Required = true, HelpText = "Путь к файлу текста для анализа.")]
    public string InputPath { get; set; } = string.Empty;

    [Option('o', "output", Required = true, HelpText = "Путь к сохранению изображения.")]
    public string OutputPath { get; set; } = string.Empty;

    [Option('n', "name", Required = true, HelpText = "Имя файла")]
    public string OutputFileName { get; set; } = string.Empty;

    [Option('e', "extension", Default = "png", HelpText = "Формат файла (png, jpeg, bmp, jpg.)")]
    public string ImageFormatString { get; set; } = "png";

    public ImageFormat ImageFormat
    {
        get
        {
            return ImageFormatString.ToLower() switch
            {
                "png" => ImageFormat.Png,
                "jpg" => ImageFormat.Jpeg,
                "jpeg" => ImageFormat.Jpeg,
                "bmp" => ImageFormat.Bmp,
                _ => throw new ArgumentException($"Unsupported image format: {ImageFormatString}")
            };
        }
    }
}