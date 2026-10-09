
using System.Web.Http;

namespace WebForm.Controllers
{
    public class StudentsController : ApiController
    {
        // Any authenticated user with a valid JWT can access this.
        [HttpGet]
        [Route("api/students")]
        [Authorize]
        public IHttpActionResult GetStudents()
        {
            return Ok(new
            {
                Message = "JWT authentication successful!"
            });
        }

        // Only users whose token has the Admin role can access this.
        [HttpGet]
        [Route("api/students/admin-test")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AdminTest()
        {
            return Ok(new
            {
                Message = "Admin authorization successful!"
            });
        }
    }
}
//using System.Security.Claims;
//using System.Web.Http;

//namespace WebForm.Controllers
//{
//    [RoutePrefix("api/students")]
//    public class StudentsController : ApiController
//    {
//        // Any authenticated user with a valid JWT can access this.
//        [HttpGet]
//        [Route("profile")]
//        [Authorize]
//        public IHttpActionResult GetProfile()
//        {
//            var identity = User.Identity as ClaimsIdentity;

//            return Ok(new
//            {
//                Message = "Valid JWT accepted!",
//                Username = identity?.FindFirst("name")?.Value,
//                Role = identity?.FindFirst(ClaimTypes.Role)?.Value
//            });
//        }

//        // Only an authenticated Admin can access this.
//        [HttpGet]
//        [Route("admin-test")]
//        [Authorize(Roles = "Admin")]
//        public IHttpActionResult AdminTest()
//        {
//            return Ok(new
//            {
//                Message = "Admin authorization successful!"
//            });
//        }
//    }
//}


////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Web.Http;
////using System.Security.Claims;

////namespace WebForm.Controllers
////{
////    public class StudentsController : ApiController
////    {
////        [HttpGet]
////        [Route("api/students")]
////        [Authorize(Roles ="Admin")]
////        public IHttpActionResult GetStudents()
////        {
////            return Ok(
////                new
////                {
////                    Message = "You successfully accessed Students api"
////                }
////                );
////        }
////    }
////}