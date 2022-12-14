using AspNetMvcClass.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcClass.Controllers;

public class CdTimerController : Controller
{
    // Get the current countdown time
    public ActionResult GetCurrentTime()
    {
        // Get the current time and the start and end times for the countdown
        var currentTime = DateTime.Now;
        var startTime = new DateTime(2022, 12, 13, 14, 0, 0); // 14:00:00 on December 13, 2022
        var endTime = new DateTime(2022, 12, 13, 16, 0, 0); // 16:00:00 on December 13, 2022

        // Calculate the remaining time and duration
        var remainingTime = endTime - currentTime;
        var totalDuration = (endTime - startTime).TotalSeconds;
        var remainingDuration = remainingTime.TotalSeconds;

        // Return the current countdown time as JSON
        return Json(new { remainingTime, totalDuration, remainingDuration });
    }
}
