using VContainer;
using VContainer.Unity;
using UnityEngine;

public class BattleLifetimeScope : LifetimeScope
{
    [SerializeField] private MouseWorld mouseWorld;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(mouseWorld);
    }
}
