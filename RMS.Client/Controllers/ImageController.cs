using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using DataAccess.Concrete;

namespace RMS.Client.Controllers
{
    public class ImageController : ApiController
    {
        [HttpPost]
        public void UploadPhoto()
        {
            var httpRequest = HttpContext.Current.Request;
            var imageFile = httpRequest.Files["file0"];

            if(imageFile != null)
            {
                var userManager = new UserManager();
                var photoUrl = this.SavePhoto(imageFile);

                var user = userManager.GetAll()
                    .FirstOrDefault(u => u.Login == HttpContext.Current.User.Identity.Name);
                user.PhotoUrl = photoUrl;
                userManager.Update(user);
            }
        }
        [HttpPost]
        public void UploadRstPic(int Id)
        {
            var httpRequest = HttpContext.Current.Request;
            var imageFile = httpRequest.Files["file0"];
   
            if(imageFile != null)
            {
                var photoUrl = this.SavePhoto(imageFile);
                var rstManager = new RestaurantManager();
                var restaurant = rstManager.GetById(Id);

                restaurant.PhotoUrl = photoUrl;
                rstManager.Update(restaurant);
            }
        }

        public string SavePhoto(HttpPostedFile imageFile)
        {
            var path = @"~/Images/users_pic";
            var filename = string.Format("{0}/{1}", path, imageFile.FileName);
            imageFile.SaveAs(System.Web.Hosting.HostingEnvironment.MapPath(filename));
            return filename;
        }

    }
}
