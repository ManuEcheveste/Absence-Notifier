using System;
using static GlobalVariables.Variables;

public static class DailyCheck
{
    public static void Start()
    {
        LoadSettings();
        VerifyLastLogin();
        if(daysSinceLastLogin == reminderDay)
        {
            LoginEmail();
            EmailHandler.SendReminder();
        }

        if(daysSinceLastLogin == AbsenceDay)
        {
            LoginEmail();
            LoadMessages();
            EmailHandler.SendAbsenceNotification();
            EmailHandler.SendSpecialAbsenceNotification();
        }
    }

}
