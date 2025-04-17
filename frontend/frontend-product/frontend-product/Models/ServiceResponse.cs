using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace frontend_product.Models
{
    public class ServiceResponse<T>
    {
        public int CODE { get; set; }
        public string STATUS { get; set; }
        public string MESSAGE { get; set; }
        public IEnumerable<T> RESULT { get; set; }
    }

    public class ServiceResponseSingle<T>
    {
        public int CODE { get; set; }
        public string STATUS { get; set; }
        public string MESSAGE { get; set; }
        public string jwt_token { get; set; }
        public T RESULT { get; set; }
    }

    public class ResLogin
    {
        public string TOKEN { get; set; }
    }

    public class ResDropdown
    {
        public string ID { get; set; }
        public string DESCRIPTION { get; set; }
    }

    public class ResDataProduct
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Decimal? Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        
    }




}
