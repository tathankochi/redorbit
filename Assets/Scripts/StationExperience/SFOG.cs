using UnityEngine;

public class SFOG : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted with SFOG");
        Dialog dialog = FindDialogInScene();
        if (dialog == null)
        {
            Debug.LogError("Dialog component not found in the scene.");
            return;
        }
        if (dialog.gameObject.activeSelf) return;
        dialog.ResetDialog();
        dialog.gameObject.SetActive(true);
        dialog.dialogLines = new string[] {
            "You found a solid fuel oxygen generator",
            "This is is a chemical “oxygen candle” used as a backup oxygen source on the space habitat ",
            "A replaceable canister containing a chlorate/perchlorate-based mixture is ignited, the hot decomposition releases oxygen gas for the station atmosphere (one cartridge can supply roughly one person for ~24 hours).  ",
            "The SFOG is kept as an emergency/backup system because, while effective, its ignition/combustion process has safety risks (notably fire incidents on Mir), so its use and cartridges are carefully controlled.",
            "Press SPACE to continue ..."
        };
        dialog.StartDialog();
        AddCompletedMission addCompletedMission = FindAddCompletedMissionInScene();
        if (addCompletedMission == null)
        {
            Debug.LogError("AddCompletedMission component not found in the scene.");
            return;
        }
        addCompletedMission.MarkMissionComplete("Interact with the Solid Fuel Oxygen Generator");
        ChecklistManager checklistManager = FindChecklistManagerInScene();
        if (checklistManager == null)
        {
            Debug.LogError("ChecklistManager component not found in the scene.");
            return;
        }
        checklistManager.RefreshChecklist();
    }

    public bool IsInteractable()
    {
        return true;
    }

    void Start()
    {

    }

    void Update()
    {

    }
    private Dialog FindDialogInScene()
    {
        Dialog[] dialogs = Resources.FindObjectsOfTypeAll<Dialog>();
        foreach (Dialog dialog in dialogs)
        {
            if (dialog.gameObject.scene.IsValid()) // Ensure it's part of the active scene
            {
                return dialog;
            }
        }
        return null;
    }
    private AddCompletedMission FindAddCompletedMissionInScene()
    {
        AddCompletedMission[] managers = Resources.FindObjectsOfTypeAll<AddCompletedMission>();
        foreach (AddCompletedMission manager in managers)
        {
            if (manager.gameObject.scene.IsValid()) // Ensure it's part of the active scene
            {
                return manager;
            }
        }
        return null;
    }
    private ChecklistManager FindChecklistManagerInScene()
    {
        ChecklistManager[] managers = Resources.FindObjectsOfTypeAll<ChecklistManager>();
        foreach (ChecklistManager manager in managers)
        {
            if (manager.gameObject.scene.IsValid()) // Ensure it's part of the active scene
            {
                return manager;
            }
        }
        return null;
    }
}
