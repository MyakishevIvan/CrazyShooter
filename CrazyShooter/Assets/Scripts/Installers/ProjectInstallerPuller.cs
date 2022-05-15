using UnityEngine;
using Zenject;

namespace CrazyShooter.Installers
{
    public class ProjectInstallerPuller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ProjectContext.Instance.EnsureIsInitialized();
        }
    }
    
}
