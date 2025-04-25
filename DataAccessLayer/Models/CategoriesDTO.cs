using Microsoft.Data.SqlClient.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class CategoryDTO
    {
        
        public CategoryDTO(int ID, string CategoryName)
        {
            this.ID = ID;
            this.Name = CategoryName;
        }
        public int ID { get; set; }
        public string Name { get; set; }

    }
    
    public class CreateCategoryDTO
    {
        [Length(4,50)]
        public string Name { get; set; }

    }
}
