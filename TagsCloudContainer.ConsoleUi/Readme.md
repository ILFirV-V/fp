## Мануал по использованию консольного приложения Tags Cloud Container

Приложение позволяет генерировать облака тегов из текстовых файлов с возможностью настройки различных параметров.

### Подробное описание команд и опций

1. настройки путей и формата файлов

    - Описание: Позволяет настроить пути к файлам, используемые приложением.
    - Синтаксис:

      `<название exe файла> -i <путь_к_входному_файлу> -o <путь_к_выходному_файлу> -n <имя_выходного_файла> [-e <формат_изображения>]`

    - Опции:
        - -i, --input (обязательно): Путь к текстовому файлу для анализа.
        - -o, --output (обязательно): Путь для сохранения сгенерированного изображения.
        - -n, --name (обязательно): Имя файла изображения.
        - -e, --format (необязательно): Формат выходного изображения (png, jpeg, bmp, jpg). По умолчанию используется
          формат png.
    
    - Примеры:
        - Установка входного файла, пути вывода и имени файла, формата нет

          `TagsCloudContainer.ConsoleUi.exe -i input.txt -o output_folder -n my_cloud`
        - Установка всех параметров, включая формат изображения (jpg):

          `TagsCloudContainer.ConsoleUi.exe -i input.txt -o output_folder -n my_cloud -e jpg`

2. настройки изображений

    - Описание: Позволяет настроить параметры изображения.
    - Синтаксис:

      `<настройки путей файлов> [-w <ширина>] [-h <высота>] [-b <цвет_фона>] [-c <цвет_слов>] [-f <шрифт>]`
    - Опции:
        - -w, --width (необязательно): Ширина изображения.
        - -h, --height (необязательно): Высота изображения.
        - -b, --background (необязательно): Цвет фона изображения (например, Red, Blue, Green, Black, White).
        - -c, --color (необязательно): Цвет слов (например, Red, Blue, Green, Black, White).
        - -f, --font (необязательно): Шрифт слов (например, Arial).
    - Примеры:

        - Установка ширины и высоты изображения:

          `TagsCloudContainer.ConsoleUi.exe -i input.txt -o output_folder -n my_cloud -w 800 -h 600`

        - Установка цвета фона, слов, ширины и высоты:

          `TagsCloudContainer.ConsoleUi.exe -i input.txt -o output_folder -n my_cloud -b White -c Black -w 1000 -h 800`

        - Установка цвета слов и шрифта:

          `TagsCloudContainer.ConsoleUi.exe -i input.txt -o output_folder -n my_cloud -c Red -f Arial`

3. Настройка анализа слов

    - Описание: Позволяет настроить параметры анализа слов.
    - Синтаксис:

      `<настройки путей файлов> [-p <части_речи1,части_речи2,...>]`

    - Опции:
        - -p, --parts (необязательно): Список частей речи, которые должны быть включены в анализ (через пробел).
    - Примеры:

        - Установка анализа только для существительных и глаголов:

      `TagsCloudContainer.ConsoleUi.exe -i input.txt -o output_folder -n my_cloud -p S V`

        - Установка анализа для прилагательных:

      `TagsCloudContainer.ConsoleUi.exe -i input.txt -o output_folder -n my_cloud -p A`

### Примечания

- Все команды чувствительны к регистру, не забывайте про это при вводе.
- Цвета указываются в соответствии с перечислением `System.Drawing.Color`.
- Шрифты должны быть установлены в вашей операционной системе.
- Есть заготовка слов в файле `Words.txt` в папке `TagsCloudContainer.ConsoleUi\Examples`
- Есть примеры готовых изображений с разными настройками в папке `TagsCloudContainer.ConsoleUi\Examples`
- Конфигурирование происходит при запуске приложения

### Примеры команд
1) `TagsCloudContainer.ConsoleUi.exe -i <путь_к_входному_файлу_с_файлом> -o <путь_к_выходному_файлу> -n result -e jpg`
результат как в `result.jpeg` в папке `TagsCloudContainer.ConsoleUi\Examples`
2) `TagsCloudContainer.ConsoleUi.exe -i <путь_к_входному_файлу_с_файлом> -o <путь_к_выходному_файлу> -n result -e png`
результат как в `result.png` в папке `TagsCloudContainer.ConsoleUi\Examples`
3) после настройки путей: `TagsCloudContainer.ConsoleUi.exe -i <путь_к_входному_файлу_с_файлом> -o <путь_к_выходному_файлу> -n result -p S V A`
результат как в `resultVSA.png` в папке `TagsCloudContainer.ConsoleUi\Examples`
4) после настройки путей: `TagsCloudContainer.ConsoleUi.exe -i <путь_к_входному_файлу_с_файлом> -o <путь_к_выходному_файлу> -n result -e png -p S`
результат как в `resultS.png` в папке `TagsCloudContainer.ConsoleUi\Examples`
5) две команды для получения такого же результата как в `resultImageSettings.png`: 
`TagsCloudContainer.ConsoleUi.exe -i <путь_к_входному_файлу_с_файлом> -o <путь_к_выходному_файлу> -n result -e png -p S V A ADV NUM -w 800 -h 600 -b White -c Black -f Arial`
результат как в `resultImageSettings.png` в папке `TagsCloudContainer.ConsoleUi\Examples`
6) `TagsCloudContainer.ConsoleUi.exe -i <путь_к_входному_файлу_с_файлом> -o <путь_к_выходному_файлу> -n result -e png -w 800 -h 600 -b Red -c Blue -f Arial` 
результат как в `resultRedBlue.png` в папке `TagsCloudContainer.ConsoleUi\Examples`