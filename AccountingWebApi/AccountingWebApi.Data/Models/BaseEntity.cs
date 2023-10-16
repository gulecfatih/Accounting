using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingWebApi.Data.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public void SetCreate()
        {
            this.CreateDate = DateTime.Now;
        }
        public void SetUpdate(BaseEntity original)
        {
            this.UpdateDate = DateTime.Now;
            this.Id = original.Id;
        }
    }

}
