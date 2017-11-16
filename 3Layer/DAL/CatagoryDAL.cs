using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _3Layer.DTO;
using System.Data;

namespace _3Layer.DAL
{
    class CatagoryDAL
    {
        DataHelper dataHelper = new DataHelper();

        public Catagory[] GetAll() {
            string query = "SELECT * FROM Catagories";
            DataTable dataTable = dataHelper.ExecuteQuery(query);
            int length = dataTable.Rows.Count;
            Catagory[] catagories = new Catagory[length];
            for (int i = 0; i < length; i++)
            {
                Catagory catagory = RowToCatagory(dataTable.Rows[i]);
                catagories[i] = catagory;
            }
            return catagories;
        }

        public void Insert(Catagory catagory) {
        
        }

        public void Update(Catagory catagory) { 
        
        }

        private Catagory RowToCatagory(DataRow row)
        {
            Catagory catagory = new Catagory();
            catagory.Id = int.Parse(row["id"].ToString());
            catagory.name = row["name"].ToString();
            return catagory;
        }

    }
}
