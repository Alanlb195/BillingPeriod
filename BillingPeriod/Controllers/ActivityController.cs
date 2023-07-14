using AutoMapper;
using BillingPeriod.Models;
using BillingPeriod.Services.Activities;
using Microsoft.AspNetCore.Mvc;
using static BillingPeriod.Models.Enum;

namespace BillingPeriod.Controllers
{
    public class ActivityController : BaseController
    {

        private readonly IActivityService _activityService;
        private readonly IMapper _mapper;

        public ActivityController(IActivityService iActivityService, IMapper mapper)
        {
            _activityService = iActivityService;
            _mapper = mapper;
        }

        // GET: Activity
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: Activity/List
        [HttpGet]
        public IActionResult List(DateTime? fecha)
        {
            List<Activity> activities;

            if (fecha.HasValue)
            {
                activities = _activityService.GetAllActivitiesByDate(fecha.Value).Result;
            }
            else
            {
                activities = _activityService.GetAllActivities().Result;
            }
            return View(activities);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityViewModel request)
        {
            if (ModelState.IsValid)
            {
                var response = _mapper.Map<Activity>(request);

                bool isActivityCreated = await _activityService.AddActivity(response);

                if (isActivityCreated == true)
                {
                    return RedirectToAction("Index", "Activity");
                }
                else
                {
                    Message("Hay un registro que coincide en tiempo", NotificationType.error);
                    return View(request);
                }
            }
            return View(request);
        }




    }
}
