using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _3Layer.DTO;
using System.Data;

namespace _3Layer.DAL
{
    class QuestionDAL
    {
        DataHelper dataHelper = new DataHelper();

        public DataTable GetTable() {
            string query = "SELECT * FROM Questions";
            DataTable dataTable = dataHelper.ExecuteQuery(query);
            return dataTable;
        }

        public bool Insert(Question question) {
            string query = string.Format("INSERT INTO Questions VALUES(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}','{5}',{6},{7})",
                question.Content, question.A, question.B, question.C, question.D, question.Correct, question.Level,question.CatagoryId);
            return dataHelper.ExecuteNonQuery(query);
        }

        public bool Delete(int id) {
            string query = string.Format("DELETE FROM Questions WHERE id={0}", id);
            return dataHelper.ExecuteNonQuery(query);
        }

        public bool Update(Question question) {
            string query = string.Format("UPDATE Questions SET content = '{0}', a='{1}', b='{2}', c='{3}', d='{4}', correct='{5}', level={6}, catagory_id={7}",
                question.Content, question.A, question.B, question.C, question.D, question.Correct, question.Level, question.CatagoryId);
            return dataHelper.ExecuteNonQuery(query);
        }

    }
}
