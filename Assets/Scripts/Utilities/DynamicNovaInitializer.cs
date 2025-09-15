using UnityEngine;
using Nova.SDK;
using Vampire;

public class DynamicNovaInitializer : MonoBehaviour
{
    void Awake()
    {
        // Only run this logic in a WebGL build
        Debug.Log("DynamicNovaInitializer Awake");
        Debug.Log("Application.absoluteURL: " + Application.absoluteURL);
#if UNITY_WEBGL && !UNITY_EDITOR
        var (orgId, appId) = GetIdsFromUrl();
        if (!string.IsNullOrEmpty(orgId) && !string.IsNullOrEmpty(appId))
        {
            var novaSettings = Resources.Load<NovaSettings>("NovaSettings");
            if (novaSettings != null)
            {
                novaSettings.OrganisationId = orgId;
                novaSettings.AppId = appId;
                Debug.Log("NovaSettings updated with dynamic IDs from URL.");
                
                // Subscribe to Nova initialization event for proper event-driven reloading
                SubscribeToNovaEvents();
            }
            else
            {
                Debug.LogError("NovaSettings asset not found in Resources folder.");
            }
        }
#endif
    }

    private (string orgId, string appId) GetIdsFromUrl()
    {
        var url = Application.absoluteURL;
        var uri = new System.Uri(url);
        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
        return (query["orgId"], query["appId"]);
    }
    
    private void SubscribeToNovaEvents()
    {
        Debug.Log("🔄 Subscribing to Nova events for configuration reload...");
        
        // If Nova is already initialized, reload immediately
        if (NovaSDK.Instance != null && NovaSDK.Instance.IsInitialized)
        {
            Debug.Log("✅ Nova SDK already initialized, reloading configuration immediately...");
            NovaManager.ReloadConfiguration();
        }
        else
        {
            // Subscribe to the Nova initialization event
            NovaManager.OnNovaInitialized += OnNovaInitialized;
        }
    }
    
    private void OnNovaInitialized()
    {
        Debug.Log("✅ Nova initialized event received, reloading configuration...");
        NovaManager.ReloadConfiguration();
        
        // Unsubscribe to avoid memory leaks
        NovaManager.OnNovaInitialized -= OnNovaInitialized;
    }
    
    private void OnDestroy()
    {
        // Clean up event subscriptions
        NovaManager.OnNovaInitialized -= OnNovaInitialized;
    }
}