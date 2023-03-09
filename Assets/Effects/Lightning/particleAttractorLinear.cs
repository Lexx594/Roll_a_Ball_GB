using System.Collections;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class particleAttractorLinear : MonoBehaviour {
	ParticleSystem ps;
	ParticleSystem.Particle[] m_Particles;
	private GameObject _player;
	public float speed = 5f;
	int numParticlesAlive;
	void Start () {
        _player = GameObject.Find("Player").gameObject;
        ps = GetComponent<ParticleSystem>();
		if (!GetComponent<Transform>()){
			GetComponent<Transform>();
		}
	}
	void Update () {
		m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
		numParticlesAlive = ps.GetParticles(m_Particles);
		float step = speed * Time.deltaTime;
		for (int i = 0; i < numParticlesAlive; i++) {
			m_Particles[i].position = Vector3.LerpUnclamped(m_Particles[i].position, _player.transform.position, step);
		}
		ps.SetParticles(m_Particles, numParticlesAlive);
	}
}
