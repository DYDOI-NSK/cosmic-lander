using UnityEngine;

public class LanderEngine : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem leftParticParticleSystem;

    [SerializeField]
    private ParticleSystem middleParticParticleSystem;

    [SerializeField]
    private ParticleSystem rightParticParticleSystem;

    private Lander lander;

    private void Awake()
    {
        DisableParticleSystem();
        lander = GetComponent<Lander>();

        lander.OnForceUp += Lander_OnForceUp;
        lander.OnForceLeft += Lander_OnForceLeft;
        lander.OnForceRight += Lander_OnForceRight;

        lander.OnBeforeForce += Lander_OnBeforeForce;
    }

    private void Lander_OnForceUp(object _, System.EventArgs __)
    {
        EnableParticleSystem();
    }

    private void Lander_OnBeforeForce(object _, System.EventArgs __)
    {
        DisableParticleSystem();
    }

    private void Lander_OnForceRight(object _, System.EventArgs __)
    {
        setParticleSystemEnabled(leftParticParticleSystem, true);
    }

    private void Lander_OnForceLeft(object _, System.EventArgs __)
    {
        setParticleSystemEnabled(rightParticParticleSystem, true);
    }

    private void EnableParticleSystem()
    {
        setParticleSystemEnabled(leftParticParticleSystem, true);
        setParticleSystemEnabled(middleParticParticleSystem, true);
        setParticleSystemEnabled(rightParticParticleSystem, true);
    }

    private void DisableParticleSystem()
    {
        setParticleSystemEnabled(leftParticParticleSystem, false);
        setParticleSystemEnabled(middleParticParticleSystem, false);
        setParticleSystemEnabled(rightParticParticleSystem, false);
    }

    private void setParticleSystemEnabled(ParticleSystem system, bool enabled)
    {
        ParticleSystem.EmissionModule emission = system.emission;
        emission.enabled = enabled;
    }
}
