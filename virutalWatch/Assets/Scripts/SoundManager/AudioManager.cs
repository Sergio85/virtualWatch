using UnityEngine;
using System.Collections;
 using System.Collections.Generic;

public enum Soundname {
		
		tictac,
		gong,
		forward,
		wipein,
		wipeout,
	    OnClick,
	    TwoClick,
	    SecondClick,
	    ThreeClick,
	    FullClick,
		xray
		
	}

public class AudioManager : MonoBehaviour
{
	
	
	private List<AudioSource> mySourceAudio;
	
	private List<AudioClip> myAudioClip;
	
	public static AudioManager instance;
	
	
	public static AudioManager Instance
	{
		
		
		get
			
		{
			if (instance == null)
			{
				instance = new GameObject ("AudioManager").AddComponent<AudioManager>();
				instance.initialize();
			}
			
			return instance;
		}
	}
	
	public void OnApplicationQuit() 
	{ destroyInstance(); }
	
	
	public void destroyInstance() {
		
		Debug.Log("Destroy Instance");
		instance = null; }
	
	
	
	public void initialize() 
	{ 
		
		
		mySourceAudio = new List<AudioSource>();
		myAudioClip = new List<AudioClip>();
		
		//Sounds Init
		
		AudioSource newAudio,newAudio2,newAudio3,newAudio4,newAudio5,newAudio6,newAudio7,newAudio8,newAudio9,newAudio10;
		
		
		newAudio = (AudioSource)gameObject.AddComponent("AudioSource");
		
		mySourceAudio.Add(newAudio); 
		
		myAudioClip.Add(((AudioClip)Resources.Load("UhrwerkFilterL")));
		mySourceAudio[0].clip = myAudioClip[0];
		
		newAudio2 = (AudioSource)gameObject.AddComponent("AudioSource");
		
		mySourceAudio.Add(newAudio2); 
		
		myAudioClip.Add(((AudioClip)Resources.Load("Gong1Event")));
		mySourceAudio[1].clip = myAudioClip[1];
		
		newAudio3 = (AudioSource)gameObject.AddComponent("AudioSource");
		
		mySourceAudio.Add(newAudio3); 
		
		myAudioClip.Add(((AudioClip)Resources.Load("delay")));
		mySourceAudio[2].clip = myAudioClip[2];
		
		//---------------------------------------------------------------------
		newAudio4 = (AudioSource)gameObject.AddComponent("AudioSource");
		mySourceAudio.Add(newAudio4); 
		
		myAudioClip.Add(((AudioClip)Resources.Load("WipeIn")));
		mySourceAudio[3].clip = myAudioClip[3];
		
		//---------------------------------------------------------------------
		newAudio5 = (AudioSource)gameObject.AddComponent("AudioSource");
		mySourceAudio.Add(newAudio5); 
		
		myAudioClip.Add(((AudioClip)Resources.Load("WipeOut")));
		mySourceAudio[4].clip = myAudioClip[4];
		
		//---------------------------------------------------------------------
		//---------------------------------------------------------------------
		newAudio6 = (AudioSource)gameObject.AddComponent("AudioSource");
		mySourceAudio.Add(newAudio6); 
		
		myAudioClip.Add(((AudioClip)Resources.Load("OnClick")));
		mySourceAudio[5].clip = myAudioClip[5];
		
		//---------------------------------------------------------------------
		newAudio7 = (AudioSource)gameObject.AddComponent("AudioSource");
		mySourceAudio.Add(newAudio7); 
		
		myAudioClip.Add(((AudioClip)Resources.Load("SecondClick")));
		mySourceAudio[6].clip = myAudioClip[6];
		//--------------------------------------------------------------------
		//---------------------------------------------------------------------
		newAudio8 = (AudioSource)gameObject.AddComponent("AudioSource");
		mySourceAudio.Add(newAudio8); 
		
		myAudioClip.Add(((AudioClip)Resources.Load("TriClick")));
		mySourceAudio[7].clip = myAudioClip[7];
		//-------------------------------------------------------------------
		//---------------------------------------------------------------------
		newAudio9 = (AudioSource)gameObject.AddComponent("AudioSource");
		mySourceAudio.Add(newAudio9); 
		
		myAudioClip.Add(((AudioClip)Resources.Load("QuadClick")));
		mySourceAudio[8].clip = myAudioClip[8];
		//----------------------------------------------------------------------
		//---------------------------------------------------------------------
		newAudio10 = (AudioSource)gameObject.AddComponent("AudioSource");
		mySourceAudio.Add(newAudio10); 
		
		myAudioClip.Add(((AudioClip)Resources.Load("FullClick")));
		mySourceAudio[9].clip = myAudioClip[9];
		
		 }
	
	public float GetLenghAudioClip(Soundname soun)
	{
		return mySourceAudio[(byte)soun].clip.length;
	}
	public void Play(Soundname sound,bool looping)
	{
		
		
		mySourceAudio[(byte)sound].loop = looping;
		mySourceAudio[(byte)sound].Play();
		
	}
	//SoundPause
	public void PauseSound(Soundname so)
	{
		mySourceAudio[(byte)so].Pause();
	}
	public void PlayDelay(Soundname s,float second)
	{
		mySourceAudio[(byte)s].PlayDelayed(second);
	}
	//Sound pitchen
	public void Pitch(Soundname sound,float pitch)
	{
		mySourceAudio[(byte)sound].pitch = pitch;
	}
	//Sound Lautstaerke aendern
	public void Volume(Soundname sound,float volume)
	{
		mySourceAudio[(byte)sound].volume = volume;
		
	}
	
	public void StoppSound(Soundname n)
	{
		mySourceAudio[(byte)n].Stop();
	}
	//******    3D Sound --------------------------------------------------------------------------------------------
	
    public AudioSource Play(AudioClip clip, Transform emitter)
    {
        return Play(clip, emitter, 1f, 1f);
    }
 
    public AudioSource Play(AudioClip clip, Transform emitter, float volume)
    {
        return Play(clip, emitter, volume, 1f);
    }
 
    
    public AudioSource Play(AudioClip clip, Transform emitter, float volume, float pitch)
    {
        
        GameObject go = new GameObject ("Audio: " +  clip.name);
        go.transform.position = emitter.position;
        go.transform.parent = emitter;
 
        
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play ();
        Destroy (go, clip.length);
        return source;
    }
 
    public AudioSource Play(AudioClip clip, Vector3 point)
    {
        return Play(clip, point, 1f, 1f);
    }
 
    public AudioSource Play(AudioClip clip, Vector3 point, float volume)
    {
        return Play(clip, point, volume, 1f);
    }
 
    
    public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch)
    {
        
        GameObject go = new GameObject("Audio: " + clip.name);
        go.transform.position = point;
 
        
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
        Destroy(go, clip.length);
        return source;
        
    }
   //+++++++++++++++----------------------------------------------------------3D Sound
	
}