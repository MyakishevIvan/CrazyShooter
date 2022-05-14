using System.ComponentModel;
using CrazyShooter.Signals;
using SMC.Profile;
using SMC.Tools.Events;
using UnityEngine;
using  Zenject;
public class ProjectInstaller : MonoInstaller
{
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
