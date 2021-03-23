﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SqlKata.Execution;
using TutorMeFMI.Data;
using TutorMeFMI.Models;

namespace TutorMeFMI.Controllers
{
    public class RequestsController : Controller
    {
        /**
         * GET method that returns all the existing requests posted by an user
         * param @userId = int value representing the id of the user to retrieve the requests for
         * Returns a list of type Request containing the retrieved requests
         */        
        public IEnumerable<Request> GetUserRequests(int userId)
        {
            using var database = new Database().GetQueryFactory();
            var requests = database.Query("request").Where("user", "=", userId).Get<Request>();
            return requests;
        }
    }
}