using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MutationMenu : MonoBehaviour
{
    public GameObject mutationMenuObject;
    public GameObject player;
    public GameObject mutationPrefab;
    public Transform mutationList;
    public int mutationsInMenu;
    public Mutation[] AllMutations;
    public List<Mutation> PlayerMutations;
    public List<Mutation> MutationPool;
    void Start()
    {
        MutationPool.AddRange(AllMutations);
        PlayerMutations = player.GetComponents<Mutation>().ToList();    
        foreach(Mutation mutation in PlayerMutations)
        {
            MutationPool.Remove(mutation);
        }
    }
    public void OpenMutationMenu()
    {
        mutationMenuObject.SetActive(true);
        for (int i = 0; i < Mathf.Min(MutationPool.Count, mutationsInMenu); i++)
        {
            GameObject newMutation = Instantiate(mutationPrefab, mutationList);
        }
    }

    public void CloseMutationMenu()
    {
        mutationMenuObject.SetActive(false);
    }
}
