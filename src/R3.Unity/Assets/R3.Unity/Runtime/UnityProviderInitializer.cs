using System;
using UnityEngine;

[assembly: UnityEngine.Scripting.AlwaysLinkAssembly]

namespace R3
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public static class UnityProviderInitializer
    {
        static Action<Exception> unhandledExceptionHandler = UnhandledExceptionHandler;

        static void UnhandledExceptionHandler(Exception ex)
        {
            UnityEngine.Debug.LogException(ex);
        }

        static UnityProviderInitializer()
        {
            SetDefaultObservableSystem();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void SetDefaultObservableSystem()
        {
            SetDefaultObservableSystem(unhandledExceptionHandler);
        }

        public static void SetDefaultObservableSystem(Action<Exception> unhandledExceptionHandler)
        {
            ObservableSystem.RegisterUnhandledExceptionHandler(unhandledExceptionHandler);
            ObservableSystem.DefaultTimeProvider = UnityTimeProvider.Update;
            ObservableSystem.DefaultFrameProvider = UnityFrameProvider.Update;
        }
    }
}
