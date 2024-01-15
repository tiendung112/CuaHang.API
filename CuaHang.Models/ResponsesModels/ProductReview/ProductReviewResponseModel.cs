using CuaHang.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Models.ResponsesModels.ProductReview
{
    public class ProductReviewResponseModel
    {
        public int Product_reviewID { get; set; }
        public int productID { get; set; }
        //public int userID { get; set; }
        public string? userName { get; set; }
        public string? content_rated { get; set; }
        public int point_evaluation { get; set; } = 0;
        public string? content_seen { get; set; }
        public int? status { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public ProductReviewResponseModel()
        {
            
        }
        public ProductReviewResponseModel(Product_review review)
        {
            productID = review.productID;
            //userID = review.userID;
            userName = review.nguoiDung.user_name;
            content_rated = review.content_rated;
            point_evaluation = review.point_evaluation;
            content_seen = review.content_seen;
            status = review.status;
            created_at = review.created_at;
            updated_at = review.updated_at;

        }
    }
}
