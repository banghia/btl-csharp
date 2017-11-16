using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _3Layer.DAL;
using _3Layer.DTO;
using System.Data;

namespace _3Layer.BLL
{
    class QuestionBLL
    {
        QuestionDAL questionDAL = new QuestionDAL();

        public List<Question> getAll() {
            List<Question> list = new List<Question>();
            DataTable questionTable = questionDAL.GetTable();
            foreach(DataRow row in questionTable.Rows){
                list.Add(RowToQuestion(row));
            }
            return list;
        }

        public bool Insert(Question question) {
            return questionDAL.Insert(question);
        }

        public bool Delete(int id) {
            return questionDAL.Delete(id);
        }

        private Question RowToQuestion(DataRow row)
        {
            Question question = new Question();
            question.Id = int.Parse(row["id"].ToString());
            question.Content = row["content"].ToString().Trim();
            question.A = row["a"].ToString().Trim();
            question.B = row["b"].ToString().Trim();
            question.C = row["c"].ToString().Trim();
            question.D = row["d"].ToString().Trim();
            question.Correct = Char.Parse(row["correct"].ToString());
            question.Level = int.Parse(row["level"].ToString());
            question.CatagoryId = int.Parse(row["catagory_id"].ToString());
            return question;
        }

    }
}
