using System.ComponentModel;
using CrazyShooter.Signals;
using CrazyShooter.System;
using SMC.Profile;
using SMC.Tools.Events;
using SMC.Windows;
using UnityEngine;
using  Zenject;
public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private WindowsManager _windowsManager;
    public override void InstallBindings()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = false;
    }

    private void DeclareSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<LoadedSceneInitializedSignal>();
    }

    private void BindSystem()
    {
        Container.Bind<WindowsManager>().FromComponentInNewPrefab(_windowsManager).AsSingle().NonLazy();
        WindowsManager.CustomWindowInstantiator = CustomWindowInstantiation;
        Container.BindInterfacesAndSelfTo<SceneTransitionSystem>().AsSingle().NonLazy();

    }
    
    private BaseWindow CustomWindowInstantiation(BaseWindow windowPrefab)
    {
        var parent = Container.Resolve<WindowsManager>().transform;
        var window = Container.InstantiatePrefabForComponent<BaseWindow>(windowPrefab, parent);
        Container.Inject(window.gameObject);
        return window;
    }

    private void CreateProfileManagers()
    {
        var profileController = SMC.Profile.ProfileController.Instance;
        profileController.Init();
        
        
        
        
        InitializeProfileController(profileController);
    }

    private void BindProfileManager<T>(ProfileController profileController) where T : ProfileManager, new()
    {
        Container
            .BindInterfacesAndSelfTo<T>()
            .FromMethod(context =>
            {
                var instance = profileController.CreateProfileManager<T>();
                context.Container.Inject(instance);
                return instance; 
            })
            .AsSingle()
            .NonLazy();
    }

    private static void InitializeProfileController(ProfileController profileController)
    {
        // profileController.Init();

        UnityEventsObserver.Instance.OnApplicationPauseEvent += pause =>
        {
            if (pause)
                profileController.Save();
        };
        
#if UNITY_EDITOR
        UnityEventsObserver.Instance.OnApplicationQuitEvent += profileController.Save;
#endif
    }
}
