using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviour
{
    // This is a singleton class
    public static ConnectionManager Instance { get; private set; }

    public List<Connector> connectors = new List<Connector>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene load event
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the scene load event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshConnectors(); // Refresh connectors when a new scene is loaded
    }

    private void RefreshConnectors()
    {
        connectors.Clear(); // Clear the list to remove invalid references
        GameObject[] connectorObjects = GameObject.FindGameObjectsWithTag("Connector");
        foreach (GameObject obj in connectorObjects)
        {
            Connector connector = obj.GetComponent<Connector>();
            if (connector != null)
            {
                connectors.Add(connector);
            }
        }
        Debug.Log("Connectors refreshed. Count: " + connectors.Count);
    }

    void Start()
    {
        RefreshConnectors(); // Ensure connectors are initialized at the start
    }

    void Update()
    {
        // Optional: Remove this block if RefreshConnectors is sufficient
        if (connectors.Count == 0)
        {
            RefreshConnectors();
        }
    }

    public Connector IsColliding(Connector thisConnector)
    {
        foreach (Connector connector in connectors)
        {
            if (connector == null || connector == thisConnector) continue; // Skip null or self
            float distance = Vector3.Distance(thisConnector.transform.position, connector.transform.position);
            if (distance < 0.5f)
            {
                // Debug.Log("1Connector position: " + connector.transform.position);
                // Debug.Log("Collision detected between connectors");
                // Get the module of connector
                Module connectorModule = connector.GetComponentInParent<Module>();
                if (connectorModule != null)
                {
                    // Debug.Log("Connector module found: " + connectorModule.name);
                }
                else
                {
                    Debug.LogError("Connector module not found");
                }
                if (connectorModule != null && connectorModule.isKinematic)
                {
                    return connector;
                }
                return null;
            }
        }
        return null;
    }

    public void MakeConnectorTransparent(Connector connector)
    {
        Renderer renderer = connector.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color color = renderer.material.color;
            color.a = 0.0f; // Set alpha to 50% transparency
            renderer.material.color = color;
        }
    }

    public void MakeAllConnectorTransparent()
    {
        foreach (Connector connector in connectors)
        {
            MakeConnectorTransparent(connector);
        }
    }
}