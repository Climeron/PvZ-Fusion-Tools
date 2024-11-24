using System;
using System.Linq;
using System.Reflection;
using ClimeronToolsForPvZ.Extensions;
using HarmonyLib;

namespace ClimeronToolsForPvZ.Classes
{
    public static class UnityExplorerIntegration
    {
        private static Assembly _unityExplorerAssembly;
        private const string _EXPECTED_ASSEMBLY_NAME = "UnityExplorer.ML.IL2CPP.net6preview.interop";
        private static Type _unityExplorerManagerType;
        private const string _MANAGER_TYPE_NAME = "UnityExplorer.UI.UIManager";
        private static PropertyInfo _showMenuProperty;
        private const string _SHOW_MENU_PROPERTY_NAME = "ShowMenu";
        public static bool ShowMenu => (bool)_showMenuProperty.GetValue(null);
        private static MethodInfo _showMenu_setter;
        public static event Action<bool> onShowMenuChanged;
        private static MethodInfo _initUI;
        private const string _INIT_UI_METHOD_NAME = "InitUI";
        public static bool Initialized { get; private set; }

        public static void Initialize()
        {
            if (!FirstAttemptToFindAssembly())
                if (!SecondAttemptToFindAssembly())
                    return;
            if (!DefineUnityExplorerManagerType())
                return;
            if (!DefineIsOpenedProperty())
                return;
            if (!DefineShowMenuSetter())
                return;
            if (!DefineInitUIMethod())
                return;
            PatchMembers();
            Initialized = true;
        }
        #region Initialization
        private static bool FirstAttemptToFindAssembly()
        {
            _unityExplorerAssembly = AppDomain.CurrentDomain.GetAssemblies()
               .FirstOrDefault(assembly => assembly.GetName().Name == _EXPECTED_ASSEMBLY_NAME);
            return _unityExplorerAssembly != null;
        }
        private static bool SecondAttemptToFindAssembly()
        {
            _unityExplorerAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(assembly => assembly.GetName().Name.Contains("UnityExplorer"));
            if (_unityExplorerAssembly != null)
                $"Found an assembly {_unityExplorerAssembly.GetName().Name} that different from the expected - {_EXPECTED_ASSEMBLY_NAME}.".Print();
            return _unityExplorerAssembly != null;
        }
        private static bool DefineUnityExplorerManagerType()
        {
            _unityExplorerManagerType = _unityExplorerAssembly.GetType(_MANAGER_TYPE_NAME);
            if (_unityExplorerManagerType == null)
                $"Unable to find type <{_MANAGER_TYPE_NAME}>.".PrintError<TypeAccessException>();
            return _unityExplorerManagerType != null;
        }
        private static bool DefineIsOpenedProperty()
        {
            _showMenuProperty = _unityExplorerManagerType.GetProperty(_SHOW_MENU_PROPERTY_NAME, BindingFlags.Public | BindingFlags.Static);
            if (_showMenuProperty == null)
                $"Unable to find property '{_SHOW_MENU_PROPERTY_NAME}'.".PrintError<MissingMemberException>();
            return _showMenuProperty != null;
        }
        private static bool DefineShowMenuSetter()
        {
            _showMenu_setter = _showMenuProperty.GetSetMethod();
            if (_showMenu_setter == null)
                $"Unable to get 'SetMethod' of property '{_SHOW_MENU_PROPERTY_NAME}'.".PrintError<MissingMemberException>();
            return _showMenu_setter != null;
        }
        private static bool DefineInitUIMethod()
        {
            _initUI = _unityExplorerManagerType.GetMethod(_INIT_UI_METHOD_NAME, BindingFlags.NonPublic | BindingFlags.Static);
            if (_initUI == null)
                $"Unable to find method '{_INIT_UI_METHOD_NAME}'.".PrintError<MissingMemberException>();
            return _initUI != null;
        }
        #endregion
        #region Patching
        private static void PatchMembers()
        {
            Main.Instance.HarmonyInstance.Patch(_showMenu_setter, prefix: new HarmonyMethod(typeof(UnityExplorerIntegration).GetMethod(nameof(PreShowMenuSetter))));
            Main.Instance.HarmonyInstance.Patch(_initUI, postfix: new HarmonyMethod(typeof(UnityExplorerIntegration).GetMethod(nameof(PostInitUI))));
        }
        public static bool PreShowMenuSetter(bool value)
        {
            onShowMenuChanged?.Invoke(value);
            return true;
        }
        public static void PostInitUI()
        {
            onShowMenuChanged?.Invoke(ShowMenu);
        }
        #endregion
    }
}
