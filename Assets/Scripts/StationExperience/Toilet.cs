using UnityEngine;

public class Toilet : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted with Electrolysis Machine");
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
            "You found an electrolysis machine!",
            "System use to split into oxygen (which feeds into the cabin atmosphere) and hydrogen (which is either vented or used in the Sabatier reactor).",
            "Press SPACE to continue ..."
        };
        dialog.StartDialog();
        AddCompletedMission addCompletedMission = FindAddCompletedMissionInScene();
        if (addCompletedMission == null)
        {
            Debug.LogError("AddCompletedMission component not found in the scene.");
            return;
        }
        addCompletedMission.MarkMissionComplete("Interact with the toilet");
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