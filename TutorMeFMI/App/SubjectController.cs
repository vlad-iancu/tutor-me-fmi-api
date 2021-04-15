using Microsoft.AspNetCore.Mvc;
using SqlKata.Execution;
using TutorMeFMI.Data;

namespace TutorMeFMI.App
{
    public class SubjectController : Controller
    {
        public IActionResult List()
        {
            using var database = new Database().GetQueryFactory();
            var subjects = database.Query("subject").Get();
            return Json(new {subjects});
        }
    }
}