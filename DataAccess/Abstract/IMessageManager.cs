using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Model;

namespace DataAccess.Abstract
{
    public interface IMessageManager
    {
        void ApplyReview(Review review);
        void CheckSpam(Review review);
        List<Review> GetAllReview();
    }
}
