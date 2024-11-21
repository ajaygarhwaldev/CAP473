using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisonHandlers : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem successParticleSystem;

	[SerializeField]
	private ParticleSystem crashParticleSystem;

	[SerializeField]
	private AudioClip crashClip;

	[SerializeField]
	private AudioClip SuccessClip;

	private Movement movementScript;

	private int currentSceneIndex;

	private bool isTransitioning;

	[SerializeField]
	private float timeBetweenLevels;

	private AudioSource audioSource;

	private void Start()
	{
		movementScript = GetComponent<Movement>();
		currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		audioSource = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!isTransitioning)
		{
			switch (collision.gameObject.tag)
			{
			case "Finish":
				SuccessSequence();
				break;
			case "Friendly":
				Debug.Log("We're touching Something Friendly");
				break;
			case "Room":
				Debug.Log("We're touching the room");
				break;
			default:
				CrashSequence();
				break;
			}
		}
	}

	private void SuccessSequence()
	{
		audioSource.Stop();
		audioSource.PlayOneShot(SuccessClip);
		isTransitioning = true;
		successParticleSystem.Play();
		movementScript.enabled = false;
		movementScript.bottleEngineParticle.Stop();
		Invoke("NextLevel", timeBetweenLevels);
	}

	private void CrashSequence()
	{
		audioSource.Stop();
		audioSource.PlayOneShot(crashClip);
		isTransitioning = true;
		crashParticleSystem.Play();
		movementScript.enabled = false;
		movementScript.bottleEngineParticle.Stop();
		Invoke("Reset", timeBetweenLevels);
	}

	private void Reset()
	{
		SceneManager.LoadScene(currentSceneIndex);
	}

	private void NextLevel()
	{
		if (currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene(0);
		}
		else
		{
			SceneManager.LoadScene(currentSceneIndex + 1);
		}
	}
}
