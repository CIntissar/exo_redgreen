using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
class SphereData
{
    public bool isGreen;
    public vector x;
    public vector y;
    public vector z; // split la position vector 3 en 3 points
}



[RequireComponent(typeof(NavMeshAgent))]
public class SphereAI : MonoBehaviour
{
    public List<Transform> targets = new List<Transform>();
    private int index = -1;

    private GameObject sphereToken;
    private NavMeshAgent agent;
    private string dataPath;

    public Material greenMaterial;
    public Material redMaterial;

    public bool isGreen = false;

    void Start()
    {
        dataPath = Application.persistentDataPath + "/sphere.dat"; 

        if(File.Exists(dataPath))
        {
            Load();
        }
        else
        {
            isGreen = true;
        }

        agent = GetComponent<NavMeshAgent>();
        NextDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) 
        {
            NextDestination();

        } 
    }

    public void NextDestination() 
    {
        Save();
        int oldIndex = index;
        while(oldIndex == index) 
        {
            index = UnityEngine.Random.Range(0, targets.Count);
        }
        agent.SetDestination(targets[index].position);


        if(isGreen)
        {
            GetComponent<Renderer>().material = redMaterial;

        }
        else
        {
            GetComponent<Renderer>().material = greenMaterial;

        }

        isGreen = !isGreen;
    }

    public void Save()
    {
        FileStream file = File.Create(dataPath);

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            SphereData sd = new SphereData();
            sd.isGreen = isGreen;
            sd.position = transform.position;

            bf.Serialize(file, sd);
        }

        finally
        {	
            file.Close(); // quoiqu'il arrive -> fermer le flux!
        }
    }

    public void Load()
    {
        FileStream file = File.Open(dataPath,FileMode.Open);

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            SphereData sd = bf.Deserialize(file) as SphereData;
            isGreen = sd.isGreen;
            Vector3 position = new Vector(sd.x,sd.y,sd.z);
            transform.position = position;

        }

        finally
        {	
            file.Close(); // quoiqu'il arrive -> fermer le flux!
        }
    }
    
}
