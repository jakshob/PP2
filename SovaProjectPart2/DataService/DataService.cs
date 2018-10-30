using System;
using System.Collections.Generic;
using System.Linq;


namespace DomainModel
{
    public class DataService : IDataService
    {


        public List<Answer> GetAnswers()
        {
            using (var db = new SovaContext())
            {
                /* var postList = new List<Post>();
                 foreach (Post p in db.Posts) {
                     if (string.IsNullOrEmpty(p.Name)){
                         p.Name = " empty ";
                     }
                     postList.Add(p);
                 }
                 */
                return db.Answers.ToList();
            }
        }

        public List<Question> GetQuestions(int page, int pageSize) {
            using (var db = new SovaContext()) {

                var questionList = new List<Question>();
                foreach (Question p in db.Questions) {
					questionList.Add(p);
                }
                return questionList;

            }
        }

        public List<Answer> GetAnswersToQuestion(int inputId, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {

                var answerList = new List<Answer>();
                foreach (Answer p in db.Answers)
                {
                    if (p.ParentId == inputId)
                    {
                        answerList.Add(p);
                    }
                }
                return answerList;

            }
        }


        public Answer GetAnswer(int inputCatId)
        {

            using (var db = new SovaContext())
            {
                return db.Answers.Find(inputCatId);
            }
		}
		public Question GetQuestion(int inputCatId) {

			using (var db = new SovaContext()) {
				return db.Questions.Find(inputCatId);
			}
		}
		public List<Comment> GetCommentsByPostId(int inputId, int page, int pagesize)
        {
            using (var db = new SovaContext())
            {
                var commentList = new List<Comment>();
                foreach (Comment c in db.Comments)
                {
                    if (c.PostId == inputId)
                    {
                        commentList.Add(c);
                    }
                }
                return commentList;
            }
        }
        public Comment GetComment(int id)
        {
            using (var db = new SovaContext())
            {
                return db.Comments.Find(id);
            }
        }
        public List<Question> GetSearchQuestionsSortedByScore(string searchText, int page, int pageSize) {

            using (var db = new SovaContext())
            {
            
                //Starter med søgning i navn, senere kan tilføjes body!

                //Liste kun med questions
                var onlyQuestions = db.Questions;
                //Liste kun hvor indeholder "searchText" i navnet + gør mindre til comparison
                var questionsFromSearch = onlyQuestions.Where(p => p.Name.ToLower().Contains(searchText.ToLower()));
                //output to List<Post>
                var queSortByScore = questionsFromSearch.OrderByDescending(x => x.Score).ToList();

                return queSortByScore;
            }
            
        }

        public List<string> GetHistory(string username, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<string> GetMostUsedSearchTexts(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<Question> GetFavorites(string username, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Favorite CreateFavoriteQuestion(int questionId, string username)
        {
            throw new NotImplementedException();
        }

        public Favorite CreateFavoriteQuestion(int questionId, string username, string note)
        {
            throw new NotImplementedException();
        }
    }
}
