using UnityEngine;
using UnityEngine.SceneManagement;

public class collision : MonoBehaviour
{
    [SerializeField]float delay = 1.5f;
    [SerializeField] AudioClip expolsionsound;
    [SerializeField] AudioClip landedsound;

    [SerializeField] ParticleSystem expolsionparticles;
    [SerializeField] ParticleSystem landedparticles;

    AudioSource audiosource;

    bool istransitioning= false;
    bool collisiondisable = false;
    void Start()
    {
         audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        processcheat();
    }

    void processcheat()
    {
        if(Input.GetKey(KeyCode.L))
        {
            loadnextlevel();
        }
        else if(Input.GetKey(KeyCode.C))
        {
            collisiondisable = !collisiondisable;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(istransitioning || collisiondisable){ return;}

        switch(other.gameObject.tag)
        {
            case "friendly":
                break;
            case "Finish":
                landedsequence();
                break; 
            default:
                startcrashedsequence();
                break;  

        }
    }
    void landedsequence()
    {   
        istransitioning = true;
        audiosource.Stop();
        audiosource.PlayOneShot(landedsound);
        landedparticles.Play();
     
        GetComponent<movement>().enabled = false;
        Invoke("loadnextlevel",delay);
    }
    void startcrashedsequence()
    {
        istransitioning = true;
        audiosource.Stop();
        audiosource.PlayOneShot(expolsionsound);   
        expolsionparticles.Play();
        
        GetComponent<movement>().enabled = false;
        Invoke("reloadlevel",delay);
    }
    void reloadlevel()
    {
        int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneindex);
    }
    void loadnextlevel()
    {
        int currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        int nextlevelindex = currentsceneindex + 1;
        if (nextlevelindex == SceneManager.sceneCountInBuildSettings)
        {
            nextlevelindex = 0;
        }
        SceneManager.LoadScene(nextlevelindex);
    }
}
