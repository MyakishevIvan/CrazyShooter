using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField]
    private MenuController menuController;
    public override void InstallBindings()
    {
        Container.Bind<MenuController>().FromInstance(menuController).AsSingle();

    }
}