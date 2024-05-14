using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using TMPro;


public class MutationMenu : MonoBehaviour
{
    public GameObject mutationMenuObject;
    public GameObject player;
    public GameObject mutationPrefab;
    public Transform mutationList;
    public int mutationsPerMenu;
    public List<Mutation> mutationsInMenu;
    public List<Mutation> AllMutations; //these lists were public for debugging only but for some reason this script breaks without it
    public List<Mutation> PlayerMutations, AIMutations;
    public List<Mutation> PlayerMutationPool, AIMutationPool;
    public List<GameObject> DisplayedMutationOptions;
    public TextMeshProUGUI mutationNameTMPro, mutationDescriptionTMPro;

    void Start()
    {
        AllMutations = GetComponents<Mutation>().ToList(); 
        PlayerMutationPool.AddRange(AllMutations);
        PlayerMutations = player.GetComponents<Mutation>().ToList();
        foreach(Mutation playerMutation in PlayerMutations){
            PlayerMutationPool.RemoveAll(Mutation => Mutation.Name == playerMutation.Name);
        }
        OpenMutationMenu();
    }
    public void OpenMutationMenu()
    {
        mutationMenuObject.SetActive(true);
        int optionsToDisplay = Mathf.Min(PlayerMutationPool.Count, mutationsPerMenu);
        mutationsInMenu.Clear();
        for (int i = 0; i < optionsToDisplay; i++)
        {
            Mutation currentMutation = PlayerMutationPool[Random.Range(0, PlayerMutationPool.Count)];
            mutationsInMenu.Add(currentMutation);
            PlayerMutationPool.Remove(currentMutation);  

            GameObject newMutationOption = Instantiate(mutationPrefab, mutationList);
            DisplayedMutationOptions.Add(newMutationOption);
            
            EventTrigger newTrigger = newMutationOption.GetComponent<EventTrigger>();
            newTrigger.triggers.Clear();

            //PointerEnter listener
            EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            pointerEnterEntry.callback.AddListener((data) => { OnPointerEnterDelegate((PointerEventData)data, currentMutation.Name, currentMutation.Description); });
            newTrigger.triggers.Add(pointerEnterEntry);

            //PointerClick listener
            EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry();
            pointerClickEntry.eventID = EventTriggerType.PointerClick;
            pointerClickEntry.callback.AddListener((data) => { OnPointerClickDelegate((PointerEventData)data, currentMutation); });
            newTrigger.triggers.Add(pointerClickEntry);
        }
    }

    public void OnPointerEnterDelegate(PointerEventData data, string mutationName, string mutationDesc) //Gets called
    {
        print(mutationName);
        mutationNameTMPro.text = mutationName;
        mutationDescriptionTMPro.text = mutationDesc;
        Debug.Log("OnPointerEnterDelegate called.");
    }
    public void OnPointerClickDelegate(PointerEventData data, Mutation selectedMutation) //Doesnt get called
    {
        Debug.Log("OnPointerClickDelegate called.");
        Mutation newMutation = player.AddComponent<Mutation>(selectedMutation, this);
        newMutation.enabled = true;
        CloseMutationMenu();
    }


    public void CloseMutationMenu()
    {
        foreach(Mutation menuMutation in mutationsInMenu)
        {
            PlayerMutationPool.Add(menuMutation);
        }
        foreach(GameObject mutationOption in DisplayedMutationOptions) {
            Destroy(mutationOption);
        }
        DisplayedMutationOptions.Clear();
        mutationsInMenu.Clear();
        mutationMenuObject.SetActive(false);
    }

    public Mutation RandomMutation(List<Mutation> myCurrentMutations)
    {
        AIMutationPool = CalculateMutationPool(myCurrentMutations);
        return AIMutationPool[Random.Range(0,AIMutationPool.Count())];
    }

    public List<Mutation> CalculateMutationPool(List<Mutation> myCurrentMutations)
    {
        List<Mutation> poolCalculation = AllMutations;
        foreach(Mutation AIMutation in myCurrentMutations){
            poolCalculation.RemoveAll(Mutation => Mutation.Name == AIMutation.Name);
        }
        return poolCalculation;
    }
}
