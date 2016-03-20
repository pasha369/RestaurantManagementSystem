using System.Collections.Generic;
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
