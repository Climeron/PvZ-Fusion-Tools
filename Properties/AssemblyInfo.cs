using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;

[assembly: MelonInfo(typeof(ClimeronToolsForPvZ.Main), ClimeronToolsForPvZ.AssemblyInfo.MODE_NAME, ClimeronToolsForPvZ.AssemblyInfo.VERSION, ClimeronToolsForPvZ.AssemblyInfo.AUTHOR)]

// Общие сведения об этой сборке предоставляются следующим набором
// набора атрибутов. Измените значения этих атрибутов для изменения сведений,
// связанные со сборкой.
[assembly: AssemblyTitle("ClimeronToolsForPvZ")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ClimeronToolsForPvZ")]
[assembly: AssemblyCopyright("Copyright ©  2024")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Установка значения False для параметра ComVisible делает типы в этой сборке невидимыми
// для компонентов COM. Если необходимо обратиться к типу в этой сборке через
// COM, задайте атрибуту ComVisible значение TRUE для этого типа.
[assembly: ComVisible(false)]

// Следующий GUID служит для идентификации библиотеки типов, если этот проект будет видимым для COM
[assembly: Guid("f41d33bb-ab29-4454-bf85-73ace7f06636")]

// Сведения о версии сборки состоят из указанных ниже четырех значений:
//
//      Основной номер версии
//      Дополнительный номер версии
//      Номер сборки
//      Редакция
//
// Можно задать все значения или принять номера сборки и редакции по умолчанию 
// используя "*", как показано ниже:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(ClimeronToolsForPvZ.AssemblyInfo.VERSION)]
[assembly: AssemblyFileVersion(ClimeronToolsForPvZ.AssemblyInfo.VERSION)]

namespace ClimeronToolsForPvZ
{
    public static class AssemblyInfo
    {
        public const string MODE_NAME = nameof(ClimeronToolsForPvZ);
        public const string VERSION = "214.0.1";
        public const string AUTHOR = "Climeron";
    }
}