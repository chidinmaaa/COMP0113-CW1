using UnityEngine;
using Meta.WitAi.TTS.Utilities;
using System.Collections;

public class SimpleSpeechController : MonoBehaviour
{
    private TTSSpeaker _ttsSpeaker;
    
    // Messages split into manageable chunks
    private string[] _welcomeChunks = new string[]{
        "Welcome to Fusion Foundry, awesome creators!",
        "This is where your avatar-making adventure begins!",
        "Look around for two glowing buttons on the walls—you'll each need to press one at the same time to open the door.",
        "Teamwork makes the dream work! Ready to create something amazing together?",
        "Find those buttons and let's get this avatar party started!"
    };

    private string[] _beltAndBodyLabChunks = new string[]{
        "See that cool conveyor belt?",
        "It's carrying your avatar-in-progress through each creative zone!",
        "You made it to the second room!",
        "Welcome to the Body Lab!",
        "See those cool sliders on the wall?",
        "Each of you can grab one to adjust your avatar's height, width, and depth.",
        "One of you might want to make your avatar super tall, while the other prefers something different—talk it out and find the perfect balance!",
        "When you're both happy with the shape, hit those magic buttons together to send your creation to the next exciting station!"
    };
    private string[] _styleStationChunks = new string[]{
        "Woohoo, you've reached the Style Station!",
        "Check out those colorful texture spheres floating around—they're how you'll give your avatar some pizzazz!",
        "But here's the fun part: one of you needs to raise the platform while the other throws the spheres at your avatar.",
        "Timing is everything!",
        "Communicate when to raise and when to throw for the perfect hit.",
        "Once your avatar is looking fabulous with its new style, both hit those buttons to continue your creative journey!"
    };
    private string[] _accessoryStudioChunks = new string[]{
        "Accessories time!",
        "This room is packed with fun add-ons for your avatar—hats, hair, bags, you name it!",
        "Take turns picking out accessories and placing them on your creation.",
        "Some might be in hard-to-reach spots, so you'll need to help each other out.",
        "Can you grab that crown while I hold this platform?",
        "That's the spirit!",
        "Once your avatar is decked out in all its glory, find those twin buttons and press them together to move on!"
    };
    private string[] _finalRoomChunks = new string[]{
        "Final room—decision time!",
        "Your amazing avatar is almost ready for action!",
        "Take a good look at your collaborative masterpiece and decide if you're both happy with it.",
        "Each of you gets to vote using the special voting station.",
        "If you both give it a thumbs up, you gets to jump in and become the avatar!",
        "Your teamwork has created something spectacular!"
    };


    void Awake(){
        _ttsSpeaker = GetComponentInChildren<TTSSpeaker>(); // Get the TTSSpeaker component
        
        if (_ttsSpeaker == null)
        {
            Debug.LogError("TTSSpeaker component not found! Please add one to this GameObject.");
        }
    }

    void Start()
    {}
    
    public void Speak(string room, float pause){
        if (_ttsSpeaker == null) return;

        switch(room)
        {
            case "entrance":
                StartCoroutine(SpeakChunksSequentially(_welcomeChunks, 10.0f));
                break;
            case "belt":
                StartCoroutine(SpeakChunksSequentially(_beltAndBodyLabChunks, pause));
                break;
            case "style_station":
                StartCoroutine(SpeakChunksSequentially(_styleStationChunks, pause));
                break;
            case "accessory_studio":
                StartCoroutine(SpeakChunksSequentially(_accessoryStudioChunks, pause));
                break;
            case "final_room":
                StartCoroutine(SpeakChunksSequentially(_finalRoomChunks, pause));
                break;
        }
    }
    
    private IEnumerator SpeakChunksSequentially(string[] chunks, float pause){
        yield return new WaitForSeconds(pause);

        for (int i = 0; i < chunks.Length; i++) {
            //Debug.Log($"Speaking chunk {i+1}/{chunks.Length}: {chunks[i]}");
            
            // Speak the current chunk
            _ttsSpeaker.SpeakQueued(chunks[i]);
            
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
    }
}
