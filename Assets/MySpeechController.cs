/* using UnityEngine;
using Meta.WitAi.TTS.Utilities;

public class MySpeechController : MonoBehaviour
{
    private TTSSpeaker _ttsSpeaker;
    private AudioSource _audioSource;

    void Awake()
    {
        // Get required components
        _ttsSpeaker = GetComponent<TTSSpeaker>();
        _audioSource = GetComponent<AudioSource>();
        
        // Verify components exist
        if (_ttsSpeaker == null)
        {
            Debug.LogError("TTSSpeaker component not found!");
        }
        
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource component not found!");
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        // Wait a moment before speaking to ensure everything is initialized
        Invoke("SpeakWelcome", 1.0f);
    }
    
    void SpeakWelcome()
    {
        if (_ttsSpeaker != null)
        {
            Debug.Log("Attempting to speak welcome message");
            _ttsSpeaker.Speak("Welcome to Fusion Foundry, creators! A blank canvas awaits your touch, a being born from two minds thinking as one. Before your journey begins, look around and find where two hands must press as one. Only when both are ready will the path forward reveal itself. Your creation awaits its form—will you give it life?");
        }
    }
} */

using UnityEngine;
using Meta.WitAi.TTS.Utilities;
using System.Collections;

public class SimpleSpeechController : MonoBehaviour
{
    // Reference to the TTSSpeaker component
    private TTSSpeaker _ttsSpeaker;
    
    // Welcome message split into manageable chunks
    private string[] _welcomeChunks = new string[] {
        "Welcome to Fusion Foundry, awesome creators!",
        "This is where your avatar-making adventure begins!",
        " Look around for two glowing buttons on the walls—you'll each need to press one at the same time to open the door.",
        "Teamwork makes the dream work! Ready to create something amazing together?",
        " Find those buttons and let's get this avatar party started!"
    };

    void Awake()
    {
        // Get the TTSSpeaker component
        _ttsSpeaker = GetComponent<TTSSpeaker>();
        
        if (_ttsSpeaker == null)
        {
            Debug.LogError("TTSSpeaker component not found! Please add one to this GameObject.");
        }
    }

    void Start()
    {
        // Wait a moment before speaking
        Invoke("SpeakWelcomeMessage", 2.0f);
    }
    
    public void SpeakWelcomeMessage()
    {
        if (_ttsSpeaker == null) return;
        
        // Start speaking the chunks
        StartCoroutine(SpeakChunksSequentially());
    }
    
    private IEnumerator SpeakChunksSequentially()
    {
        for (int i = 0; i < _welcomeChunks.Length; i++)
        {
            Debug.Log($"Speaking chunk {i+1}/{_welcomeChunks.Length}: {_welcomeChunks[i]}");
            
            // Speak the current chunk
            _ttsSpeaker.Speak(_welcomeChunks[i]);
            
            // Wait a moment to let the system start processing
            yield return new WaitForSeconds(0.5f);
            
            // Wait until the speaker is no longer speaking this chunk
            while (_ttsSpeaker.IsSpeaking)
            {
                yield return null;  // Wait one frame
            }
            
            // Add a small pause between chunks for more natural speech
            yield return new WaitForSeconds(0.3f);
        }
        
        Debug.Log("Finished speaking all welcome chunks");
    }
}

//         "Welcome to Fusion Foundry, creators!",
//        "A blank canvas awaits your touch, a being born from two minds thinking as one.",
//        "Before your journey begins, look around and find where two hands must press as one.",
//        "Only when both are ready will the path forward reveal itself.",
//       "Your creation awaits its form—will you give it life?"



