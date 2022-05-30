using System;
using System.Collections;
using CrazyShooter.Signals;
using SMC.Tools.Coroutines;
using SMC.Windows;
using UnityEngine.SceneManagement;
using Zenject;
using CrazyShooter.Enum;

namespace CrazyShooter.System
{
    public class SceneTransitionSystem : IInitializable, IDisposable
    {
        [Inject] private WindowsManager _windowsManager;
        [Inject] private SignalBus _signalBus;

        private Action _currentLoadAction;

        public void Initialize()
        {
            WindowsManager.WindowStateChangedEvent += OnWindowStateChange;
            _signalBus.Subscribe<LoadedSceneInitializedSignal>(OnLoadSceneInitialized);
        }

        public void Dispose()
        {
            WindowsManager.WindowStateChangedEvent -= OnWindowStateChange;
            _signalBus.TryUnsubscribe<LoadedSceneInitializedSignal>(OnLoadSceneInitialized);
        }

        private void OnWindowStateChange(Type windowType, WindowState state)
        {
            if (windowType != typeof(SceneTransitionWindow))
                return;

            if (state != WindowState.Opened)
                return;

            InvokeLoadAction();
        }

        private void InvokeLoadAction()
        {
            _currentLoadAction?.Invoke();
            _currentLoadAction = null;
        }

        private void OnLoadSceneInitialized()
        {
            CloseTransitionWindow();
        }

        private void CloseTransitionWindow()
        {
            _windowsManager.Close<SceneTransitionWindow>();
        }

        public void GoToScene(SceneType scene, bool closeAllWindow = true, bool withTransitionScene = true,
            bool closeTransitionScene = true, bool currentUnload = true)
        {
            _currentLoadAction = () =>
            {
                var loadMode = currentUnload ? LoadSceneMode.Single : LoadSceneMode.Additive;
                Coroutines.Instance.StartCoroutine(LoadSceneAsync(scene, loadMode, closeTransitionScene));
            };

            if (closeAllWindow)
                _windowsManager.CloseAll();

            if (withTransitionScene)
                _windowsManager.Open<SceneTransitionWindow>();
            else
                InvokeLoadAction();
        }

        private IEnumerator LoadSceneAsync(SceneType scene, LoadSceneMode mode, bool closeTransitionScene)
        {
            var load = SceneManager.LoadSceneAsync((int)scene, mode);
            load.allowSceneActivation = false;

            while (true)
            {
                yield return null;
                if (load.progress >= .9f)
                    break;
            }

            load.allowSceneActivation = true;
            
            if(closeTransitionScene)
                _windowsManager.Close<SceneTransitionWindow>();
        }
    }
}