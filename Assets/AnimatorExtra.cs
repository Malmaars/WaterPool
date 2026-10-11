using UnityEngine;

public class AnimatorExtra : MonoBehaviour
{
    public ParticleSystem[] particleSystems;

    public void PlayParticles(int _index)
    {
        if(_index > particleSystems.Length - 1)
        {
            Debug.LogError("particlesystem not assigned with an index of " + _index);
        }

        if (particleSystems[_index].isPlaying)
            particleSystems[_index].Stop();
        particleSystems[_index].Play();
    }
}
