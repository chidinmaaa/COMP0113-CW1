using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.Spawning;
using Ubiq.Geometry;


public class FireworksFinalRoom : MonoBehaviour, INetworkSpawnable
{
    private Rigidbody[] fireworks;
    private ParticleSystem[] particles;

    public NetworkId NetworkId { get; set; }

    public bool owner;  // remove
    public bool fired = false;

    private NetworkContext context;
    private Vector3 flightForce;
    private float explodeTime;

    private void Awake()
    {
        fireworks = GetComponentsInChildren<Rigidbody>();
        particles = GetComponentsInChildren<ParticleSystem>();
        owner = true;
    }

    private void Start()
    {
        context = NetworkScene.Register(this);
    }

    public void LaunchFireworks()
    {
        flightForce = new Vector3(
            x: (Random.value - 0.5f) * 0.05f,
            y: 3.0f,
            z: (Random.value - 0.5f) * 0.05f);
        explodeTime = Time.time + 10.0f;
    }

    private void FixedUpdate()
    {
        if (owner && fired)
        {

            foreach (ParticleSystem particle in particles)
            {
                if (!particle.isPlaying)
                {
                    particle.Play();
                }
            }
            //foreach (Rigidbody body in fireworks)
            //{
            //    body.isKinematic = false;
            //    body.AddForce(flightForce, ForceMode.Force);
            //}

           
            //foreach (Rigidbody body in fireworks)
            //{
            //    body.AddForce(transform.up * 2.0f, ForceMode.Impulse);
            //}

            if (Time.time > explodeTime)
            {
                NetworkSpawnManager.Find(this).Despawn(gameObject);
                return;
            }

            if (!owner && fired)
            {
                foreach (ParticleSystem particle in particles)
                {
                    if (!particle.isPlaying)
                    {
                        particle.Play();
                    }
                }
            }
        }
    }
}