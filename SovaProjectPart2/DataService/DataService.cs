using System;
using System.Collections.Generic;
using System.Linq;


namespace DomainModel
{
    public class DataService : IDataService
    {
        //_____________________Hardcoded USER________MEGA NEDEREN KODE_____________
        readonly List<SOVA_User> _users = new List<SOVA_User>();

        public DataService()
        {
            _users.Add(new SOVA_User()
            {
               Username = "ObiWan.Kenobi"
            });
        }

        public SOVA_User GetUser(string username)
        {
            return _users.FirstOrDefault(x => x.Username == username);
        }

        public SOVA_User CreateUser(string name, string username, string password, string salt)
        {
            var user = new SOVA_User()
            {
                Username = username,
                Password = password,
                Salt = salt
            };
            _users.Add(user);
            return user;
        }

    
        ///____________________________________________Her starter den fede kode___________________


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
                return db.Questions
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public List<Answer> GetAnswersToQuestion(int inputId, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                var answerList = db.Answers.Where(x => x.ParentId == inputId);
                // Skal vi lave sort by score???

                return answerList
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }


        public Answer GetAnswer(int inputId)
        {

            using (var db = new SovaContext())
            {
                return db.Answers.Find(inputId);
            }
		}
		public Question GetQuestion(int inputId) {

			using (var db = new SovaContext()) {
				return db.Questions.Find(inputId);
			}
		}
		public List<Comment> GetCommentsByPostId(int inputId, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                var commentList = db.Comments.Where(x => x.PostId == inputId);

                return commentList
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
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

                return queSortByScore
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }    
        }

        public List<History> GetHistory(string username, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {

                var historyByUser = db.Histories.Where(h => h.SOVA_UserUsername == username); 
                return historyByUser
                    .Skip(page * pageSize)

                    .Take(page)
                    .ToList();
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

                return outputList
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
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

        public int GetNumberOfQuestions()
        {
            using (var db = new SovaContext())
            {
                return db.Questions.Count();
            }
        }


        public int GetNumberOfComments()
        {
            using (var db = new SovaContext())
            {
                return db.Comments.Count();
            }
        }

       
    }
}