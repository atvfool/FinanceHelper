using FinanceHelper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceHelper.Controllers
{
    [Authorize]
    public class AuthorizedController : Controller
    {
        
    }
}
