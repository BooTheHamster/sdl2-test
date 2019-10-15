# sdl2-test
Приложение для изучения библиотеки SDL2 

# Возможные проблемы и способы их устранения:
При сборке из командной строки возникает ошибка: "The SDK 'Microsoft.NET.Sdk.Web' specified could not be found."
Создать переменную среды MSBuildSDKsPath в которой указать путь до Net Core SDK, например, "C:\Program Files\dotnet\sdk\3.0.100\Sdks".
