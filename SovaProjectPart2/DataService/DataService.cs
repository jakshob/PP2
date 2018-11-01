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
                return db.Answers.ToList();
            }
        }

        public List<Question> GetQuestions(int page, int pageSize) {

            using (var db = new SovaContext())
            {
                return db.Questions.ToList();
            }
        }

        public List<Answer> GetAnswersToQuestion(int inputId, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                var answerList = db.Answers.Where(x => x.ParentId == inputId);
                // Skal vi lave sort by score???

                return answerList.ToList();
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
                var commentList = db.Comments.Where(x => x.PostId == inputId);

                return commentList.ToList();
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
                // OBS!!! SØGER IKKE I BODY!!
                //Starter med søgning i navn, senere kan tilføjes body!

                //Liste kun hvor indeholder "searchText" i navnet + gør mindre til comparison
                var questionsFromSearch = db.Questions.Where(p => p.Name.ToLower().Contains(searchText.ToLower()));
                //output to List<Post>
                var queSortByScore = questionsFromSearch.OrderByDescending(x => x.Score).ToList();

                return queSortByScore;
            }    
        }

        public List<History> GetHistory(string username, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {

                var historyByUser = db.Histories.Where(h => h.SOVA_UserUsername == username); 
                return historyByUser.ToList();
            }
        }

        public List<string> GetMostUsedSearchTexts(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<Question> GetFavorites(string username, int page, int pageSize)
        {
            /*
            using (var db = new SovaContext())
            {
                //DENNE FUNKTION HAR EN FEJL!!
                //Select from favorites et postid, som referer til linq posts
                //output post som questions
                
                var favoritesByUser = from h in db.Favorites
                                    where h.SOVA_UserUsername == username
                                    select h;
                List<Question> outputList;
                foreach (Favorite f in favoritesByUser) {

                var que = db.Questions.Where(x => x.Id == f.PostId).ToList;

                    outputList.Add(que.ToList);
                    
                }

                return outputList;
            }
            */
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