﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SandboxCore.Models;

namespace SandboxCore.Controllers
{
    public class BaseController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        protected void AddMessageFields(ResultMessage m1, ResultMessage m2)
        {
            m1.IsError = m2.IsError;
            m1.ShowMessage = m2.ShowMessage;
            m1.Message = m2.Message;            
        }
    }
}
