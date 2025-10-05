using UnityEngine;

public class Locker : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted with Locker");
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
            "You found a locker!",
            "lockers are standardized storage compartments used to house scientific experiments, equipment, and supplies.",
            "Press SPACE to continue ..."
        };
        dialog.StartDialog();
        AddCompletedMission addCompletedMission = FindAddCompletedMissionInScene();
        if (addCompletedMission == null)
        {
            Debug.LogError("AddCompletedMission component not found in the scene.");
            return;
        }
        addCompletedMission.MarkMissionComplete("Interact with the Locker");
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
