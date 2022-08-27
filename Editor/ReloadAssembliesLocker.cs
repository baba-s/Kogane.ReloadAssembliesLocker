using UnityEditor;

namespace Kogane.Internal
{
    [InitializeOnLoad]
    internal static class ReloadAssembliesLocker
    {
        private static bool m_isLock;

        static ReloadAssembliesLocker()
        {
            EditorApplication.delayCall += () => UpdateChecked();

            void OnChanged( PlayModeStateChange change )
            {
                if ( change != PlayModeStateChange.ExitingEditMode ) return;
                Unlock();
            }

            EditorApplication.playModeStateChanged += OnChanged;
        }

        [MenuItem( "Kogane/Reload Assemblies/Lock" )]
        private static void Lock()
        {
            if ( m_isLock ) return;
            m_isLock = true;
            EditorApplication.LockReloadAssemblies();
            UpdateChecked();
        }

        [MenuItem( "Kogane/Reload Assemblies/Unlock" )]
        private static void Unlock()
        {
            if ( !m_isLock ) return;
            m_isLock = false;
            EditorApplication.UnlockReloadAssemblies();
            UpdateChecked();
        }

        private static void UpdateChecked()
        {
            Menu.SetChecked( "Kogane/Reload Assemblies/Lock", m_isLock );
            Menu.SetChecked( "Kogane/Reload Assemblies/Unlock", !m_isLock );
        }
    }
}