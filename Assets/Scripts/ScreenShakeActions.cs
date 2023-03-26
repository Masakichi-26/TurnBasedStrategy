using System;
using UnityEngine;
using VContainer;

public class ScreenShakeActions : MonoBehaviour
{
    private ScreenShake screenShake;
    
    [Inject]
    private void Construct(ScreenShake screenShake)
    {
        this.screenShake = screenShake;

        Debug.Log("injecting into Testing");
    }

    private void Start()
    {
        ShootAction.OnAnyShoot += ShootAction_OnAnyShoot;
        GrenadeProjectile.OnAnyGrenadeExploded += GrenadeProjectile_OnAnyGrenadeExploded;
    }

    private void ShootAction_OnAnyShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        screenShake.Shake();
    }

    private void GrenadeProjectile_OnAnyGrenadeExploded(object sender, EventArgs e)
    {
        screenShake.Shake(5f);
    }
}
