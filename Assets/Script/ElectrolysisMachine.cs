using UnityEngine;

/// <summary>
/// Bàn làm việc - object có thể tương tác
/// Hiển thị dialog về bàn làm việc
/// Đánh dấu nhiệm vụ "Interact with the ElectrolysisMachine" hoàn thành  
/// Không chuyển scene (khác với Computer và Arcade)
/// </summary>
public class ElectrolysisMachine : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Thực hiện tương tác với bàn làm việc
    /// 1. Tìm Dialog component và hiển thị dialog
    /// 2. Đánh dấu nhiệm vụ hoàn thành
    /// 3. Refresh checklist UI
    /// Không chuyển scene
    /// </summary>
    public void Interact()
    {
        Debug.Log("Interacted with Electrolysis Machine");
        Dialog dialog = FindDialogInScene();
        if (dialog == null)
        {
            Debug.LogError("Dialog component not found in the scene.");
            return;
        }
        if (dialog.gameObject.activeSelf) return; // Tránh hiển thị dialog khi đang active
        
        // Hiển thị dialog về bàn làm việc
        dialog.ResetDialog();
        dialog.gameObject.SetActive(true);
        dialog.dialogLines = new string[] {
            "You found a electrolysis machine!",
            "You can use this to do experiments.",
            "Press SPACE to continue ..."
        };
        dialog.StartDialog();
        
        // Đánh dấu nhiệm vụ hoàn thành
        AddCompletedMission addCompletedMission = FindAddCompletedMissionInScene();
        if (addCompletedMission == null)
        {
            Debug.LogError("AddCompletedMission component not found in the scene.");
            return;
        }
        addCompletedMission.MarkMissionComplete("Interact with the electrolysis machine");
        
        // Refresh checklist UI
        ChecklistManager checklistManager = FindChecklistManagerInScene();
        if (checklistManager == null)
        {
            Debug.LogError("ChecklistManager component not found in the scene.");
            return;
        }
        checklistManager.RefreshChecklist();
    }

    /// <summary>
    /// Kiểm tra có thể tương tác không
    /// </summary>
    /// <returns>true - luôn có thể tương tác</returns>
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
    
    /// <summary>
    /// Tìm Dialog component trong scene
    /// </summary>
    /// <returns>Dialog component hoặc null</returns>
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
    
    /// <summary>
    /// Tìm AddCompletedMission component trong scene
    /// </summary>
    /// <returns>AddCompletedMission component hoặc null</returns>
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
    
    /// <summary>
    /// Tìm ChecklistManager component trong scene
    /// </summary>
    /// <returns>ChecklistManager component hoặc null</returns>
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
