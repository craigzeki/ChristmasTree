using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StationTriggerAreaTest : MonoBehaviour
{
    public int id;

    private Collider tempCollider;
    [SerializeField] private DecorationType decoExpected;
    [SerializeField] private GameObject completeItem;
    [SerializeField] private GameObject interactableStationObject;
    [SerializeField] private GameObject interactableObjectLocationTag;

    private DecorationType backupDecoType;

    private bool isComplete = false;

    public bool objectOnStation = false;

    [SerializeField] private GameObject particlePrefab;
    private GameObject particles;

    private AudioSource audioSource;
    [SerializeField] private bool needsAudio = false;

    private float progress = 0f;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Decoration")
        {
            if (other.gameObject.GetComponentInChildren<iDecoration>().GetDecoration().MyDecorationType == decoExpected && !other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                objectOnStation = true;
                other.gameObject.GetComponentInChildren<iDecoration>().SetUpgrading(true);
                progress = other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeProgress();
                tempCollider = other;

                if(audioSource != null && other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeMethod() != UpgradeMethod.ButtonMash)
                {
                    audioSource.Play();
                }
                

                //Call the event from game events 
                //GameEvents.current.StationHolderTriggerEnter(id);
            }
        }
        else if(other.gameObject.tag == "MoveableObject")
        {
            if (other.gameObject.GetComponentInChildren<iDecoration>().GetDecoration().MyDecorationType == decoExpected)
            {
                
                other.gameObject.GetComponentInChildren<iDecoration>().SetUpgrading(true);

                tempCollider = other;


                if (audioSource != null && other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeMethod() != UpgradeMethod.ButtonMash)
                {
                    audioSource.Play();
                }

                //Call the event from game events 
                //GameEvents.current.StationHolderTriggerEnter(id);
            }
        }
        
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Decoration" && other.gameObject.GetComponentInChildren<iDecoration>().GetDecoration().MyDecorationType == decoExpected)
        {

            if(other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeComplete())
            {
                decoExpected = backupDecoType;
                if (other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeMethod() == UpgradeMethod.AddTogether)
                {
                    other.gameObject.GetComponentInChildren<iDecoration>().DestroyDecoration();

                    isComplete = true;
                    CompleteItem();
                    Destroy(this.gameObject.transform.parent.gameObject);
                }
                else
                {
                    other.gameObject.GetComponentInChildren<iDecoration>().DestroyDecoration();

                    isComplete = true;
                    CompleteItem();
                }
                

                
            }
            else if(!other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                
                other.gameObject.GetComponentInChildren<iDecoration>().SetUpgrading(true);
                if (audioSource != null && !audioSource.isPlaying && other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeMethod() != UpgradeMethod.ButtonMash)
                {
                    audioSource.Play();
                }
                else if(audioSource != null && !audioSource.isPlaying && (progress < other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeProgress()))
                {
                    progress = other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeProgress();
                    audioSource.Play();
                }
            }
            else if (other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {

                other.gameObject.GetComponentInChildren<iDecoration>().SetUpgrading(false);
                if (audioSource != null)
                {
                    audioSource.Stop();
                }
            }

        }
        else if (other.tag == "MoveableObject" && other.gameObject.GetComponentInChildren<iDecoration>().GetDecoration().MyDecorationType == decoExpected)
        {
            if (other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeComplete())
            {

                isComplete = true;
                CompleteMobileStation(other.gameObject);

            }
            else if(!other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                other.gameObject.GetComponentInChildren<iDecoration>().SetUpgrading(true);
                if (audioSource != null && !audioSource.isPlaying && other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeMethod() != UpgradeMethod.ButtonMash)
                {
                    audioSource.Play();
                }
                else if (audioSource != null && !audioSource.isPlaying && (progress < other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeProgress()))
                {
                    progress = other.gameObject.GetComponentInChildren<iDecoration>().GetUpgradeProgress();
                    audioSource.Play();
                }
            }
            else if (other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                other.gameObject.GetComponentInChildren<iDecoration>().SetUpgrading(false);
                if (audioSource != null)
                {
                    audioSource.Stop();
                }
            }
        }

        /*
        if (other.gameObject.GetComponent<RawBaubleHandler>() != null)
        {
            if (other.gameObject.GetComponent<RawBaubleHandler>().completed)
            {
                other.gameObject.GetComponent<RawBaubleHandler>().DestroyDecoration();

                isComplete = true;
                CompleteItem(other);
            }
        }
        */
    }

    private void CompleteMobileStation(GameObject mobileStation)
    {
        if(isComplete)
        {
            if (mobileStation.GetComponentInChildren<iDecoration>().GetUpgradeMethod() == UpgradeMethod.RemoveFrom)
            {
                if(interactableStationObject != null)
                {
                    GameObject temp = Instantiate(interactableStationObject, interactableObjectLocationTag.transform.position, interactableObjectLocationTag.transform.rotation);
                    backupDecoType = decoExpected;
                    decoExpected = temp.GetComponentInChildren<iDecoration>().GetDecoration().MyDecorationType;
                    objectOnStation = true;
                }    
            }
            mobileStation.GetComponentInChildren<iMobileStation>().UpgradeComplete();
            
            isComplete = false;
        }
    }    

    private void CompleteItem()
    {
        if (isComplete)
        {
            GameObject tempCompleteItem = (GameObject)Instantiate(completeItem, this.gameObject.transform.parent.transform.position, this.gameObject.transform.parent.transform.rotation);
            particles = (GameObject)Instantiate(particlePrefab, tempCompleteItem.transform.position, Quaternion.Euler(0, 0, 0));
            
            isComplete = false;
        }
    }

    private IEnumerator StopParticles()
    {
        yield return new WaitForSeconds(5f);
        Destroy(particles);
    }

    public void OnTriggerExit(Collider other)
    {
        
        if(other.gameObject.tag == "Decoration" || other.gameObject.tag == "MoveableObject")
        {
            if (other.gameObject.GetComponentInChildren<iDecoration>().GetDecoration().MyDecorationType == decoExpected && other.gameObject.GetComponent<ItemDecoration>().isBeingHeld)
            {
                objectOnStation = false;
                
                
                other.gameObject.GetComponentInChildren<iDecoration>().SetUpgrading(false);

                //GameEvents.current.StationHolderTriggerExit(id);
            }
            if(audioSource != null)
            {
                audioSource.Stop();
            }
            
            StartCoroutine(StopParticles());
        }
        
        

            
    }

    

    private void Start()
    {
        backupDecoType = decoExpected;
        if (needsAudio)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }
        
    }
}
