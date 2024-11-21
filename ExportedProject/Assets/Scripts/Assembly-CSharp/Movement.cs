using UnityEngine;

public class Movement : MonoBehaviour
{
	private Rigidbody rb;

	[SerializeField]
	private float playerForce;

	[SerializeField]
	private float rotationForce;

	private AudioSource audioSource;

	[SerializeField]
	private AudioClip engineClip;

	public ParticleSystem bottleEngineParticle;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		ApplyingPlayerForce();
		if (Input.GetKey(KeyCode.A))
		{
			PlayerRotation(rotationForce);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			PlayerRotation(0f - rotationForce);
		}
	}

	private void PlayerRotation(float rotationForce)
	{
		rb.freezeRotation = true;
		base.transform.Rotate(0f, 0f, rotationForce * Time.deltaTime);
		rb.freezeRotation = false;
	}

	private void ApplyingPlayerForce()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			rb.AddRelativeForce(Vector3.up * playerForce * Time.deltaTime);
			if (!bottleEngineParticle.isPlaying)
			{
				bottleEngineParticle.Play();
			}
			if (!audioSource.isPlaying)
			{
				audioSource.PlayOneShot(engineClip);
			}
		}
		else
		{
			bottleEngineParticle.Stop();
			audioSource.Stop();
		}
	}
}
